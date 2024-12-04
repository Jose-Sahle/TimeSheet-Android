using System;
using System.Collections.Generic;
using System.Text;
using SHL.Utils;

namespace SHL.TimeSheet
{
    static class GlobalData
    {
        private static WHEREDATA m_wheredatabase;
        private static String m_urlremotedata;

        public static WHEREDATA WhereDatabase
        {
            get { return m_wheredatabase; }
            set { m_wheredatabase = value; }
        }

        public static String URLRemoteData
        {
            get { return m_urlremotedata; }
            set { m_urlremotedata = value; }
        }
    }
}
