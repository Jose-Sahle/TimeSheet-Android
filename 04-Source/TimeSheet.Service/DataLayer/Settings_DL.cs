using System;
using System.Collections.Generic;
using System.Text;
using SHL.Settings.Model;
using Microsoft.Data.Sqlite;
using System.Collections;
using SHL.Data.Base;

namespace SHL.Settings.DataLayer
{
    public sealed partial class SETTINGS_DL : BaseDAL<SETTINGS>
    {
		public List<SETTINGS> SelectList()
		{
			SETTINGS entity = new SETTINGS();

			return base.ReturnList(GetSelectString(), CreateParameters(entity, true));
		}

		public SETTINGS Select(SETTINGS entity)
		{
			return base.ReturnUnique(GetSelectString(), CreateParameters(entity, true));
		}

		public List<SETTINGS> SelectList(SETTINGS entity)
		{
			return base.ReturnList(GetSelectString(), CreateParameters(entity, true));
		}

		public void Insert(SETTINGS entity)
		{
			SQLHelper.ExecuteNonQuery(GetInsertString(), CreateParameters(entity, false));
		}

		public void Insert(SqliteTransaction oSqlTransaction, SETTINGS entity)
		{
			SQLHelper.ExecuteNonQuery(GetInsertString(), oSqlTransaction, CreateParameters(entity, false));
		}

		public void Update(SETTINGS entity)
		{
			SQLHelper.ExecuteNonQuery(GetUpdateString(), CreateParameters(entity, true));
		}

		public void Update(SqliteTransaction oSqlTransaction, SETTINGS entity)
		{
			SQLHelper.ExecuteNonQuery(GetUpdateString(), oSqlTransaction, CreateParameters(entity, true));
		}

		public void Delete(SETTINGS entity)
		{
			SQLHelper.ExecuteNonQuery(GetDeleteString(), CreateParameters(entity, true));
		}

		public void Delete(SqliteTransaction oSqlTransaction, SETTINGS entity)
		{
			SQLHelper.ExecuteNonQuery(GetDeleteString(), oSqlTransaction, CreateParameters(entity, true));
		}

		private ArrayList CreateParameters(SETTINGS entity, bool pid)
		{
			ArrayList parameters = new ArrayList();

			SQLHelper.AddParameter(ref parameters, "@KEY", entity.KEY);
			SQLHelper.AddParameter(ref parameters, "@VALUE", entity.VALUE);

			return parameters;
		}

		private String GetInsertString()
		{
			String sql = String.Empty;

			sql = "INSERT INTO SETTINGS\n";
			sql += "(\n";
			sql += "	 KEY\n";
			sql += "	,VALUE\n";
			sql += ")\n";
			sql += "VALUES\n";
			sql += "(\n";
			sql += "	 @KEY\n";
			sql += "	,@VALUE\n";
			sql += ")";

			return sql;
		}

		private String GetUpdateString()
		{
			String sql = String.Empty;

			sql = "UPDATE \n";
			sql += "	SETTINGS \n";
			sql += "SET \n";
			sql += "	VALUE = @VALUE\n";
			sql += "WHERE 1 = 1\n";
			sql += "  AND (KEY = @KEY OR @KEY IS NULL)\n";

			return sql;
		}

		private String GetDeleteString()
		{
			String sql = String.Empty;

			sql = "DELETE FROM  \n";
			sql += "	SETTINS \n";
			sql += "WHERE 1 = 1\n";
			sql += "  AND (KEY = @KEY)\n";

			return sql;
		}

		private String GetSelectString()
		{
			String sql = String.Empty;

			sql = "SELECT  \n";
			sql += "	KEY,\n";
			sql += "	VALUE\n";
			sql += "FROM   SETTINGS \n";
			sql += "WHERE 1 = 1\n";
			sql += "  AND (KEY = @KEY OR @KEY IS NULL)\n";
			sql += "  AND (VALUE = @VALUE OR @VALUE IS NULL)\n";

			return sql;
		}
	}
}
