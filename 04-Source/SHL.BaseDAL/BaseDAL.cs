/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 02/12/2018 16:22:03
**********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Microsoft.Data.Sqlite;

namespace SHL.Data.Base
{
	/// <summary>
	/// Base dal.
	/// </summary>
	public abstract class BaseDAL<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SHL.TokenSecurity.DataLayer.BaseDAL`1"/> class.
		/// </summary>
		public BaseDAL()
		{

		}

		private StringBuilder _sb;

		/// <summary>
		/// Gets the query.
		/// </summary>
		/// <value>The query.</value>
		protected StringBuilder Query
		{
			get
			{
				if (_sb == null) _sb = new StringBuilder();
				return _sb;
			}
		}

		/// <summary>
		/// Gets the transaction.
		/// </summary>
		/// <returns>The transaction.</returns>
		public SqliteTransaction GetTransaction()
		{
			SqliteTransaction transaction = null;

			transaction = SQLHelper.GetTransaction();

			return transaction;
		}

		/// <summary>
		/// Returns the list.
		/// </summary>
		/// <returns>The list.</returns>
		/// <param name="procedure">Procedure.</param>
		/// <param name="parameters">Parameters.</param>
		protected List<T> ReturnList(string procedure, ArrayList parameters)
		{
			List<T> returnlist = new List<T>();

			using (SqliteDataReader dataReader = SQLHelper.ExecuteReader(procedure, parameters))
			{
				if (dataReader.HasRows)
				{
					returnlist = new List<T>();

					while (dataReader.Read())
					{
						T obj = default(T);
						obj = (T)Activator.CreateInstance(typeof(T), dataReader);
						returnlist.Add(obj);
					}
				}
			}

			return returnlist;
		}

		/// <summary>
		/// Returns the list.
		/// </summary>
		/// <returns>The list.</returns>
		/// <param name="procedure">Procedure.</param>
		protected List<T> ReturnList(string procedure)
		{
			List<T> returnlist = new List<T>();

			using (SqliteDataReader dataReader = SQLHelper.ExecuteReader(procedure))
			{
				if (dataReader.HasRows)
				{
					returnlist = new List<T>();
					while (dataReader.Read())
					{
						T obj;
						obj = (T)Activator.CreateInstance(typeof(T), dataReader);
						returnlist.Add(obj);
					}
				}
			}

			return returnlist;
		}

		/// <summary>
		/// Returns the unique.
		/// </summary>
		/// <returns>The unique.</returns>
		/// <param name="commandText">Command text.</param>
		/// <param name="parameters">Parameters.</param>
		protected T ReturnUnique(String commandText, ArrayList parameters)
		{
			T obj = default(T);

			using (SqliteDataReader dataReader = SQLHelper.ExecuteReader(commandText, parameters))
			{
				if (dataReader.HasRows)
				{
					dataReader.Read();
					obj = (T)Activator.CreateInstance(typeof(T), dataReader);
				}
			}

			return obj;
		}

		/// <summary>
		/// Returns the unique.
		/// </summary>
		/// <returns>The unique.</returns>
		/// <param name="commandText">Command text.</param>
		protected T ReturnUnique(string commandText)
		{
			T obj = default(T);

			using (SqliteDataReader dataReader = SQLHelper.ExecuteReader(commandText))
			{
				if (dataReader.HasRows)
				{
					dataReader.Read();
					obj = (T)Activator.CreateInstance(typeof(T), dataReader);
				}
			}

			return obj;
		}

		/// <summary>
		/// Clears the query.
		/// </summary>
		protected void ClearQuery()
		{
			if (_sb != null)
			{
				_sb.Remove(0, _sb.Length);
			}
		}
	}
}
