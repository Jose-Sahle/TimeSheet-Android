/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 28/03/2020 13:45:05
**********************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SHL.Parameter.Model;
using Microsoft.Data.Sqlite;
using SHL.Data.Base;

namespace SHL.Parameter.DataLayer
{

	public sealed partial class PARAMETER_DL : BaseDAL<PARAMETER>
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
		/// Select and return all records from table PARAMETER
		/// </summary>
		public Int32 SelectValidate(String property, String table, String value)
		{
			Int32 ret = Convert.ToInt32(SQLHelper.ExecuteScalar(GetSelectStringValidate(property, table, value)));

			return ret;
		}

		/// <summary>
		/// Select and return all records from table PARAMETER
		/// </summary>
		public List<PARAMETER> SelectList()
		{
			PARAMETER entity = new PARAMETER();

			return base.ReturnList(GetSelectString(), CreateParameters(entity));
		}

		/// <summary>
		/// Select a record with a filter
		/// </summary>
		public PARAMETER Select(PARAMETER entity)
		{
			return base.ReturnUnique(GetSelectString(), CreateParameters(entity));
		}

		/// <summary>
		/// Select some records with a filter
		/// </summary>
		public List<PARAMETER> SelectList(PARAMETER entity)
		{
			return base.ReturnList(GetSelectString(), CreateParameters(entity));
		}

		/// <summary>
		/// Insert a record in the table PARAMETER
		/// </summary>
		public void Insert(PARAMETER entity)
		{
			Insert(null, entity);
		}

		/// <summary>
		/// Insert a record in the table PARAMETER with transction
		/// </summary>
		public void Insert(SqliteTransaction oSqlTransaction, PARAMETER entity)
		{
			SQLHelper.ExecuteNonQuery(GetInsertString(), oSqlTransaction, CreateParameters(entity));
		}

		/// <summary>
		/// Update a record in the table PARAMETER
		/// </summary>
		public void Update(PARAMETER entity)
		{
			Update(null, entity);
		}

		/// <summary>
		/// Update a record in the table PARAMETER with transaction
		/// </summary>
		public void Update(SqliteTransaction oSqlTransaction, PARAMETER entity)
		{
			SQLHelper.ExecuteNonQuery(GetUpdateString(), oSqlTransaction, CreateParameters(entity));
		}

		/// <summary>
		/// Delete a record from table PARAMETER
		/// </summary>
		public void Delete(PARAMETER entity)
		{
			Delete(null, entity);
		}

		/// <summary>
		/// Delete a record from table PARAMETER with transaction
		/// </summary>
		public void Delete(SqliteTransaction oSqlTransaction, PARAMETER entity)
		{
			SQLHelper.ExecuteNonQuery(GetDeleteString(), oSqlTransaction, CreateParameters(entity));
		}

		/// <summary>
		/// Carrega os Parametros com ou sem ID
		/// </summary>
		private ArrayList CreateParameters(PARAMETER entity)
		{
			ArrayList parameters = new ArrayList();

			SQLHelper.AddParameter(ref parameters, "@KEY", entity.KEY);
			SQLHelper.AddParameter(ref parameters, "@VALUE", entity.VALUE);

			return parameters;
		}

		private String GetInsertString()
		{
			String sql = String.Empty;

			sql = "INSERT INTO PARAMETER\n";
			sql += "(\n";
			sql += "	 KEY\n";
			sql += "	,VALUE\n";
			sql += ")\n";
			sql += "VALUES\n";
			sql += "(\n";
			sql += "	 @KEY\n";
			sql += "	,@VALUE\n";
			sql += ")\n\n";

			return sql;
		}

		private String GetUpdateString()
		{
			String sql = String.Empty;

			sql = "UPDATE \n";
			sql += "	PARAMETER \n";
			sql += "SET \n";
			sql += "	VALUE = @VALUE\n";
			sql += "WHERE KEY = @KEY\n";

			return sql;
		}


		private String GetDeleteString()
		{
			String sql = String.Empty;

			sql = "DELETE FROM  \n";
			sql += "	PARAMETER \n";
			sql += "WHERE KEY = @KEY\n";

			return sql;
		}

		private String GetSelectString()
		{
			String sql = String.Empty;

			sql = "SELECT  \n";
			sql += "	KEY,\n";
			sql += "	VALUE\n";
			sql += "FROM   PARAMETER\n";
			sql += "WHERE 1 = 1\n";
			sql += "  AND (KEY = @KEY OR @KEY IS NULL)\n";
			sql += "  AND (VALUE = @VALUE OR @VALUE IS NULL)\n";

			return sql;
		}
	}// Close out the class and namespace
}
