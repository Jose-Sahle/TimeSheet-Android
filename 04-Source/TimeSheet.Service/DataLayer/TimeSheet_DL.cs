/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 07/03/2019 21:26:05
**********************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SHL.TimeSheet.Model;
using Microsoft.Data.Sqlite;
using SHL.Data.Base;

namespace SHL.TimeSheet.DataLayer
{

	public sealed partial class TIMESHEET_DL : BaseDAL<TIMESHEET>
	{
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
		/// Select and return all records from table TIMESHEET
		/// </summary>
		public Int32 SelectValidate(String property, String table, String value)
		{
			Int32 ret = Convert.ToInt32(SQLHelper.ExecuteScalar(GetSelectStringValidate(property, table, value)));

			return ret;
		}

		/// <summary>
		/// Select and return all records from table TIMESHEET
		/// </summary>
		public List<TIMESHEET> SelectList()
		{
			TIMESHEET entity = new TIMESHEET();

			return base.ReturnList(GetSelectString(), CreateParameters(entity, true));
		}

		/// <summary>
		/// Select a record with a filter
		/// </summary>
		public TIMESHEET Select(TIMESHEET entity)
		{
			return base.ReturnUnique(GetSelectString(), CreateParameters(entity, true));
		}

		/// <summary>
		/// Select some records with a filter
		/// </summary>
		public List<TIMESHEET> SelectList(TIMESHEET entity)
		{
			return base.ReturnList(GetSelectString(), CreateParameters(entity, true));
		}

		/// <summary>
		/// Insert a record in the table TIMESHEET
		/// </summary>
		public void Insert(TIMESHEET entity)
		{
			Insert(null, entity);
		}

		/// <summary>
		/// Insert a record in the table TIMESHEET with transction
		/// </summary>
		public void Insert(SqliteTransaction oSqlTransaction, TIMESHEET entity)
		{
			SQLHelper.ExecuteNonQuery(GetInsertString(), oSqlTransaction, CreateParameters(entity, false));
		}

		/// <summary>
		/// Update a record in the table TIMESHEET
		/// </summary>
		public void Update(TIMESHEET entity)
		{
			Update(null, entity);
		}

		/// <summary>
		/// Update a record in the table TIMESHEET with transaction
		/// </summary>
		public void Update(SqliteTransaction oSqlTransaction, TIMESHEET entity)
		{
			SQLHelper.ExecuteNonQuery(GetUpdateString(), oSqlTransaction, CreateParameters(entity, true));
		}

		/// <summary>
		/// Delete a record from table TIMESHEET
		/// </summary>
		public void Delete(TIMESHEET entity)
		{
			Delete(null, entity);
		}

		/// <summary>
		/// Delete a record from table TIMESHEET with transaction
		/// </summary>
		public void Delete(SqliteTransaction oSqlTransaction, TIMESHEET entity)
		{
			SQLHelper.ExecuteNonQuery(GetDeleteString(), oSqlTransaction, CreateParameters(entity, true));
		}

		/// <summary>
		/// Carrega os Parametros com ou sem ID
		/// </summary>
		private ArrayList CreateParameters(TIMESHEET entity, bool pid)
		{
			ArrayList parameters = new ArrayList();

			SQLHelper.AddParameter(ref parameters, "@ID_TIMESHEET", entity.ID_TIMESHEET);
			SQLHelper.AddParameter(ref parameters, "@DATE_RG", entity.DATE_RG);
			SQLHelper.AddParameter(ref parameters, "@START_AM", entity.START_AM);
			SQLHelper.AddParameter(ref parameters, "@END_AM", entity.END_AM);
			SQLHelper.AddParameter(ref parameters, "@START_PM", entity.START_PM);
			SQLHelper.AddParameter(ref parameters, "@END_PM", entity.END_PM);
			SQLHelper.AddParameter(ref parameters, "@DESCRIPTION", entity.DESCRIPTION);

			return parameters;
		}

		private String GetInsertString()
		{
			String sql = String.Empty;

			sql = "INSERT INTO TIMESHEET\n";
			sql += "(\n";
			sql += "	 DATE_RG\n";
			sql += "	,START_AM\n";
			sql += "	,END_AM\n";
			sql += "	,START_PM\n";
			sql += "	,END_PM\n";
			sql += "	,DESCRIPTION\n";
			sql += ")\n";
			sql += "VALUES\n";
			sql += "(\n";
			sql += "	 @DATE_RG\n";
			sql += "	,@START_AM\n";
			sql += "	,@END_AM\n";
			sql += "	,@START_PM\n";
			sql += "	,@END_PM\n";
			sql += "	,@DESCRIPTION\n";
			sql += ")";

			return sql;
		}

		private String GetUpdateString()
		{
			String sql = String.Empty;

			sql = "UPDATE \n";
			sql += "	TIMESHEET \n";
			sql += "SET \n";
			sql += "	 DATE_RG = @DATE_RG\n";
			sql += "	,START_AM = @START_AM\n";
			sql += "	,END_AM = @END_AM\n";
			sql += "	,START_PM = @START_PM\n";
			sql += "	,END_PM = @END_PM\n";
			sql += "	,DESCRIPTION = @DESCRIPTION\n";
			sql += "WHERE 1 = 1\n";
			sql += "  AND (ID_TIMESHEET = @ID_TIMESHEET OR @ID_TIMESHEET IS NULL)\n";
			sql += "  AND (DATE_RG = @DATE_RG OR @DATE_RG IS NULL)\n";

			return sql;
		}


		private String GetDeleteString()
		{
			String sql = String.Empty;

			sql = "DELETE FROM  \n";
			sql += "	TIMESHEET \n";
			sql += "WHERE 1 = 1\n";
			sql += "  AND (ID_TIMESHEET = @ID_TIMESHEET OR @ID_TIMESHEET ISNULL)\n";

			return sql;
		}

		private String GetSelectString()
		{
			String sql = String.Empty;

			sql = "SELECT  \n";
			sql += "	ID_TIMESHEET,\n";
			sql += "	DATE_RG,\n";
			sql += "	START_AM,\n";
			sql += "	END_AM,\n";
			sql += "	START_PM,\n";
			sql += "	END_PM,\n";
			sql += "	DESCRIPTION\n";
			sql += "FROM   TIMESHEET \n";
			sql += "WHERE 1 = 1\n";
			sql += "  AND (ID_TIMESHEET = @ID_TIMESHEET OR @ID_TIMESHEET IS NULL)\n";
			sql += "  AND (DATE_RG = @DATE_RG OR @DATE_RG IS NULL)\n";
			
			return sql;
		}
	}// Close out the class and namespace
}
