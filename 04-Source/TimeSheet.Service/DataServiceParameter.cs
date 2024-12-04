using Newtonsoft.Json;
using SHL.IRetorno.Model;
using SHL.ITimeSheetNG.Model;
using SHL.Parameter.BusinessLayer;
using SHL.Parameter.Model;
using SHL.Settings.BusinessLayer;
using SHL.Settings.Model;
using SHL.Syncronism.BusinessLayer;
using SHL.Syncronism.Model;
using SHL.TokenSecurity;
using SHL.Utils;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SHL.TimeSheet.Service
{
    public class DataServiceParameter
    {
        #region [Constructors]
        public DataServiceParameter()
        {
            SETTINGS settings = null;
            SETTINGS_BL settingsbl = new SETTINGS_BL();

            settings = new SETTINGS();
            settings.KEY = "url_timesheet";
            settings = settingsbl.Select(settings);
            if (settings != null)
                ParameterUrl = settings.VALUE;

            settings = new SETTINGS();
            settings.KEY = "url_tokensecurity";
            settings = settingsbl.Select(settings);
            if (settings != null)
                TokeSecurityUrl = settings.VALUE;
        }
        #endregion

        #region [Private Members]
        String m_url_parameter = "http://10.0.2.2:5000/api/";
        String m_url_tokensecurity = "http://10.0.2.2:5000/api/";
        #endregion

        #region [Properties]
        public String ParameterUrl
        {
            get { return m_url_parameter; }
            set { m_url_parameter = value; }
        }

        public String TokeSecurityUrl
        {
            get { return m_url_tokensecurity; }
            set { m_url_tokensecurity = value; }
        }
        #endregion

        #region [Actions]
        public void AddData(WHEREDATA wheredata, PARAMETER parameter)
        {
            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    AddRemoteData(parameter);
                    AddLocalData(parameter, false);
                    break;
                case WHEREDATA.LOCAL:
                    AddLocalData(parameter, true);
                    break;
            }
        }

        public void UpdateData(WHEREDATA wheredata, PARAMETER parameter)
        {
            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    UpdateRemoteData(parameter);
                    UpdateLocalData(parameter, false);
                    break;
                case WHEREDATA.LOCAL:
                    UpdateLocalData(parameter, true);
                    break;
            }
        }

        public void DeleteData(WHEREDATA wheredata, PARAMETER parameter)
        {
            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    DeleteRemoteData(parameter);
                    DeleteLocalData(parameter, false);
                    break;
                case WHEREDATA.LOCAL:
                    DeleteLocalData(parameter, true);
                    break;
            }
        }

        public PARAMETERNG GetData(WHEREDATA wheredata, PARAMETER parameter)
        {
            PARAMETERNG ret = null;

            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    ret = GetRemoteData(parameter);
                    break;
                case WHEREDATA.LOCAL:
                    ret = GetLocalData(parameter);
                    break;
            }

            return ret;
        }

        public PARAMETERNG GetAllData(WHEREDATA wheredata, PARAMETER parameter)
        {
            PARAMETERNG ret = null;

            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    ret = GetAllRemoteData(parameter);
                    break;
                case WHEREDATA.LOCAL:
                    ret = GetAllLocalData(parameter);
                    break;
            }

            return ret;
        }
        #endregion

        #region [Remote]
        public void AddRemoteData(PARAMETER parameter)
        {
            String hash = String.Empty;
            String tokensecurity = String.Empty;
            Int32 repeatedcount = 0;
            try
            {
                while (tokensecurity == String.Empty && repeatedcount < 5)
                {
                    hash = TokenKey.GetCryptMessage("SHL", "/api/TimeSheetNG/Insert");

                    tokensecurity = (String)(Util.ControllerSelectEx<String>(m_url_tokensecurity, "TokenSecurity", "GetTokenSecurity", "key", hash));

                    repeatedcount++;
                }

                Util.ControllerPostExT<List<RETORNO>>(true, m_url_parameter, "parameterng", "insert", parameter, "tokensecurity", tokensecurity);
            }
            catch
            {
                AddLocalData(parameter, true);
            }
        }

        public void UpdateRemoteData(PARAMETER parameter)
        {
            String hash = String.Empty;
            String tokensecurity = String.Empty;
            Int32 repeatedcount = 0;

            try
            {
                while (tokensecurity == String.Empty && repeatedcount < 5)
                {
                    hash = TokenKey.GetCryptMessage("SHL", "/api/TimeSheetNG/Update");

                    tokensecurity = (String)(Util.ControllerSelectEx<String>(m_url_tokensecurity, "TokenSecurity", "GetTokenSecurity", "key", hash));

                    repeatedcount++;
                }

                Util.ControllerPutExT<List<RETORNO>>(m_url_parameter, "parameterng", "update", parameter, "tokensecurity", tokensecurity);
            }
            catch
            {
                UpdateLocalData(parameter, true);
            }
        }

        public void DeleteRemoteData(PARAMETER parameter)
        {
            String hash = String.Empty;
            String tokensecurity = String.Empty;
            Int32 repeatedcount = 0;

            try
            {
                while (tokensecurity == String.Empty && repeatedcount < 5)
                {
                    hash = TokenKey.GetCryptMessage("SHL", "/api/TimeSheetNG/Delete");

                    tokensecurity = (String)(Util.ControllerSelectEx<String>(m_url_tokensecurity, "TokenSecurity", "GetTokenSecurity", "key", hash));

                    repeatedcount++;
                }

                Util.ControllerDeleteExT<List<RETORNO>>(m_url_parameter, "parameterng", "delete", parameter, "tokensecurity", tokensecurity);
            }
            catch
            {
                DeleteLocalData(parameter, true);
            }
        }

        public PARAMETERNG GetRemoteData(PARAMETER parameter)
        {
            PARAMETERNG parameterng = null;
            String hash = String.Empty;
            String tokensecurity = String.Empty;
            Int32 repeatedcount = 0;

            try
            {
                while (tokensecurity == String.Empty && repeatedcount < 5)
                {
                    hash = TokenKey.GetCryptMessage("SHL", "/api/TimeSheetNG/Select");

                    tokensecurity = (String)(Util.ControllerSelectEx<String>(m_url_tokensecurity, "TokenSecurity", "GetTokenSecurity", "key", hash));

                    repeatedcount++;
                }

                parameterng = (PARAMETERNG)(Util.ControllerPostExT<PARAMETERNG>(true, m_url_parameter, "parameterng", "select", parameter, "tokensecurity", tokensecurity));

            }
            catch
            {
                GetLocalData(parameter);
            }

            return parameterng;
        }

        public PARAMETERNG GetAllRemoteData(PARAMETER parameter)
        {
            PARAMETERNG parameterng = null;
            Int32 repeatedcount = 0;

            String hash = String.Empty;
            String tokensecurity = String.Empty;

            try
            {
                parameter = new PARAMETER();

                while (tokensecurity == String.Empty && repeatedcount < 5)
                {
                    hash = TokenKey.GetCryptMessage("SHL", "/api/TimeSheetNG/SelectAll");

                    tokensecurity = (String)(Util.ControllerSelectEx<String>(m_url_tokensecurity, "TokenSecurity", "GetTokenSecurity", "key", hash));

                    repeatedcount++;
                }

                parameterng = (PARAMETERNG)(Util.ControllerSelectEx<PARAMETERNG>(m_url_parameter, "parameterng", "selectall", "tokensecurity", tokensecurity));

            }
            catch
            {
                GetAllLocalData(parameter);
            }

            return parameterng;
        }
        #endregion

        #region [Local]
        public void AddLocalData(PARAMETER parameter, Boolean needsynchronize)
        {
            PARAMETER_BL parameterbl = new PARAMETER_BL();
            SYNCRONISM_BL syncronismbl = null;
            SYNCRONISM syncronism = null;
            SqliteTransaction transaction = null;            

            try
            {
                if (needsynchronize)
                {
                    transaction = parameterbl.GetTransaction();

                    parameterbl.Insert(transaction, parameter);

                    syncronismbl = new SYNCRONISM_BL();
                    syncronism = new SYNCRONISM();

                    syncronism.DATE_MODIFICATION = DateTime.Now;
                    syncronism.TABLENAME = "PARAMETER";
                    syncronism.OPERATION = 1;
                    syncronism.DATA = JsonConvert.SerializeObject(parameter);
                    syncronismbl.Insert(transaction, syncronism);
                }
                else
                    parameterbl.Insert(parameter);
            }
            catch (Exception ex)
            {
                if (needsynchronize)
                    transaction.Rollback();

                throw ex;
            }
        }

        public void UpdateLocalData(PARAMETER parameter, Boolean needsynchronize)
        {
            PARAMETER_BL parameterbl = new PARAMETER_BL();
            SYNCRONISM_BL syncronismbl = null;
            SYNCRONISM syncronism = null;
            SqliteTransaction transaction = null;

            try
            {
                if (needsynchronize)
                {
                    transaction = parameterbl.GetTransaction();

                    parameterbl.Update(transaction, parameter);

                    syncronismbl = new SYNCRONISM_BL();
                    syncronism = new SYNCRONISM();

                    syncronism.DATE_MODIFICATION = DateTime.Now;
                    syncronism.TABLENAME = "PARAMETER";
                    syncronism.OPERATION = 2;
                    syncronism.DATA = JsonConvert.SerializeObject(parameter);
                    syncronismbl.Insert(transaction, syncronism);
                }
                else
                    parameterbl.Update(parameter);
            }
            catch (Exception ex)
            {
                if (needsynchronize)
                    transaction.Rollback();

                throw ex;
            }
        }

        public void DeleteLocalData(PARAMETER parameter, Boolean needsynchronize)
        {
            PARAMETER_BL parameterbl = new PARAMETER_BL();
            SYNCRONISM_BL syncronismbl = null;
            SYNCRONISM syncronism = null;
            SqliteTransaction transaction = null;

            try
            {
                if (needsynchronize)
                {
                    transaction = parameterbl.GetTransaction();

                    parameterbl.Delete(transaction, parameter);

                    syncronismbl = new SYNCRONISM_BL();
                    syncronism = new SYNCRONISM();

                    syncronism.DATE_MODIFICATION = DateTime.Now;
                    syncronism.TABLENAME = "PARAMETER";
                    syncronism.OPERATION = 3;
                    syncronism.DATA = JsonConvert.SerializeObject(parameter);
                    syncronismbl.Insert(transaction, syncronism);
                }
                else
                    parameterbl.Delete(parameter);
            }
            catch (Exception ex)
            {
                if (needsynchronize)
                    transaction.Rollback();

                throw ex;
            }
        }

        public PARAMETERNG GetLocalData(PARAMETER parameter)
        {
            PARAMETER_BL parameterbl = new PARAMETER_BL();
            PARAMETERNG parameterng = new PARAMETERNG();

            parameterng.records = parameterbl.SelectList(parameter);
            parameterng.errors = new List<RETORNO>();

            return parameterng;
        }

        public PARAMETERNG GetAllLocalData(PARAMETER parameter)
        {
            PARAMETER_BL parameterbl = new PARAMETER_BL();
            PARAMETERNG parameterng = new PARAMETERNG();

            parameterng.records = parameterbl.SelectList(parameter);
            parameterng.errors = new List<RETORNO>();

            return parameterng;
        }
        #endregion
    }
}
