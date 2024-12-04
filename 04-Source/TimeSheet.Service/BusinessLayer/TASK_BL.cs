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

using SHL.Task.Model;
using SHL.Task.DataLayer;

namespace SHL.Task.BusinessLayer
{ 
	public sealed partial class TASK_BL
	{	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		/// <returns></returns>
		public Boolean ValidateToInsert(TASK entity, ref List<RETORNO> retornos)
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
		public Boolean ValidateToUpdate(TASK entity, ref List<RETORNO> retornos)
		{
			Boolean ret = true;

			//SHLSTUDIO_USER_AREA_START_2
			//SHLSTUDIO_USER_AREA_END_2
						
			return ret;
		}
	
		/// <summary>
		/// Select all records
		/// </summary>");
		public List<TASK> SelectList()
		{		
			TASK entity = new TASK();
			TASK_DL oTASK_DL = new TASK_DL();
			
			return oTASK_DL.SelectList(entity);
		}

		/// <summary>
		/// Select a record by filter
		/// </summary>");
		public TASK Select(TASK entity)
		{		
			TASK_DL oTASK_DL = new TASK_DL();
			
			return oTASK_DL.Select(entity);
		}

		/// <summary>
		/// Select some records by filter
		/// </summary>");
		public List<TASK> SelectList(TASK entity)
		{		
			TASK_DL oTASK_DL = new TASK_DL();
			
			return oTASK_DL.SelectList(entity);
		}

		/// <summary>
		/// Insert a record in the table TASK
		/// </summary>");
		/// <param name=""></param>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		public void Insert(TASK entity)
		{		
			TASK_DL oTASK_DL = new TASK_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToInsert(entity, ref retornos))
				oTASK_DL.Insert(entity);
		}

		/// <summary>
		/// Insert a record in the table TASK inside a transaction
		/// </summary>");		
		/// <param name=""></param>
		/// <param name="oSqliteConnection"></param>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		public void Insert(SqliteTransaction oSqliteConnection,  TASK entity)
		{		
			TASK_DL oTASK_DL = new TASK_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToInsert(entity, ref retornos))
				oTASK_DL.Insert(oSqliteConnection, entity);
		}

		/// <summary>
		/// Update a record in the table TASK
		/// </summary>");
		public void Update(TASK entity)
		{		
			TASK_DL oTASK_DL = new TASK_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToUpdate(entity, ref retornos))
				oTASK_DL.Update(entity);
		}

		/// <summary>
		/// Update a record in the table TASK with transaction
		/// </summary>");
		public void Update(SqliteTransaction oSqliteConnection, TASK entity)
		{		
			TASK_DL oTASK_DL = new TASK_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToUpdate(entity, ref retornos))
				oTASK_DL.Update(oSqliteConnection, entity);
		}

		/// <summary>
		/// Delete a record from table TASK
		/// </summary>");
		public void Delete(TASK entity)
		{		
			TASK_DL oTASK_DL = new TASK_DL();
			
			oTASK_DL.Delete(entity);
		}

		/// <summary>
		/// Delete a record from table TASK with transaction
		/// </summary>");
		public void Delete(SqliteTransaction oSqliteConnection, TASK entity)
		{		
			TASK_DL oTASK_DL = new TASK_DL();
			
			oTASK_DL.Delete(oSqliteConnection, entity);
		}		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public SqliteTransaction GetTransaction()
		{
			SqliteTransaction transaction = null;
			
			TASK_DL oTASK_DL = new TASK_DL();

			transaction = oTASK_DL.GetTransaction();

			return transaction;
		}			
	}
}
