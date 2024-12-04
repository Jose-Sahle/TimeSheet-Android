using SHL.ITimeSheetNG.Model;
using SHL.Project.BusinessLayer;
using SHL.Project.Model;
using SHL.Task.Model;
using SHL.TimeSheet.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SHL.TimeSheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTask : ContentPage
    {
        #region [Contructors/Destructors]
        public EditTask()
        {
            InitializeComponent();
        }
        #endregion

        #region [Private Members]
        private TASK m_task;
        private TaskAppointment parentpage = null;
        private Boolean m_addnew = false;
        #endregion

        #region [Properties]
        #endregion

        #region [Events]
        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitDialog();
        }

        private async void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SelecionaProject();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void cmdFinish_Click(object sender, EventArgs args)
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

        private async void cmdBack_Click(object sender, EventArgs args)
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

        #region [Private Methods]
        private void InitDialog()
        {
            try
            {
                PreencheProject();
                ClearFields();
                parentpage = BindingContext as TaskAppointment;
                m_task = parentpage.Task;
                if (m_task == null)
                {
                    m_addnew = true;
                    m_task = new TASK();
                }
                else
                    FillFields();

            }
            catch(Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private void ClearFields()
        {
            txtIndieTask.Text = String.Empty;
            txtStartTask.Text = String.Empty;
            txtEndTask.Text = String.Empty;
            pckProject.SelectedIndex = -1;
            txtDescription.Text = String.Empty;
        }

        private void FillFields()
        {
            List<PROJECT> projectlist = pckProject.ItemsSource as List<PROJECT>;
            
            txtIndieTask.Text = m_task.INDICE_PROJECT;
            txtStartTask.Text = m_task.START_TM.ToString();
            txtEndTask.Text = m_task.END_TM.ToString();

            for (int INDEX = 0; INDEX < projectlist.Count; INDEX++)
            {
                if (projectlist[INDEX].ID_PROJECT == m_task.ID_PROJECT)
                {
                    pckProject.SelectedIndex = INDEX;
                    break;
                }
            }
            txtDescription.Text = m_task.DESCRIPTION;
        }

        private void PreencheProject()
        {
            DataServiceProject dataservice = new DataServiceProject();            
            PROJECTNG projectng = dataservice.GetAllData(GlobalData.WhereDatabase, new PROJECT());

            if (projectng == null || projectng.records == null)
            {
                throw new Exception("Não foi possível acessar a tabela PROJECT");
            }

            foreach(PROJECT project in projectng.records)
            {
                project.END_DT = DateTime.Now;
            }

            pckProject.ItemsSource = projectng.records;
        }

        private void SelecionaProject()
        {
            PROJECT project = null;

            project = (PROJECT)pckProject.SelectedItem;
        }

        private Boolean ValidateFields()
        {
            Boolean ret = true;

            CultureInfo cultureinfo = new CultureInfo("pt-BR");
            PROJECT project = null;

            project = (PROJECT)pckProject.SelectedItem;

            if (txtStartTask.Text.Trim() == String.Empty)
                throw new Exception("Preencha o ínicio da atividade");

            if (txtEndTask.Text.Trim() == String.Empty)
                throw new Exception("Preencha o término da atividade");

            if (txtIndieTask.Text.Trim() == String.Empty)
                throw new Exception("Preenche o Índice");

            if (txtDescription.Text.Trim() == String.Empty)
                throw new Exception("Preenche a descrição");

            if (project == null)
                throw new Exception("Selecione um projeto");

            m_task.START_TM = txtStartTask.Text.Trim();
            m_task.END_TM = txtEndTask.Text.Trim();
            m_task.INDICE_PROJECT = txtIndieTask.Text;
            m_task.DESCRIPTION = txtDescription.Text;
            m_task.ID_PROJECT = project.ID_PROJECT;
            m_task.PROJECTNAME = project.NAME;

            return ret;
        }

        private void Finish()
        {
            if (!ValidateFields())
                return;

            if (m_addnew)
                parentpage.TasksAppointment.Add(m_task);
            else
                parentpage.Task = m_task;

            Navigation.PopModalAsync();
        }
        #endregion
    }
}