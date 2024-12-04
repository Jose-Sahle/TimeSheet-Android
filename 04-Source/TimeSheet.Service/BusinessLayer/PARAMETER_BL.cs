/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 28/03/2020 02:44:43
**********************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using SHL.IRetorno.Model;

using SHL.Parameter.Model;
using SHL.Parameter.DataLayer;
using Microsoft.Data.Sqlite;

namespace SHL.Parameter.BusinessLayer
{ 
	public sealed partial class PARAMETER_BL
	{	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		/// <returns></returns>
		public Boolean ValidateToInsert(PARAMETER entity, ref List<RETORNO> retornos)
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
		public Boolean ValidateToUpdate(PARAMETER entity, ref List<RETORNO> retornos)
		{
			Boolean ret = true;

			//SHLSTUDIO_USER_AREA_START_2
			//SHLSTUDIO_USER_AREA_END_2
						
			return ret;
		}
	
		/// <summary>
		/// Select all records
		/// </summary>");
		public List<PARAMETER> SelectList()
		{		
			PARAMETER entity = new PARAMETER();
			PARAMETER_DL oPARAMETER_DL = new PARAMETER_DL();
			
			return oPARAMETER_DL.SelectList(entity);
		}

		/// <summary>
		/// Select a record by filter
		/// </summary>");
		public PARAMETER Select(PARAMETER entity)
		{		
			PARAMETER_DL oPARAMETER_DL = new PARAMETER_DL();
			
			return oPARAMETER_DL.Select(entity);
		}

		/// <summary>
		/// Select some records by filter
		/// </summary>");
		public List<PARAMETER> SelectList(PARAMETER entity)
		{		
			PARAMETER_DL oPARAMETER_DL = new PARAMETER_DL();
			
			return oPARAMETER_DL.SelectList(entity);
		}

		/// <summary>
		/// Insert a record in the table PARAMETER
		/// </summary>");
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		public void Insert(PARAMETER entity)
		{		
			PARAMETER_DL oPARAMETER_DL = new PARAMETER_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToInsert(entity, ref retornos))
				oPARAMETER_DL.Insert(entity);
		}

		/// <summary>
		/// Insert a record in the table PARAMETER inside a transaction
		/// </summary>");		
		/// <param name="oSqliteConnection"></param>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		public void Insert(SqliteTransaction oSqliteConnection,  PARAMETER entity)
		{		
			PARAMETER_DL oPARAMETER_DL = new PARAMETER_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToInsert(entity, ref retornos))
				oPARAMETER_DL.Insert(oSqliteConnection, entity);
		}

		/// <summary>
		/// Update a record in the table PARAMETER
		/// </summary>");
		public void Update(PARAMETER entity)
		{		
			PARAMETER_DL oPARAMETER_DL = new PARAMETER_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToUpdate(entity, ref retornos))
				oPARAMETER_DL.Update(entity);
		}

		/// <summary>
		/// Update a record in the table PARAMETER with transaction
		/// </summary>");
		public void Update(SqliteTransaction oSqliteConnection, PARAMETER entity)
		{		
			PARAMETER_DL oPARAMETER_DL = new PARAMETER_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToUpdate(entity, ref retornos))
				oPARAMETER_DL.Update(oSqliteConnection, entity);
		}

		/// <summary>
		/// Delete a record from table PARAMETER
		/// </summary>");
		public void Delete(PARAMETER entity)
		{		
			PARAMETER_DL oPARAMETER_DL = new PARAMETER_DL();
			
			oPARAMETER_DL.Delete(entity);
		}

		/// <summary>
		/// Delete a record from table PARAMETER with transaction
		/// </summary>");
		public void Delete(SqliteTransaction oSqliteConnection, PARAMETER entity)
		{		
			PARAMETER_DL oPARAMETER_DL = new PARAMETER_DL();
			
			oPARAMETER_DL.Delete(oSqliteConnection, entity);
		}		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public SqliteTransaction GetTransaction()
		{
			SqliteTransaction transaction = null;
			
			PARAMETER_DL oPARAMETER_DL = new PARAMETER_DL();

			transaction = oPARAMETER_DL.GetTransaction();

			return transaction;
		}			
	}
}
