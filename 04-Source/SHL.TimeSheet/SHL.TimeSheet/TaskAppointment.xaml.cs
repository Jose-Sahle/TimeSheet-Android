/**********************************************************
  AUTHOR	: Jose Sahle Netto
  VERSION	: 1.0.0.0
  DATE		: 29/03/2020 02:49:05
**********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SHL.Task.Model;

namespace SHL.TimeSheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskAppointment : ContentPage
    {
        #region [Contructors]
        /// <summary>
        /// 
        /// </summary>
        public TaskAppointment()
        {
            InitializeComponent();
        }
        #endregion

        #region [Private Members]
        private List<TASK> m_tasks;
        private TASK m_task;
        private Boolean m_first = true;
        #endregion

        #region [Properties]
        public List<TASK> TasksAppointment { get{ return m_tasks; } }
        public TASK Task { get { return m_task; } set { m_task = value; } }
        #endregion

        #region [Events]
        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitDialog();
        }

        private async void cmdAddTask_Click(object sender, EventArgs args)
        {
            try
            {
                AddTask();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void cmdEditTask_Click(object sender, EventArgs args)
        {
            try
            {
                EditTask();
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

        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem != null)
                    m_task = e.SelectedItem as TASK;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
        #endregion

        #region [Private Methods]
        /// <summary>
        /// 
        /// </summary>
        private void InitDialog()
        {
            try
            {
                if (m_first)
                {
                    m_first = false;
                    var parentpage = BindingContext as Appointment;
                    m_tasks = parentpage.Tasks;
                }

                lstTasks.ItemsSource = m_tasks
                .OrderBy(d => d.START_TM)
                .ToList();
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void AddTask()
        {
            try
            {
                m_task = null;
                await Navigation.PushModalAsync(new NavigationPage(new EditTask { BindingContext = this }));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void EditTask()
        {
            try
            {
                if (m_task != null)
                {
                    await Navigation.PushModalAsync(new NavigationPage(new EditTask { BindingContext = this }));
                }
                else
                    await DisplayAlert("Aviso", "Selecione uma chave", "OK");
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