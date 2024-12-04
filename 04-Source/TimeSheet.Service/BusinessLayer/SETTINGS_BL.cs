/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 28/03/2020 02:49:05
**********************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

using SHL.IRetorno.Model;

using SHL.Settings.Model;
using SHL.Settings.DataLayer;

namespace SHL.Settings.BusinessLayer
{ 
	public sealed partial class SETTINGS_BL
	{	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		/// <returns></returns>
		public Boolean ValidateToInsert(SETTINGS entity, ref List<RETORNO> retornos)
		{
			Boolean ret = true;

			//SHLSTUDIO_USER_AREA_START_1
			//SHLSTUDIO_USER_AREA_END_1
						
			return ret;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		/// <returns></returns>
		public Boolean ValidateToUpdate(SETTINGS entity, ref List<RETORNO> retornos)
		{
			Boolean ret = true;

			//SHLSTUDIO_USER_AREA_START_2
			//SHLSTUDIO_USER_AREA_END_2
						
			return ret;
		}
	
		/// <summary>
		/// Select all records
		/// </summary>");
		public List<SETTINGS> SelectList()
		{
			SETTINGS entity = new SETTINGS();
			SETTINGS_DL oSETTINGS_DL = new SETTINGS_DL();
			
			return oSETTINGS_DL.SelectList(entity);
		}

		/// <summary>
		/// Select a record by filter
		/// </summary>");
		public SETTINGS Select(SETTINGS entity)
		{		
			SETTINGS_DL oSETTINGS_DL = new SETTINGS_DL();
			
			return oSETTINGS_DL.Select(entity);
		}

		/// <summary>
		/// Select some records by filter
		/// </summary>");
		public List<SETTINGS> SelectList(SETTINGS entity)
		{		
			SETTINGS_DL oSETTINGS_DL = new SETTINGS_DL();
			
			return oSETTINGS_DL.SelectList(entity);
		}

		/// <summary>
		/// Insert a record in the table SETTINGS
		/// </summary>");
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		public void Insert(SETTINGS entity)
		{		
			SETTINGS_DL oSETTINGS_DL = new SETTINGS_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToInsert(entity, ref retornos))
				oSETTINGS_DL.Insert(entity);
		}

		/// <summary>
		/// Insert a record in the table SETTINGS inside a transaction
		/// </summary>");		
		/// <param name="Group"></param>
		/// <param name="oSqlTransaction"></param>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		public void Insert(String Group, SqliteTransaction oSqlTransaction,  SETTINGS entity)
		{		
			SETTINGS_DL oSETTINGS_DL = new SETTINGS_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToInsert(entity, ref retornos))
				oSETTINGS_DL.Insert(oSqlTransaction, entity);
		}

		/// <summary>
		/// Update a record in the table SETTINGS
		/// </summary>");
		public void Update(SETTINGS entity)
		{		
			SETTINGS_DL oSETTINGS_DL = new SETTINGS_DL();
			List<RETORNO> retornos = new List<RETORNO>();
			if (ValidateToUpdate(entity, ref retornos))
				oSETTINGS_DL.Update(entity);
		}

		/// <summary>
		/// Update a record in the table SETTINGS with transaction
		/// </summary>");
		public void Update(SqliteTransaction oSqlTransaction, SETTINGS entity)
		{		
			SETTINGS_DL oSETTINGS_DL = new SETTINGS_DL();
			List<RETORNO> retornos = new List<RETORNO>();
			if (ValidateToUpdate(entity, ref retornos))
				oSETTINGS_DL.Update(oSqlTransaction, entity);
		}

		/// <summary>
		/// Delete a record from table SETTINGS
		/// </summary>");
		public void Delete(SETTINGS entity)
		{		
			SETTINGS_DL oSETTINGS_DL = new SETTINGS_DL();
			
			oSETTINGS_DL.Delete(entity);
		}

		/// <summary>
		/// Delete a record from table SETTINGS with transaction
		/// </summary>");
		public void Delete(SqliteTransaction oSqlTransaction, SETTINGS entity)
		{		
			SETTINGS_DL oSETTINGS_DL = new SETTINGS_DL();
			
			oSETTINGS_DL.Delete(oSqlTransaction, entity);
		}		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Group"></param>
		/// <returns></returns>
		public SqliteTransaction GetTransaction()
		{
			SqliteTransaction transaction = null;
			
			SETTINGS_DL oSETTINGS_DL = new SETTINGS_DL();

			transaction = oSETTINGS_DL.GetTransaction();

			return transaction;
		}			
	}
}
