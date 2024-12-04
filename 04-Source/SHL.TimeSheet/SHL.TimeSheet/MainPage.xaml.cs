using SHL.Settings.BusinessLayer;
using SHL.Settings.Model;
using SHL.Syncronism.BusinessLayer;
using SHL.Syncronism.Model;
using SHL.TimeSheet.Service;
using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SHL.Parameter.BusinessLayer;
using SHL.Project.BusinessLayer;
using SHL.TimeSheet.BusinessLayer;
using SHL.Parameter.Model;
using SHL.ITimeSheetNG.Model;
using SHL.IRetorno.Model;
using SHL.Project.Model;
using SHL.TimeSheet.Model;
using SHL.Task.Model;
using SHL.Task.BusinessLayer;
using SHL.Data.Base;

namespace SHL.TimeSheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        #region [Constructors]
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, true);
            InitializeComponent();            
        }
        #endregion

        #region [Private members]
        protected Boolean m_canclick = true;
        #endregion

        #region [Events]
        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitDialog();
        }
        private async void cmdAppointments_Click(object sender, EventArgs args)
        {
            try
            {
                if (m_canclick)
                {
                    OneClick();
                    await Navigation.PushModalAsync(new NavigationPage(new Appointment()));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void cmdSendEmail_Click(object sender, EventArgs args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void cmdSyncronism_Click(object sender, EventArgs args)
        {
            try
            {
                LoadSyncronism();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void cmdSettings_Click(object sender, EventArgs args)
        {
            try
            {
                if (m_canclick)
                {
                    OneClick();
                    LoadSettings(true);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        void ClickedOnlineOffLine(object sender, ToggledEventArgs e)
        {
            if (!WhereDataOnline(e.Value))
               swtWhere.IsToggled = false;
        }
        #endregion

        #region [Private methods]
        private void InitDialog()
        {
            SETTINGS settings = new SETTINGS();
            SETTINGS_BL settingsbl = new SETTINGS_BL();

            try
            {
                LoadSettings(false);

                settings.KEY = "WHEREDATA";
                settings = settingsbl.Select(settings);

                if (settings != null)
                {
                    if (settings.VALUE == "ONLINE")
                    {
                        swtWhere.IsToggled = true;
                        GlobalData.WhereDatabase = Utils.WHEREDATA.REMOTE;
                    }
                    else
                    {
                        swtWhere.IsToggled = false;
                        GlobalData.WhereDatabase = Utils.WHEREDATA.LOCAL;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private void LoadSettings(Boolean loadsettinganyway)
        {
            SETTINGS settings = null;
            SETTINGS_BL settingsbl = new SETTINGS_BL();
            List<SETTINGS> settingslist = null;

            try
            {
                if (loadsettinganyway)
                {
                    Navigation.PushModalAsync(new NavigationPage(new Settings()));
                }
                else
                { 
                    settingslist = settingsbl.SelectList();

                    if (settingslist == null || settingslist.Count == 0)
                    {
                        settings = new SETTINGS();
                        settings.KEY = "WHEREDATA";
                        settings.VALUE = "ONLINE";
                        settingsbl.Insert(settings);

                        #if DEBUG
                        settings = new SETTINGS();
                        settings.KEY = "url_timesheet";
                        settings.VALUE = "http://10.0.2.2:5000/api/";
                        settingsbl.Insert(settings);

                        settings = new SETTINGS();
                        settings.KEY = "url_tokensecurity";
                        settings.VALUE = "http://10.0.2.2:5000/api/";
                        settingsbl.Insert(settings);
                        #else
                        settings = new SETTINGS();
                        settings.KEY = "url_timesheet";
                        settings.VALUE = "http://";
                        settingsbl.Insert(settings);

                        settings = new SETTINGS();
                        settings.KEY = "url_tokensecurity";
                        settings.VALUE = "http://";
                        settingsbl.Insert(settings);
                        Navigation.PushModalAsync(new NavigationPage(new Settings()));
                        #endif                        
                    }
                }
            }
            catch(Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private void LoadSyncronism()
        {
            try
            {
                SyncronismData();
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private void SyncronismData()
        {
            try
            {
                if (!SendData())
                    throw new Exception("Erro ao enviar dados");

                if (!ReceiveData())
                    throw new Exception("Erro ao receber dados");

                DisplayAlert("Aviso", "Sincronismo finalizado com sucesso", "OK");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private Boolean SendData()
        {
            Boolean ret = false;

            List<SYNCRONISM> syncronismlist = new List<SYNCRONISM>();
            SYNCRONISM_BL syncronismbl = new SYNCRONISM_BL();
            DataServiceSyncronism dataservicesycronism = new DataServiceSyncronism();

            syncronismlist = syncronismbl.SelectList();

            if (syncronismlist == null || syncronismlist.Count == 0)
                return true;

            if(dataservicesycronism.AddData(syncronismlist))
                syncronismbl.Delete(new SYNCRONISM());

            return ret;
        }

        private Boolean ReceiveData()
        {
            Boolean ret = false;
            PARAMETER_BL parameterbl = new PARAMETER_BL();
            SqliteTransaction transaction = null;

            try
            {
                transaction = parameterbl.GetTransaction();
                
                SyncronismParameter(transaction);
                SyncronismProject(transaction);
                SyncronismTimeSheet(transaction);

                transaction.Commit();

                ret = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                DisplayAlert("Alerta", ex.Message, "OK");
            }

            return ret;
        }

        private void SyncronismParameter(SqliteTransaction transaction)
        {
            DataServiceParameter dataserviceparameter = new DataServiceParameter();
            PARAMETERNG parameterng = null;
            
            PARAMETER_BL parameterbl = new PARAMETER_BL();
            String msgerror = String.Empty;

            parameterbl.Delete(transaction, new PARAMETER());

            parameterng = dataserviceparameter.GetAllRemoteData(new PARAMETER());

            if (parameterng != null && parameterng.errors != null && parameterng.errors.Count > 0)
            {
                foreach (RETORNO error in parameterng.errors)
                {
                    msgerror += error.mensagem;
                }
                throw new Exception(msgerror);
            }

            if (parameterng != null && parameterng.records != null && parameterng.records.Count > 0)
            {
                foreach (PARAMETER record in parameterng.records)
                {
                    parameterbl.Insert(transaction, record);
                }
            }
        }

        private void SyncronismProject(SqliteTransaction transaction)
        {
            DataServiceProject dataserviceproject = new DataServiceProject();
            PROJECTNG projectrng = null;
            
            PROJECT_BL projectbl = new PROJECT_BL();
            String msgerror = String.Empty;

            projectbl.Delete(transaction, new PROJECT());

            projectrng = dataserviceproject.GetAllRemoteData(new PROJECT());

            if (projectrng != null && projectrng.errors != null && projectrng.errors.Count > 0)
            {
                foreach (RETORNO error in projectrng.errors)
                {
                    msgerror += error.mensagem;
                }
                throw new Exception(msgerror);
            }

            if (projectrng != null && projectrng.records != null && projectrng.records.Count > 0)
            {
                foreach (PROJECT record in projectrng.records)
                {
                    projectbl.Insert(transaction, record);
                }
            }
        }

        private void SyncronismTimeSheet(SqliteTransaction transaction)
        {
            DataServiceTimeSheet dataservicetimesheet = new DataServiceTimeSheet();
            TIMESHEETNG timesheetng = null;
            PROJECT project = null;
            
            TIMESHEET_BL timesheetbl = new TIMESHEET_BL();
            TASK_BL taskbl = new TASK_BL();
            PROJECT_BL projectbl = new PROJECT_BL();
            String msgerror = String.Empty;
            Int32 id_timesheet;

            taskbl.Delete(transaction, new TASK());
            timesheetbl.Delete(transaction, new TIMESHEET());

            timesheetng = dataservicetimesheet.GetAllRemoteData(null, null);

            if (timesheetng != null && timesheetng.errors != null && timesheetng.errors.Count > 0)
            {
                foreach (RETORNO error in timesheetng.errors)
                {
                    msgerror += error.mensagem;
                }
                throw new Exception(msgerror);
            }

            if (timesheetng != null && timesheetng.records != null && timesheetng.records.Count > 0)
            {
                foreach (TIMESHEET record in timesheetng.records)
                {
                    timesheetbl.Insert(record, transaction);
                    id_timesheet = Convert.ToInt32(SQLHelper.ExecuteScalar(transaction.Connection, transaction, "select last_insert_rowid();")); 

                    foreach (TASK task in record.TASKS)
                    {
                        project = new PROJECT();

                        project.NAME = task.PROJECTNAME;
                        project = projectbl.Select(project);

                        task.ID_TIMESHEET = id_timesheet;
                        task.ID_PROJECT = project.ID_PROJECT;

                        taskbl.Insert(transaction, task);
                    }
                }
            }
        }

        private Boolean WhereDataOnline(Boolean isonline)
        {
            Boolean ret = false;
            SETTINGS settings = new SETTINGS();
            SETTINGS_BL settingsbl = new SETTINGS_BL();

            List<SYNCRONISM> syncronismlist = new List<SYNCRONISM>();
            SYNCRONISM_BL syncronismbl = new SYNCRONISM_BL();

            try
            {
                if (isonline)
                {
                    syncronismlist = syncronismbl.SelectList();

                    if (syncronismlist != null && syncronismlist.Count > 0)
                    {
                        DisplayAlert("Alerta", "Faça um sincronismo antes de tornar ONLINE.", "OK");

                        return false;
                    }

                    settings.KEY = "WHEREDATA";
                    settings.VALUE = "ONLINE";
                    settingsbl.Update(settings);
                    GlobalData.WhereDatabase = Utils.WHEREDATA.REMOTE;
                }
                else
                {
                    settings.KEY = "WHEREDATA";
                    settings.VALUE = "OFFLINE";
                    settingsbl.Update(settings);
                    GlobalData.WhereDatabase = Utils.WHEREDATA.LOCAL;
                }

                ret = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }

            return ret;
        }

        protected async void OneClick()
        {
            m_canclick = false;
            await System.Threading.Tasks.Task.Delay(10000);
            m_canclick = true;
        }
        #endregion
    }

}
