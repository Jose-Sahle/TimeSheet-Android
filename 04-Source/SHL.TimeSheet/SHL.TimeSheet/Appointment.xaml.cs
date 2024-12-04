using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SHL.ITimeSheetNG.Model;
using SHL.TimeSheet.Model;
using SHL.TimeSheet.Service;
using System.Globalization;
using SHL.Task.Model;
using SHL.Settings.Model;
using SHL.Settings.BusinessLayer;

namespace SHL.TimeSheet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Appointment : ContentPage
	{
        #region [Constructors]
        public Appointment()
        {
            InitializeComponent();
        }
        #endregion

        #region [Private Members]
        private Boolean m_isnewrecord = false;
        private String m_timespanhours = String.Empty;
        private List<TASK> m_tasks = null;
        private Boolean m_loadfrommainpage = true;
        #endregion

        #region [Properties]
        public string TimeSpanHours
        {
            get { return m_timespanhours; }
            set
            {
                m_timespanhours = value;
                OnPropertyChanged(nameof(TimeSpanHours)); 
            }
        }

        public List<TASK> Tasks
        {
            get { return m_tasks; }
            set { m_tasks = value; }
        }
        #endregion

        #region [Events]
        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitDialog();
        }

        private async void cmdSave_Clicked(object sender, EventArgs args)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void cmdTask_Clicked(object sender, EventArgs args)
        {
            try
            {
                LoadTask();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void cmdFinish_Clicked(object sender, EventArgs args)
        {
            try
            {
                Finish();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void txtDataRG_OnCompleted(object sender, EventArgs args)
        {
            try
            {
                BindingContext = this;
                ClearFields();
                LoadDatas();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void txtDataRG_OnUnfocused(object sender, EventArgs args)
        {
            try
            {
                BindingContext = this;
                ClearFields();
                LoadDatas();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void Hours_OnUnfocused(object sender, EventArgs args)
        {
            try
            {
                BindingContext = this;
                CalcHours();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        #endregion

        #region [Private Methods]
        private void InitDialog()
        {
            String data = String.Empty;

            try
            {
                if (m_loadfrommainpage)
                {
                    data = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDateRG.Text = data;
                    ClearFields();
                    LoadDatas();
                    m_loadfrommainpage = false;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private void ClearFields()
        {
            txtEnterAM.Text = String.Empty;
            txtExitAM.Text = String.Empty;
            txtEnterPM.Text = String.Empty;
            txtExitPM.Text = String.Empty;
            txtDescription.Text = String.Empty;
            m_tasks = new List<TASK>();
        }

        private async void CalcHours()
        {
            TimeSpan start_am;
            TimeSpan end_am;

            TimeSpan start_pm;
            TimeSpan end_pm;

            TimeSpan am;
            TimeSpan pm;

            TimeSpan ampluspm;

            try
            {
                if (txtEnterAM.Text.Trim() != String.Empty)
                {
                    start_am = TimeSpan.Parse(txtEnterAM.Text.Trim());

                    if (txtExitAM.Text.Trim() != String.Empty)
                        end_am = TimeSpan.Parse(txtExitAM.Text.Trim());
                    else
                        end_am = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));

                    am = end_am - start_am;

                    if (txtEnterPM.Text.Trim() != String.Empty)
                    {
                        start_pm = TimeSpan.Parse(txtEnterPM.Text.Trim());

                        if (txtExitPM.Text.Trim() != String.Empty)
                            end_pm = TimeSpan.Parse(txtExitPM.Text.Trim());
                        else
                            end_pm = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));

                        pm = end_pm - start_pm;

                        ampluspm = am + pm;

                        TimeSpanHours = String.Format("{0:D2}:{1:D2}", ampluspm.Hours, ampluspm.Minutes);
                    }
                    else
                    {
                        TimeSpanHours = String.Format("{0:D2}:{1:D2}", am.Hours, am.Minutes);
                    }
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void LoadDatas()
        {
            TIMESHEET timesheet = null;
            TIMESHEETNG timesheetng = null;

            DataServiceTimeSheet dataservice = null;
            DateTime date = DateTime.Now;

            CultureInfo cultureinfo = new CultureInfo("pt-BR");

            try
            {
                date = DateTime.Parse(txtDateRG.Text, cultureinfo);

                dataservice = new DataServiceTimeSheet();

                timesheetng = dataservice.GetData(GlobalData.WhereDatabase, date);

                if (timesheetng != null && timesheetng.records != null && timesheetng.records.Count > 0)
                {
                    m_isnewrecord = false;

                    timesheet = timesheetng.records[0];

                    txtEnterAM.Text = timesheet.START_AM != null ? timesheet.START_AM.ToString() : String.Empty;
                    txtExitAM.Text = timesheet.END_AM != null ? timesheet.END_AM.ToString() : String.Empty;
                    txtEnterPM.Text = timesheet.START_PM != null ? timesheet.START_PM.ToString() : String.Empty;
                    txtExitPM.Text = timesheet.END_PM != null ? timesheet.END_PM.ToString() : String.Empty;
                    txtDescription.Text = timesheet.DESCRIPTION != null ? timesheet.DESCRIPTION : String.Empty;
                    m_tasks = timesheet.TASKS;
                }
                else
                {
                    m_isnewrecord = true;
                    ClearFields();
                }

                CalcHours();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void LoadTask()
        {
            try
            {
                await Navigation.PushModalAsync(new NavigationPage(new TaskAppointment { BindingContext = this }));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void Save()
        {
            TIMESHEET timesheet = null;

            DataServiceTimeSheet dataservice = null;
            CultureInfo cultureinfo = new CultureInfo("pt-BR");

            try
            {
                dataservice = new DataServiceTimeSheet();
                timesheet = new TIMESHEET();

                timesheet.DATE_RG = DateTime.Parse(txtDateRG.Text, cultureinfo);

                if (txtEnterAM.Text.Trim() != String.Empty)
                    timesheet.START_AM = txtEnterAM.Text.Trim();

                if (txtExitAM.Text.Trim() != String.Empty)
                    timesheet.END_AM = txtExitAM.Text.Trim();

                if (txtEnterPM.Text.Trim() != String.Empty)
                    timesheet.START_PM = txtEnterPM.Text.Trim();

                if (txtExitPM.Text.Trim() != String.Empty)
                    timesheet.END_PM = txtExitPM.Text.Trim();

                if (txtDescription.Text != String.Empty)
                    timesheet.DESCRIPTION = txtDescription.Text;

                timesheet.TASKS = m_tasks;

                if (m_isnewrecord)
                {
                    dataservice.AddData(GlobalData.WhereDatabase, timesheet);
                    m_isnewrecord = false;
                }
                else
                    dataservice.UpdateData(GlobalData.WhereDatabase, timesheet);

                await DisplayAlert("Alerta", "Salvo", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void Finish()
        {
            try
            {
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
        #endregion
    }
}