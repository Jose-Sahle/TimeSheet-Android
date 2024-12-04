/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 07/03/2019 21:26:05
**********************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

using SHL.IRetorno.Model;

using SHL.TimeSheet.Model;
using SHL.TimeSheet.DataLayer;
using SHL.Task.BusinessLayer;
using SHL.Project.BusinessLayer;
using SHL.Project.Model;
using SHL.Data.Base;
using SHL.Task.Model;

namespace SHL.TimeSheet.BusinessLayer
{ 
	/// <summary>
	/// 
	/// </summary>
	public sealed partial class TIMESHEET_BL
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="retornos"></param>
		/// <returns></returns>
		public Boolean ValidateToInsert(TIMESHEET entity, ref List<RETORNO> retornos)
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
		public Boolean ValidateToUpdate(TIMESHEET entity, ref List<RETORNO> retornos)
		{
			Boolean ret = true;

			//SHLSTUDIO_USER_AREA_START_2
			//SHLSTUDIO_USER_AREA_END_2
						
			return ret;
		}
	
		/// <summary>
		/// Select all records
		/// </summary>");
		public List<TIMESHEET> SelectList()
		{		
			TIMESHEET entity = new TIMESHEET();
			TIMESHEET_DL oTIMESHEET_DL = new TIMESHEET_DL();
			
			return oTIMESHEET_DL.SelectList(entity);
		}

		/// <summary>
		/// Select a record by filter
		/// </summary>");
		public TIMESHEET Select(TIMESHEET entity)
		{		
			TIMESHEET_DL oTIMESHEET_DL = new TIMESHEET_DL();
			
			return oTIMESHEET_DL.Select(entity);
		}

		/// <summary>
		/// Select some records by filter
		/// </summary>");
		public List<TIMESHEET> SelectList(TIMESHEET entity)
		{		
			TIMESHEET_DL oTIMESHEET_DL = new TIMESHEET_DL();
			
			return oTIMESHEET_DL.SelectList(entity);
		}

		/// <summary>
		/// Insert a record in the table TIMESHEET
		/// </summary>");
		public void Insert(TIMESHEET entity)
		{		
			TIMESHEET_DL oTIMESHEET_DL = new TIMESHEET_DL();
			SqliteTransaction transaction = null;

			try
			{
				transaction = oTIMESHEET_DL.GetTransaction();
				Insert(entity, transaction);
				transaction.Commit();
			}
			catch (Exception ex)
			{
				if (transaction != null)
					transaction.Rollback();

				throw ex;
			}
		}

		/// <summary>
		/// Insert a record in the table TIMESHEET inside a transaction
		/// </summary>");
		public void Insert(TIMESHEET entity, SqliteTransaction oSqliteConnection)
		{		
			TIMESHEET_DL oTIMESHEET_DL = new TIMESHEET_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			TASK_BL taskbl = new TASK_BL();
			PROJECT_BL projectbl = new PROJECT_BL();
			PROJECT project = null;

			Int32 id_timesheet;

			try
			{
				if (ValidateToInsert(entity, ref retornos))
				{
					oTIMESHEET_DL.Insert(oSqliteConnection, entity);

					id_timesheet = Convert.ToInt32(SQLHelper.ExecuteScalar(oSqliteConnection.Connection, oSqliteConnection, "select last_insert_rowid();"));

					foreach (TASK task in entity.TASKS)
					{
						project = new PROJECT();

						project.NAME = task.PROJECTNAME;
						project = projectbl.Select(project);

						task.ID_TIMESHEET = id_timesheet;
						task.ID_PROJECT = project.ID_PROJECT;

						taskbl.Insert(oSqliteConnection, task);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Update a record in the table TIMESHEET
		/// </summary>");
		public void Update(TIMESHEET entity)
		{		
			TIMESHEET_DL oTIMESHEET_DL = new TIMESHEET_DL();
			SqliteTransaction transaction = null;

			try
			{
				transaction = oTIMESHEET_DL.GetTransaction();
				Update(transaction, entity);
				transaction.Commit();
			}
			catch (Exception ex)
			{
				if (transaction != null)
					transaction.Rollback();

				throw ex;
			}
		}

		/// <summary>
		/// Update a record in the table TIMESHEET with transaction
		/// </summary>");
		public void Update(SqliteTransaction oSqliteTransaction, TIMESHEET entity)
		{		
			TIMESHEET_DL oTIMESHEET_DL = new TIMESHEET_DL();
			List<RETORNO> retornos = new List<RETORNO>();

			TASK_BL taskbl = new TASK_BL();
			PROJECT_BL projectbl = new PROJECT_BL();
			PROJECT project = null;
			TASK tasktodelete = null;

			try
			{
				if (ValidateToUpdate(entity, ref retornos))
				{
					entity.ID_TIMESHEET = oTIMESHEET_DL.Select(new TIMESHEET() { DATE_RG = entity.DATE_RG }).ID_TIMESHEET;
					oTIMESHEET_DL.Update(oSqliteTransaction, entity);
					
					tasktodelete = new TASK();
					tasktodelete.ID_TIMESHEET = entity.ID_TIMESHEET;
					taskbl.Delete(oSqliteTransaction, tasktodelete);

					foreach (TASK task in entity.TASKS)
					{
						project = new PROJECT();

						project.NAME = task.PROJECTNAME;
						project = projectbl.Select(project);

						task.ID_TIMESHEET = entity.ID_TIMESHEET;
						task.ID_PROJECT = project.ID_PROJECT;

						taskbl.Insert(oSqliteTransaction, task);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Delete a record from table TIMESHEET
		/// </summary>");
		public void Delete(TIMESHEET entity)
		{		
			TIMESHEET_DL oTIMESHEET_DL = new TIMESHEET_DL();
			SqliteTransaction transaction = null;

			try
			{
				transaction = oTIMESHEET_DL.GetTransaction();
				Delete(transaction, entity);
				transaction.Commit();
			}
			catch (Exception ex)
			{
				if (transaction != null)
					transaction.Rollback();

				throw ex;
			}
		}

		/// <summary>
		/// Delete a record from table TIMESHEET with transaction
		/// </summary>");
		public void Delete(SqliteTransaction oSqliteTransaction, TIMESHEET entity)
		{		
			TIMESHEET_DL oTIMESHEET_DL = new TIMESHEET_DL();
			TASK_BL taskbl = new TASK_BL();
			TASK tasktodelete = null;

			try
			{
				tasktodelete = new TASK();
				tasktodelete.ID_TIMESHEET = entity.ID_TIMESHEET;
				taskbl.Delete(oSqliteTransaction, tasktodelete);

				oTIMESHEET_DL.Delete(oSqliteTransaction, entity);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}	
	
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public SqliteTransaction GetTransaction()
		{
			SqliteTransaction transaction = null;
			
			TIMESHEET_DL oTIMESHEET_DL = new TIMESHEET_DL();

			transaction = oTIMESHEET_DL.GetTransaction();

			return transaction;
		}			
	}
}
