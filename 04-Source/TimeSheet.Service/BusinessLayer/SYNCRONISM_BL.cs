/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 05/04/2020 16:58:05
**********************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

using SHL.IRetorno.Model;
using SHL.Syncronism.Model;
using SHL.TimeSheet.Service.DataLayer;

namespace SHL.Syncronism.BusinessLayer
{
	public sealed partial class SYNCRONISM_BL
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		/// <returns></returns>
		public Boolean ValidateToInsert(SYNCRONISM entity, ref List<RETORNO> retornos)
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
		public Boolean ValidateToUpdate(SYNCRONISM entity, ref List<RETORNO> retornos)
		{
			Boolean ret = true;

			//SHLSTUDIO_USER_AREA_START_2
			//SHLSTUDIO_USER_AREA_END_2

			return ret;
		}

		/// <summary>
		/// Select all records
		/// </summary>");
		public List<SYNCRONISM> SelectList()
		{
			SYNCRONISM entity = new SYNCRONISM();
			SYNCRONISM_DL oSYNCRONISM_DL = new SYNCRONISM_DL();

			return oSYNCRONISM_DL.SelectList(entity);
		}

		/// <summary>
		/// Select a record by filter
		/// </summary>");
		public SYNCRONISM Select(SYNCRONISM entity)
		{
			SYNCRONISM_DL oSYNCRONISM_DL = new SYNCRONISM_DL();

			return oSYNCRONISM_DL.Select(entity);
		}

		/// <summary>
		/// Select some records by filter
		/// </summary>");
		public List<SYNCRONISM> SelectList(SYNCRONISM entity)
		{
			SYNCRONISM_DL oSYNCRONISM_DL = new SYNCRONISM_DL();

			return oSYNCRONISM_DL.SelectList(entity);
		}

		/// <summary>
		/// Insert a record in the table SYNCRONISM
		/// </summary>");
		/// <param name=""></param>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		public void Insert(SYNCRONISM entity)
		{
			SYNCRONISM_DL oSYNCRONISM_DL = new SYNCRONISM_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToInsert(entity, ref retornos))
				oSYNCRONISM_DL.Insert(entity);
		}

		/// <summary>
		/// Insert a record in the table SYNCRONISM inside a transaction
		/// </summary>");		
		/// <param name=""></param>
		/// <param name="oSqliteConnection"></param>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		public void Insert(SqliteTransaction oSqliteConnection, SYNCRONISM entity)
		{
			SYNCRONISM_DL oSYNCRONISM_DL = new SYNCRONISM_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToInsert(entity, ref retornos))
				oSYNCRONISM_DL.Insert(oSqliteConnection, entity);
		}

		/// <summary>
		/// Update a record in the table SYNCRONISM
		/// </summary>");
		public void Update(SYNCRONISM entity)
		{
			SYNCRONISM_DL oSYNCRONISM_DL = new SYNCRONISM_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToUpdate(entity, ref retornos))
				oSYNCRONISM_DL.Update(entity);
		}

		/// <summary>
		/// Update a record in the table SYNCRONISM with transaction
		/// </summary>");
		public void Update(SqliteTransaction oSqliteConnection, SYNCRONISM entity)
		{
			SYNCRONISM_DL oSYNCRONISM_DL = new SYNCRONISM_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToUpdate(entity, ref retornos))
				oSYNCRONISM_DL.Update(oSqliteConnection, entity);
		}

		/// <summary>
		/// Delete a record from table SYNCRONISM
		/// </summary>");
		public void Delete(SYNCRONISM entity)
		{
			SYNCRONISM_DL oSYNCRONISM_DL = new SYNCRONISM_DL();

			oSYNCRONISM_DL.Delete(entity);
		}

		/// <summary>
		/// Delete a record from table SYNCRONISM with transaction
		/// </summary>");
		public void Delete(SqliteTransaction oSqliteConnection, SYNCRONISM entity)
		{
			SYNCRONISM_DL oSYNCRONISM_DL = new SYNCRONISM_DL();

			oSYNCRONISM_DL.Delete(oSqliteConnection, entity);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public SqliteTransaction GetTransaction()
		{
			SqliteTransaction transaction = null;

			SYNCRONISM_DL oSYNCRONISM_DL = new SYNCRONISM_DL();

			transaction = oSYNCRONISM_DL.GetTransaction();

			return transaction;
		}
	}
}
