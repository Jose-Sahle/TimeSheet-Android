using Newtonsoft.Json;
using SHL.IRetorno.Model;
using SHL.ITimeSheetNG.Model;
using SHL.Project.BusinessLayer;
using SHL.Project.Model;
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
    public class DataServiceProject
    {
        #region [Constructors]
        public DataServiceProject()
        {
            SETTINGS settings = null;
            SETTINGS_BL settingsbl = new SETTINGS_BL();

            settings = new SETTINGS();
            settings.KEY = "url_timesheet";
            settings = settingsbl.Select(settings);
            if (settings != null)
                ProjectUrl = settings.VALUE;

            settings = new SETTINGS();
            settings.KEY = "url_tokensecurity";
            settings = settingsbl.Select(settings);
            if (settings != null)
                TokeSecurityUrl = settings.VALUE;
        }
        #endregion

        #region [Private Members]
        String m_url_project = "http://10.0.2.2:5000/api/";
        String m_url_tokensecurity = "http://10.0.2.2:5000/api/";
        #endregion

        #region [Properties]
        public String ProjectUrl
        {
            get { return m_url_project; }
            set { m_url_project = value; }
        }

        public String TokeSecurityUrl
        {
            get { return m_url_tokensecurity; }
            set { m_url_tokensecurity = value; }
        }
        #endregion

        #region [Actions]
        public void AddData(WHEREDATA wheredata, PROJECT project)
        {
            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    AddRemoteData(project);
                    AddLocalData(project, false);
                    break;
                case WHEREDATA.LOCAL:
                    AddLocalData(project, true);
                    break;
            }
        }

        public void UpdateData(WHEREDATA wheredata, PROJECT project)
        {
            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    UpdateRemoteData(project);
                    UpdateLocalData(project, false);
                    break;
                case WHEREDATA.LOCAL:
                    UpdateLocalData(project, true);
                    break;
            }
        }

        public void DeleteData(WHEREDATA wheredata, PROJECT project)
        {
            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    DeleteRemoteData(project);
                    DeleteLocalData(project, false);
                    break;
                case WHEREDATA.LOCAL:
                    DeleteLocalData(project, true);
                    break;
            }
        }

        public PROJECTNG GetData(WHEREDATA wheredata, PROJECT project)
        {
            PROJECTNG ret = null;

            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    ret = GetRemoteData(project);
                    break;
                case WHEREDATA.LOCAL:
                    ret = GetLocalData(project);
                    break;
            }

            return ret;
        }

        public PROJECTNG GetAllData(WHEREDATA wheredata, PROJECT project)
        {
            PROJECTNG ret = null;

            switch (wheredata)
            {
                case WHEREDATA.REMOTE:
                    ret = GetAllRemoteData(project);
                    break;
                case WHEREDATA.LOCAL:
                    ret = GetAllLocalData(project);
                    break;
            }

            return ret;
        }
        #endregion

        #region [Remote]
        public void AddRemoteData(PROJECT project)
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

                Util.ControllerPostExT<List<RETORNO>>(true, m_url_project, "projectng", "insert", project, "tokensecurity", tokensecurity);
            }
            catch
            {
                AddLocalData(project, true);
            }
        }

        public void UpdateRemoteData(PROJECT project)
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

                Util.ControllerPutExT<List<RETORNO>>(m_url_project, "projectng", "update", project, "tokensecurity", tokensecurity);
            }
            catch
            {
                UpdateLocalData(project, true);
            }
        }

        public void DeleteRemoteData(PROJECT project)
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

                Util.ControllerDeleteExT<List<RETORNO>>(m_url_project, "projectng", "delete", project, "tokensecurity", tokensecurity);
            }
            catch
            {
                DeleteLocalData(project, true);
            }
        }

        public PROJECTNG GetRemoteData(PROJECT project)
        {
            PROJECTNG projectng = null;
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

                projectng = (PROJECTNG)(Util.ControllerPostExT<PROJECTNG>(true, m_url_project, "projectng", "select", project, "tokensecurity", tokensecurity));

            }
            catch
            {
                GetLocalData(project);
            }

            return projectng;
        }

        public PROJECTNG GetAllRemoteData(PROJECT project)
        {
            PROJECTNG projectng = null;
            Int32 repeatedcount = 0;

            String hash = String.Empty;
            String tokensecurity = String.Empty;

            try
            {
                while (tokensecurity == String.Empty && repeatedcount < 5)
                {
                    hash = TokenKey.GetCryptMessage("SHL", "/api/TimeSheetNG/SelectAll");

                    tokensecurity = (String)(Util.ControllerSelectEx<String>(m_url_tokensecurity, "TokenSecurity", "GetTokenSecurity", "key", hash));

                    repeatedcount++;
                }

                projectng = (PROJECTNG)(Util.ControllerSelectEx<PROJECTNG>(m_url_project, "projectng", "selectall", "tokensecurity", tokensecurity));
            }
            catch
            {
                GetAllLocalData(project);
            }

            return projectng;
        }
        #endregion

        #region [Local]
        public void AddLocalData(PROJECT project, Boolean needsynchronize)
        {
            PROJECT_BL projectbl = new PROJECT_BL();
            SYNCRONISM_BL syncronismbl = null;
            SYNCRONISM syncronism = null;
            SqliteTransaction transaction = null;         

            try
            {
                if (needsynchronize)
                {
                    transaction = projectbl.GetTransaction();

                    projectbl.Insert(transaction, project);

                    syncronismbl = new SYNCRONISM_BL();
                    syncronism = new SYNCRONISM();

                    syncronism.DATE_MODIFICATION = DateTime.Now;
                    syncronism.TABLENAME = "PROJECT";
                    syncronism.OPERATION = 1;
                    syncronism.DATA = JsonConvert.SerializeObject(project);
                    syncronismbl.Insert(transaction, syncronism);
                }
                else
                    projectbl.Insert(project);
            }
            catch (Exception ex)
            {
                if (needsynchronize)
                    transaction.Rollback();

                throw ex;
            }
        }

        public void UpdateLocalData(PROJECT project, Boolean needsynchronize)
        {
            PROJECT_BL projectbl = new PROJECT_BL();
            SYNCRONISM_BL syncronismbl = null;
            SYNCRONISM syncronism = null;
            SqliteTransaction transaction = null;

            try
            {
                if (needsynchronize)
                {
                    transaction = projectbl.GetTransaction();

                    projectbl.Update(transaction, project);

                    syncronismbl = new SYNCRONISM_BL();
                    syncronism = new SYNCRONISM();

                    syncronism.DATE_MODIFICATION = DateTime.Now;
                    syncronism.TABLENAME = "PROJECT";
                    syncronism.OPERATION = 2;
                    syncronism.DATA = JsonConvert.SerializeObject(project);
                    syncronismbl.Insert(transaction, syncronism);
                }
                else
                    projectbl.Update(project);
            }
            catch (Exception ex)
            {
                if (needsynchronize)
                    transaction.Rollback();

                throw ex;
            }
        }

        public void DeleteLocalData(PROJECT project, Boolean needsynchronize)
        {
            PROJECT_BL projectbl = new PROJECT_BL();
            SYNCRONISM_BL syncronismbl = null;
            SYNCRONISM syncronism = null;
            SqliteTransaction transaction = null;

            try
            {
                if (needsynchronize)
                {
                    transaction = projectbl.GetTransaction();

                    projectbl.Delete(transaction, project);

                    syncronismbl = new SYNCRONISM_BL();
                    syncronism = new SYNCRONISM();

                    syncronism.DATE_MODIFICATION = DateTime.Now;
                    syncronism.TABLENAME = "PROJECT";
                    syncronism.OPERATION = 3;
                    syncronism.DATA = JsonConvert.SerializeObject(project);
                    syncronismbl.Insert(transaction, syncronism);
                }
                else
                    projectbl.Delete(project);
            }
            catch (Exception ex)
            {
                if (needsynchronize)
                    transaction.Rollback();

                throw ex;
            }
        }

        public PROJECTNG GetLocalData(PROJECT project)
        {
            PROJECT_BL projectbl = new PROJECT_BL();
            PROJECTNG projectng = new PROJECTNG();

            projectng.records = projectbl.SelectList(project);
            projectng.errors = new List<RETORNO>();

            return projectng;
        }

        public PROJECTNG GetAllLocalData(PROJECT project)
        {
            PROJECT_BL projectbl = new PROJECT_BL();
            PROJECTNG projectng = new PROJECTNG();

            projectng.records = projectbl.SelectList(project);
            projectng.errors = new List<RETORNO>();

            return projectng;
        }
        #endregion
    }
}
