using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SHL.Settings.Model;
using SHL.Settings.BusinessLayer;
using SHL.TimeSheet.Service;

namespace SHL.TimeSheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        #region [Constructors]
        public Settings()
        {
            InitializeComponent();
        }
        #endregion

        #region [Private Members]
        SETTINGS m_settings;
        #endregion

        #region [Events]
        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitDialog();
        }

        private async void cmdAddSettings_Click(object sender, EventArgs args)
        {
            try
            {
                AddSettings();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
        
        private async void cmdEditSettings_Click(object sender, EventArgs args)
        {
            try
            {
                EditSettings();
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
                    m_settings = e.SelectedItem as SETTINGS;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
        #endregion

        #region [Private Members]
        private void InitDialog()
        {
            SETTINGS_BL settingsbl = new SETTINGS_BL();
            List<SETTINGS> settingslist = null;

            try
            {
                settingslist = settingsbl.SelectList();

                lstSettings.ItemsSource = settingslist.OrderBy(d => d.KEY).ToList();
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void AddSettings()
        {
            try
            {
                await Navigation.PushModalAsync(new NavigationPage(new EditSettings {BindingContext = new SETTINGS()}));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private async void EditSettings()
        {
            try
            {
                if (m_settings != null)
                {
                    await Navigation.PushModalAsync(new NavigationPage(new EditSettings { BindingContext = m_settings }));
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
            SETTINGS_BL settingsbl = new SETTINGS_BL();
            List<SETTINGS> settingslist = null;

            try
            {
                settingslist = settingsbl.SelectList();

                if (settingslist == null || settingslist.Count == 0)
                {
                    await DisplayAlert("Alerta", "Inclua ", "OK");
                }
                else
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