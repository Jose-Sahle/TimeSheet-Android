using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SHL.IRetorno.Model;
using SHL.ITimeSheetNG.Model;
using SHL.Settings.BusinessLayer;
using SHL.Settings.Model;
using SHL.Syncronism.BusinessLayer;
using SHL.Syncronism.Model;
using SHL.TimeSheet.BusinessLayer;
using SHL.TimeSheet.Model;
using SHL.TokenSecurity;
using SHL.Utils;
using Microsoft.Data.Sqlite;
using SHL.Data.Base;
using SHL.Task.BusinessLayer;
using SHL.Project.BusinessLayer;
using SHL.Project.Model;
using SHL.Task.Model;

namespace SHL.TimeSheet.Service
{
    public class DataServiceTimeSheet
    {
        #region [Constructors]
        public DataServiceTimeSheet()
        {
            SETTINGS settings = null;
            SETTINGS_BL settingsbl = new SETTINGS_BL();

            settings = new SETTINGS();
            settings.KEY = "url_timesheet";
            settings = settingsbl.Select(settings);
            if (settings != null)
                TimeSheetUrl = settings.VALUE;

            settings = new SETTINGS();
            settings.KEY = "url_tokensecurity";
            settings = settingsbl.Select(settings);
            if (settings != null)
                TokeSecurityUrl = settings.VALUE;
        }
        #endregion

        #region [Private Members]
        String m_url_timesheet = "http://10.0.2.2:5000/api/";
        String m_url_tokensecurity = "http://10.0.2.2:5000/api/";
        #endregion

        #region [Properties]
        public String TimeSheetUrl
        {
            get { return m_url_timesheet; }
            set { m_url_timesheet = value; }
        }

        public String TokeSecurityUrl
        {
            get { return m_url_tokensecurity; }
            set { m_url_tokensecurity = value; }
        }
        #endregion

        #region [Actions]
        public void AddData(WHEREDATA wheredata, TIMESHEET timesheet)
        {
            switch(wheredata)
            {
                case WHEREDATA.REMOTE:
                    AddRemoteData(timesheet);
                    AddLocalData(timesheet, false);
                    break;
                case WHEREDATA.LOCAL:
                    AddLocalData(timesheet, true);
                    break;
            }                          
        }

        public void UpdateData(WHEREDATA wheredata, TIMESHEET timesheet)
        {
            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    UpdateRemoteData(timesheet);
                    UpdateLocalData(timesheet, false);
                    break;
                case WHEREDATA.LOCAL:
                    UpdateLocalData(timesheet, true);
                    break;
            }
        }

        public void DeleteData(WHEREDATA wheredata, TIMESHEET timesheet)
        {
            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    DeleteRemoteData(timesheet);
                    DeleteLocalData(timesheet, false);
                    break;
                case WHEREDATA.LOCAL:
                    DeleteLocalData(timesheet, true);
                    break;
            }                        
        }

        public TIMESHEETNG GetData(WHEREDATA wheredata, DateTime date)
        {
            TIMESHEETNG ret = null;

            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    ret = GetRemoteData(date);
                    break;
                case WHEREDATA.LOCAL:
                    ret = GetLocalData(date);
                    break;
            }

            return ret;
        }

        public TIMESHEETNG GetAllData(WHEREDATA wheredata, DateTime dateini, DateTime dateend)
        {
            TIMESHEETNG ret = null;

            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    ret = GetAllRemoteData(dateini, dateend);
                    break;
                case WHEREDATA.LOCAL:
                    ret = GetAllLocalData(dateini, dateend);
                    break;
            }

            return ret;
        }
        #endregion

        #region [Remote]
        public void AddRemoteData(TIMESHEET timesheet)
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

                Util.ControllerPostExT<List<RETORNO>>(true, m_url_timesheet, "timesheetng", "insert", timesheet, "tokensecurity", tokensecurity);
            }
            catch 
            {
                AddLocalData(timesheet, true);
            }
        }

        public void UpdateRemoteData(TIMESHEET timesheet)
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

                Util.ControllerPutExT<List<RETORNO>>(m_url_timesheet, "timesheetng", "update", timesheet, "tokensecurity", tokensecurity);
            }
            catch(Exception ex) 
            {
                UpdateLocalData(timesheet, true);
                TurnOnline(false);
                throw ex;
            }
        }

        public void DeleteRemoteData(TIMESHEET timesheet)
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

                Util.ControllerDeleteExT<List<RETORNO>>(m_url_timesheet, "timesheetng", "delete", timesheet, "tokensecurity", tokensecurity);
            }
            catch (Exception ex)
            {
                DeleteLocalData(timesheet, true);
                TurnOnline(false);
                throw ex;
            }
        }

        public TIMESHEETNG GetRemoteData(DateTime date)
        {
            TIMESHEET timesheet = null;
            TIMESHEETNG timesheetng = null;
            String hash = String.Empty;
            String tokensecurity = String.Empty;
            Int32 repeatedcount = 0;

            try
            {
                timesheet = new TIMESHEET();
                timesheet.DATE_RG = date;

                while (tokensecurity == String.Empty && repeatedcount < 5)
                {
                    hash = TokenKey.GetCryptMessage("SHL", "/api/TimeSheetNG/Select");

                    tokensecurity = (String)(Util.ControllerSelectEx<String>(m_url_tokensecurity, "TokenSecurity", "GetTokenSecurity", "key", hash));

                    repeatedcount++;
                }

                timesheetng = (TIMESHEETNG)(Util.ControllerPostExT<TIMESHEETNG>(true, m_url_timesheet, "timesheetng", "select", timesheet, "tokensecurity", tokensecurity));

            }
            catch 
            {
                GetLocalData(date);
                TurnOnline(false);
            }

            return timesheetng;
        }

        public TIMESHEETNG GetAllRemoteData(DateTime? dateini, DateTime? dateend)
        {
            TIMESHEET timesheet = null;
            TIMESHEETNG timesheetng = null;
            Int32 repeatedcount = 0;

            String hash = String.Empty;
            String tokensecurity = String.Empty;

            try
            {
                timesheet = new TIMESHEET();
                timesheet.DATE_RG = dateini;
                timesheet.DATE_RG_END = dateend;

                while (tokensecurity == String.Empty && repeatedcount < 5)
                {
                    hash = TokenKey.GetCryptMessage("SHL", "/api/TimeSheetNG/SelectAll");

                    tokensecurity = (String)(Util.ControllerSelectEx<String>(m_url_tokensecurity, "TokenSecurity", "GetTokenSecurity", "key", hash));

                    repeatedcount++;
                }

                timesheetng = (TIMESHEETNG)(Util.ControllerSelectEx<TIMESHEETNG>(m_url_timesheet, "timesheetng", "selectall", "tokensecurity", tokensecurity));

            }
            catch 
            {
                GetAllLocalData(dateini, dateend);
                TurnOnline(false);
            }

            return timesheetng;
        }
        #endregion

        #region [Local]
        public void AddLocalData(TIMESHEET timesheet, Boolean needsynchronize)
        {
            TIMESHEET_BL timesheetbl = new TIMESHEET_BL();
            SYNCRONISM_BL syncronismbl = null;
            TASK_BL taskbl = new TASK_BL();
            PROJECT_BL projectbl = new PROJECT_BL();
            PROJECT project = null;

            SYNCRONISM syncronism = null;
            SqliteTransaction transaction = null;
            String msgerror = String.Empty;
            Int32 id_timesheet;

            try
            {
                transaction = timesheetbl.GetTransaction();

                if (needsynchronize)
                {
                    syncronismbl = new SYNCRONISM_BL();
                    syncronism = new SYNCRONISM();

                    syncronism.DATE_MODIFICATION = DateTime.Now;
                    syncronism.TABLENAME = "TIMESHEET";
                    syncronism.OPERATION = 1;
                    syncronism.DATA = JsonConvert.SerializeObject(timesheet);
                    syncronismbl.Insert(transaction, syncronism);
                }

                timesheetbl.Insert(timesheet, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                throw ex;
            }
        }

        public void UpdateLocalData(TIMESHEET timesheet, Boolean needsynchronize)
        {
            TIMESHEET_BL timesheetbl = new TIMESHEET_BL();
            SYNCRONISM_BL syncronismbl = null;
            SYNCRONISM syncronism = null;
            SqliteTransaction transaction = null;

            try
            {
                transaction = timesheetbl.GetTransaction();
                timesheetbl.Update(transaction, timesheet);

                if (needsynchronize)
                {
                    syncronismbl = new SYNCRONISM_BL();
                    syncronism = new SYNCRONISM();

                    syncronism.DATE_MODIFICATION = DateTime.Now;
                    syncronism.TABLENAME = "TIMESHEET";
                    syncronism.OPERATION = 2;
                    syncronism.DATA = JsonConvert.SerializeObject(timesheet);
                    syncronismbl.Insert(transaction, syncronism);

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                throw ex;
            }
        }

        public void DeleteLocalData(TIMESHEET timesheet, Boolean needsynchronize)
        {
            TIMESHEET_BL timesheetbl = new TIMESHEET_BL();
            SYNCRONISM_BL syncronismbl = null;
            SYNCRONISM syncronism = null;
            SqliteTransaction transaction = null;

            try
            {
                transaction = timesheetbl.GetTransaction();
                timesheetbl.Delete(transaction, timesheet);

                if (needsynchronize)
                {
                    syncronismbl = new SYNCRONISM_BL();
                    syncronism = new SYNCRONISM();

                    syncronism.DATE_MODIFICATION = DateTime.Now;
                    syncronism.TABLENAME = "TIMESHEET";
                    syncronism.OPERATION = 3;
                    syncronism.DATA = JsonConvert.SerializeObject(timesheet);
                    syncronismbl.Insert(transaction, syncronism);

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                throw ex;
            }
        }

        public TIMESHEETNG GetLocalData(DateTime date)
        {
            TIMESHEET timesheet = new TIMESHEET();            
            TIMESHEET_BL timesheetbl = new TIMESHEET_BL();
            TIMESHEETNG timesheetng = new TIMESHEETNG();

            timesheet.DATE_RG = date;
            timesheetng.records = timesheetbl.SelectList(timesheet);
            timesheetng.errors = new List<RETORNO>();

            return timesheetng;
        }

        public TIMESHEETNG GetAllLocalData(DateTime? dateini, DateTime? dataend)
        {
            TIMESHEET timesheet = new TIMESHEET();
            TIMESHEET_BL timesheetbl = new TIMESHEET_BL();
            TIMESHEETNG timesheetng = new TIMESHEETNG();

            timesheet.DATE_RG = dateini;
            timesheet.DATE_RG_END = dataend;
            timesheetng.records = timesheetbl.SelectList(timesheet);
            timesheetng.errors = new List<RETORNO>();

            return timesheetng;
        }

        private void TurnOnline(Boolean isonline)
        {
            SETTINGS settings = new SETTINGS();
            SETTINGS_BL settingsbl = new SETTINGS_BL();

            try
            {
                settings.KEY = "WHEREDATA";
                if (isonline)
                    settings.VALUE = "ONLINE";
                else
                    settings.VALUE = "OFFLINE";

                settingsbl.Update(settings);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
