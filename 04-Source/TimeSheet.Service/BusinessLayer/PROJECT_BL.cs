/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 28/03/2020 02:49:03
**********************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

using SHL.IRetorno.Model;

using SHL.Project.Model;
using SHL.Project.DataLayer;

namespace SHL.Project.BusinessLayer
{ 
	public sealed partial class PROJECT_BL
	{	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		/// <returns></returns>
		public Boolean ValidateToInsert(PROJECT entity, ref List<RETORNO> retornos)
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
		public Boolean ValidateToUpdate(PROJECT entity, ref List<RETORNO> retornos)
		{
			Boolean ret = true;

			//SHLSTUDIO_USER_AREA_START_2
			//SHLSTUDIO_USER_AREA_END_2
						
			return ret;
		}
	
		/// <summary>
		/// Select all records
		/// </summary>");
		public List<PROJECT> SelectList()
		{		
			PROJECT entity = new PROJECT();
			PROJECT_DL oPROJECT_DL = new PROJECT_DL();
			
			return oPROJECT_DL.SelectList(entity);
		}

		/// <summary>
		/// Select a record by filter
		/// </summary>");
		public PROJECT Select(PROJECT entity)
		{		
			PROJECT_DL oPROJECT_DL = new PROJECT_DL();
			
			return oPROJECT_DL.Select(entity);
		}

		/// <summary>
		/// Select some records by filter
		/// </summary>");
		public List<PROJECT> SelectList(PROJECT entity)
		{		
			PROJECT_DL oPROJECT_DL = new PROJECT_DL();
			
			return oPROJECT_DL.SelectList(entity);
		}

		/// <summary>
		/// Insert a record in the table PROJECT
		/// </summary>");
		/// <param name=""></param>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		public void Insert(PROJECT entity)
		{		
			PROJECT_DL oPROJECT_DL = new PROJECT_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToInsert(entity, ref retornos))
				oPROJECT_DL.Insert(entity);
		}

		/// <summary>
		/// Insert a record in the table PROJECT inside a transaction
		/// </summary>");		
		/// <param name=""></param>
		/// <param name="oSqliteConnection"></param>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		public void Insert(SqliteTransaction oSqliteConnection,  PROJECT entity)
		{		
			PROJECT_DL oPROJECT_DL = new PROJECT_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToInsert(entity, ref retornos))
				oPROJECT_DL.Insert(oSqliteConnection, entity);
		}

		/// <summary>
		/// Update a record in the table PROJECT
		/// </summary>");
		public void Update(PROJECT entity)
		{		
			PROJECT_DL oPROJECT_DL = new PROJECT_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToUpdate(entity, ref retornos))
				oPROJECT_DL.Update(entity);
		}

		/// <summary>
		/// Update a record in the table PROJECT with transaction
		/// </summary>");
		public void Update(SqliteTransaction oSqliteConnection, PROJECT entity)
		{		
			PROJECT_DL oPROJECT_DL = new PROJECT_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			if (ValidateToUpdate(entity, ref retornos))
				oPROJECT_DL.Update(oSqliteConnection, entity);
		}

		/// <summary>
		/// Delete a record from table PROJECT
		/// </summary>");
		public void Delete(PROJECT entity)
		{		
			PROJECT_DL oPROJECT_DL = new PROJECT_DL();
			
			oPROJECT_DL.Delete(entity);
		}

		/// <summary>
		/// Delete a record from table PROJECT with transaction
		/// </summary>");
		public void Delete(SqliteTransaction oSqliteConnection, PROJECT entity)
		{		
			PROJECT_DL oPROJECT_DL = new PROJECT_DL();
			
			oPROJECT_DL.Delete(oSqliteConnection, entity);
		}		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public SqliteTransaction GetTransaction()
		{
			SqliteTransaction transaction = null;
			
			PROJECT_DL oPROJECT_DL = new PROJECT_DL();

			transaction = oPROJECT_DL.GetTransaction();

			return transaction;
		}			
	}
}
