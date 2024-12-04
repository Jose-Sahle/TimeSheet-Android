/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 28/03/2020 13:45:05
**********************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SHL.Project.Model;
using Microsoft.Data.Sqlite;
using SHL.Data.Base;

namespace SHL.Project.DataLayer
{
	public sealed partial class PROJECT_DL : BaseDAL<PROJECT>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="property"></param>
		/// <param name="table"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		private String GetSelectStringValidate(String property, String table, String value)
		{
			StringBuilder sql = new StringBuilder();

			sql.Append("SELECT COUNT(1) AS 'COUNT'");
			sql.Append(" FROM ");
			sql.Append(table.ToUpper());
			sql.Append(" WHERE ");
			sql.Append(property.ToUpper() + " = '" + value + "'");

			return sql.ToString();
		}

		/// <summary>
		/// Select and return all records from table PROJECT
		/// </summary>
		public Int32 SelectValidate(String property, String table, String value)
		{
			Int32 ret = Convert.ToInt32(SQLHelper.ExecuteScalar(GetSelectStringValidate(property, table, value)));

			return ret;
		}

		/// <summary>
		/// Select and return all records from table PROJECT
		/// </summary>
		public List<PROJECT> SelectList()
		{
			PROJECT entity = new PROJECT();

			return base.ReturnList(GetSelectString(), CreateParameters(entity, true));
		}

		/// <summary>
		/// Select a record with a filter
		/// </summary>
		public PROJECT Select(PROJECT entity)
		{
			return base.ReturnUnique(GetSelectString(), CreateParameters(entity, true));
		}

		/// <summary>
		/// Select some records with a filter
		/// </summary>
		public List<PROJECT> SelectList(PROJECT entity)
		{
			return base.ReturnList(GetSelectString(), CreateParameters(entity, true));
		}

		/// <summary>
		/// Insert a record in the table PARAMETER
		/// </summary>
		public void Insert(PROJECT entity)
		{
			Insert(null, entity);
		}

		/// <summary>
		/// Insert a record in the table PARAMETER with transction
		/// </summary>
		public void Insert(SqliteTransaction oSqlTransaction, PROJECT entity)
		{
			SQLHelper.ExecuteNonQuery(GetInsertString(), oSqlTransaction, CreateParameters(entity, false));
		}

		/// <summary>
		/// Update a record in the table PROJECT
		/// </summary>
		public void Update(PROJECT entity)
		{
			Update(null, entity);
		}

		/// <summary>
		/// Update a record in the table PROJECT with transaction
		/// </summary>
		public void Update(SqliteTransaction oSqlTransaction, PROJECT entity)
		{
			SQLHelper.ExecuteNonQuery(GetUpdateString(), oSqlTransaction, CreateParameters(entity, true));
		}

		/// <summary>
		/// Delete a record from table PROJECT
		/// </summary>
		public void Delete(PROJECT entity)
		{
			Delete(null, entity);
		}

		/// <summary>
		/// Delete a record from table PROJECT with transaction
		/// </summary>
		public void Delete(SqliteTransaction oSqlTransaction, PROJECT entity)
		{
			SQLHelper.ExecuteNonQuery(GetDeleteString(), oSqlTransaction, CreateParameters(entity, true));
		}

		/// <summary>
		/// Carrega os Parametros com ou sem ID
		/// </summary>
		private ArrayList CreateParameters(PROJECT entity, bool pid)
		{
			ArrayList parameters = new ArrayList();

			SQLHelper.AddParameter(ref parameters, "@ID_PROJECT", entity.ID_PROJECT);
			SQLHelper.AddParameter(ref parameters, "@START_DT", entity.START_DT);
			SQLHelper.AddParameter(ref parameters, "@END_DT", entity.END_DT);
			SQLHelper.AddParameter(ref parameters, "@NAME", entity.NAME);
			SQLHelper.AddParameter(ref parameters, "@ALIAS", entity.ALIAS);
			SQLHelper.AddParameter(ref parameters, "@DESCRIPTION", entity.DESCRIPTION);

			return parameters;
		}

		private String GetInsertString()
		{
			String sql = String.Empty;

			sql = "INSERT INTO PROJECT\n";
			sql += "(\n";
			sql += "	START_DT\n";
			sql += "	,END_DT\n";
			sql += "	,NAME\n";
			sql += "	,ALIAS\n";
			sql += "	,DESCRIPTION\n";
			sql += ")\n";
			sql += "VALUES\n";
			sql += "(\n";
			sql += "	@START_DT\n";
			sql += "	,@END_DT\n";
			sql += "	,@NAME\n";
			sql += "	,@ALIAS\n";
			sql += "	,@DESCRIPTION\n";
			sql += ")\n\n";

			return sql;
		}

		private String GetUpdateString()
		{
			String sql = String.Empty;

			sql = "UPDATE \n";
			sql += "	PROJECT \n";
			sql += "SET \n";
			sql += "	START_DT = @START_DT\n";
			sql += "	,END_DT = @END_DT\n";
			sql += "	,NAME = @NAME\n";
			sql += "	,ALIAS = @ALIAS\n";
			sql += "	,DESCRIPTION = @DESCRIPTION\n";
			sql += "WHERE ID_PROJECT = @ID_PROJECT\n";

			return sql;
		}


		private String GetDeleteString()
		{
			String sql = String.Empty;

			sql = "DELETE FROM  \n";
			sql += "	PROJECT \n";
			sql += "WHERE (ID_PROJECT = @ID_PROJECT OR @ID_PROJECT IS NULL)\n";

			return sql;
		}

		private String GetSelectString()
		{
			String sql = String.Empty;

			sql = "SELECT  \n";
			sql += "	ID_PROJECT,\n";
			sql += "	START_DT,\n";
			sql += "	END_DT,\n";
			sql += "	NAME,\n";
			sql += "	ALIAS,\n";
			sql += "	DESCRIPTION\n";
			sql += "FROM   PROJECT  \n";
			sql += "WHERE 1 = 1\n";
			sql += "  AND (ID_PROJECT = @ID_PROJECT OR @ID_PROJECT IS NULL)\n";
			sql += "  AND (START_DT = @START_DT OR @START_DT IS NULL)\n";
			sql += "  AND (END_DT = @END_DT OR @END_DT IS NULL)\n";
			sql += "  AND (NAME = @NAME OR @NAME IS NULL)\n";
			sql += "  AND (ALIAS = @ALIAS OR @ALIAS IS NULL)\n";

			return sql;
		}
	}// Close out the class and namespace
}
