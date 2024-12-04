/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 05/04/2020 16:58:05
**********************************************************/

using SHL.Data.Base;
using SHL.Syncronism.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SHL.TimeSheet.Service.DataLayer
{
	public sealed partial class SYNCRONISM_DL : BaseDAL<SYNCRONISM>
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
		/// Select and return all records from table SYNCRONISM
		/// </summary>
		public Int32 SelectValidate(String property, String table, String value)
		{
			Int32 ret = Convert.ToInt32(SQLHelper.ExecuteScalar(GetSelectStringValidate(property, table, value)));

			return ret;
		}

		/// <summary>
		/// Select and return all records from table SYNCRONISM
		/// </summary>
		public List<SYNCRONISM> SelectList()
		{
			SYNCRONISM entity = new SYNCRONISM();

			return base.ReturnList(GetSelectString(), CreateParameters(entity, true));
		}

		/// <summary>
		/// Select a record with a filter
		/// </summary>
		public SYNCRONISM Select(SYNCRONISM entity)
		{
			return base.ReturnUnique(GetSelectString(), CreateParameters(entity, true));
		}

		/// <summary>
		/// Select some records with a filter
		/// </summary>
		public List<SYNCRONISM> SelectList(SYNCRONISM entity)
		{
			return base.ReturnList(GetSelectString(), CreateParameters(entity, true));
		}

		/// <summary>
		/// Insert a record in the table PARAMETER
		/// </summary>
		public void Insert(SYNCRONISM entity)
		{
			Insert(null, entity);
		}

		/// <summary>
		/// Insert a record in the table PARAMETER with transction
		/// </summary>
		public void Insert(SqliteTransaction oSqlTransaction, SYNCRONISM entity)
		{
			SQLHelper.ExecuteNonQuery(GetInsertString(), oSqlTransaction, CreateParameters(entity, false));
		}

		/// <summary>
		/// Update a record in the table SYNCRONISM
		/// </summary>
		public void Update(SYNCRONISM entity)
		{
			Update(null, entity);
		}

		/// <summary>
		/// Update a record in the table SYNCRONISM with transaction
		/// </summary>
		public void Update(SqliteTransaction oSqlTransaction, SYNCRONISM entity)
		{
			SQLHelper.ExecuteNonQuery(GetUpdateString(), oSqlTransaction, CreateParameters(entity, true));
		}

		/// <summary>
		/// Delete a record from table SYNCRONISM
		/// </summary>
		public void Delete(SYNCRONISM entity)
		{
			Delete(null, entity);
		}

		/// <summary>
		/// Delete a record from table SYNCRONISM with transaction
		/// </summary>
		public void Delete(SqliteTransaction oSqlTransaction, SYNCRONISM entity)
		{
			SQLHelper.ExecuteNonQuery(GetDeleteString(), oSqlTransaction, CreateParameters(entity, true));
		}

		/// <summary>
		/// Carrega os Parametros com ou sem ID
		/// </summary>
		private ArrayList CreateParameters(SYNCRONISM entity, bool pid)
		{
			ArrayList parameters = new ArrayList();

			SQLHelper.AddParameter(ref parameters, "@TABLENAME", entity.TABLENAME);
			SQLHelper.AddParameter(ref parameters, "@OPERATION", entity.OPERATION);
			SQLHelper.AddParameter(ref parameters, "@DATA", entity.DATA);
			SQLHelper.AddParameter(ref parameters, "@DATE_MODIFICATION", entity.DATE_MODIFICATION);

			return parameters;
		}

		private String GetInsertString()
		{
			String sql = String.Empty;

			sql = "INSERT INTO SYNCRONISM\n";
			sql += "(\n";
			sql += "	TABLENAME\n";
			sql += "	,OPERATION\n";
			sql += "	,DATA\n";
			sql += "	,DATE_MODIFICATION\n";
			sql += ")\n";
			sql += "VALUES\n";
			sql += "(\n";
			sql += "	 @TABLENAME\n";
			sql += "	,@OPERATION\n";
			sql += "	,@DATA\n";
			sql += "	,@DATE_MODIFICATION\n";
			sql += ")\n\n";

			return sql;
		}

		private String GetUpdateString()
		{
			String sql = String.Empty;

			sql = "UPDATE \n";
			sql += "	SYNCRONISM \n";
			sql += "SET \n";
			sql += "	DATA = @DATA\n";
			sql += "WHERE TABLENAME = @TABLENAME\n";
			sql += "  AND OPERATION = @OPERATION\n";
			sql += "  AND DATE_MODIFICATION = @DATE_MODIFICATION\n";

			return sql;
		}


		private String GetDeleteString()
		{
			String sql = String.Empty;

			sql = "DELETE FROM  \n";
			sql += "	SYNCRONISM \n";
			sql += "WHERE (TABLENAME = @TABLENAME OR @TABLENAME IS NULL)\n";
			sql += "  AND (OPERATION = @OPERATION OR @OPERATION IS NULL)\n";
			sql += "  AND (DATE_MODIFICATION = @DATE_MODIFICATION OR @DATE_MODIFICATION IS NULL)\n";

			return sql;
		}

		private String GetSelectString()
		{
			String sql = String.Empty;

			sql = "SELECT  \n";
			sql += "	TABLENAME,\n";
			sql += "	OPERATION,\n";
			sql += "	DATA,\n";
			sql += "	DATE_MODIFICATION\n";
			sql += "FROM   SYNCRONISM\n";
			sql += "WHERE 1=1\n";
			sql += "  AND (TABLENAME = @TABLENAME OR @TABLENAME IS NULL)\n";
			sql += "  AND (OPERATION = @OPERATION OR @OPERATION IS NULL)\n";
			sql += "  AND (DATE_MODIFICATION = @DATE_MODIFICATION OR @DATE_MODIFICATION IS NULL)\n";
			sql += "ORDER BY DATE_MODIFICATION \n";

			return sql;
		}
	}// Close out the class and namespace
}
