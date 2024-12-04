using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SHL.Settings.Model;
using SHL.Settings.BusinessLayer;
using SHL.IRetorno.Model;

namespace SHL.TimeSheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSettings : ContentPage
    {
        #region [Constructor]
        public EditSettings()
        {
            InitializeComponent();
        }
        #endregion

        #region [Private Methods]
        #endregion

        #region [Events]
        private async void cmdFinish_Click(object sender, EventArgs args)
        {            
            try
            {
                SaveSettings();
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
        private async void SaveSettings()
        {
            SETTINGS settingsfind = null;
            SETTINGS settings = null;
            SETTINGS_BL settingsbl = null;

            try
            {
                settingsfind = new SETTINGS();
                settingsbl = new SETTINGS_BL();

                settings = (SETTINGS)BindingContext;

                settingsfind.KEY = settings.KEY;
                settingsfind = settingsbl.Select(settingsfind);

                if (settingsfind == null)
                    settingsbl.Insert(settings);
                else
                    settingsbl.Update(settings);

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