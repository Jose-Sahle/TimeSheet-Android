using SHL.IRetorno.Model;
using SHL.Settings.BusinessLayer;
using SHL.Settings.Model;
using SHL.Syncronism.Model;
using SHL.TokenSecurity;
using SHL.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHL.TimeSheet.Service
{
    public class DataServiceSyncronism
    {
        #region [Constructors]
        public DataServiceSyncronism()
        {
            SETTINGS settings = null;
            SETTINGS_BL settingsbl = new SETTINGS_BL();

            settings = new SETTINGS();
            settings.KEY = "url_timesheet";
            settings = settingsbl.Select(settings);
            if (settings != null)
                SyncronismUrl = settings.VALUE;

            settings = new SETTINGS();
            settings.KEY = "url_tokensecurity";
            settings = settingsbl.Select(settings);
            if (settings != null)
                TokeSecurityUrl = settings.VALUE;
        }
        #endregion

        #region [Private Members]
        String m_url_syncronism = "http://10.0.2.2:5000/api/";
        String m_url_tokensecurity = "http://10.0.2.2:5000/api/";
        #endregion

        #region [Properties]
        public String SyncronismUrl
        {
            get { return m_url_syncronism; }
            set { m_url_syncronism = value; }
        }

        public String TokeSecurityUrl
        {
            get { return m_url_tokensecurity; }
            set { m_url_tokensecurity = value; }
        }
        #endregion

        #region [Actions]
        public Boolean AddData(List<SYNCRONISM> syncronism)
        {
            Boolean ret = false;

            String hash = String.Empty;
            String tokensecurity = String.Empty;
            Int32 repeatedcount = 0;
            List<RETORNO> retornos = null;

            try
            {
                while (tokensecurity == String.Empty && repeatedcount < 5)
                {
                    hash = TokenKey.GetCryptMessage("SHL", "/api/TimeSheetNG/Insert");

                    tokensecurity = (String)(Util.ControllerSelectEx<String>(m_url_tokensecurity, "TokenSecurity", "GetTokenSecurity", "key", hash));

                    repeatedcount++;
                }

                retornos = (List<RETORNO>)Util.ControllerPostExT<List<RETORNO>>(true, m_url_syncronism, "Syncronismng", "Sendto", syncronism, "tokensecurity", tokensecurity);

                if (retornos == null || retornos.Count == 0)
                    ret = true;
                else
                    ret = false;
            }
            catch
            {
                
            }

            return ret;
        }
        #endregion
    }
}
