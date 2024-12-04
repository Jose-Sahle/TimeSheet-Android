/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 25/11/2018 13:50:29
**********************************************************/
using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Data.Sqlite;

namespace SHL.Data.Base
{
	/// <summary>
	/// SQLH elper.
	/// </summary>
	public static class SQLHelper
	{
		private static String ConnectionString = String.Empty;
		// Time out em minutos COMMAND_TIMEOUT/60.
		private const int COMMAND_TIMEOUT = 60;

		/// <summary>
		/// Get the database transaction
		/// </summary>
		public static SqliteTransaction GetTransaction()
		{
			SqliteConnection oConnection = GetConnection();
			SqliteTransaction oTransaction;

			try
			{
				OpenDatabase(ref oConnection);
			}
			finally
			{
				oTransaction = oConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
			}

			return oTransaction;
		}

		/// <summary>
		/// Retorna a String de Conexo
		/// </summary>
		/// <returns>A conexo configurada.</returns>
		public static String GetConnectionString()
		{
			String connectionstring = String.Empty;

			string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			connectionstring = String.Format("Filename={0}", Path.Combine(path, "TimeSheet.db"));

			if (!File.Exists(Path.Combine(path, "TimeSheet.db")))
				throw new Exception("Arquivo '" + connectionstring + "' não foi encontrado.");

			return connectionstring;
		}

		/// <summary>		
		/// Obt??uma conex??para o banco de dados padr??o
		/// </summary>
		public static SqliteConnection GetConnection()
		{
			return new SqliteConnection(GetConnectionString());
		}

		/// <summary>
		/// Sets the connection.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		public static void SetConnection(String connectionString)
		{
			ConnectionString = connectionString;
		}

		#region ExecuteNonQuery functions
		/// <summary>
		/// Execute a stored procedure.
		/// </summary>
		/// <param name="commandText">The stored procedure to execute.</param>
		public static void ExecuteNonQuery(string commandText)
		{
			SqliteConnection connection = GetConnection();
			ExecuteNonQuery(connection, commandText);
		}

		/// <summary>
		/// Executes the stored procedure on the specified <see cref="SqliteConnection"/>.
		/// </summary>
		/// <param name="connection">The database connection to be used.</param>
		/// <param name="commandText">The stored procedure to execute.</param>
		public static void ExecuteNonQuery(SqliteConnection connection, String commandText)
		{
			ExecuteNonQuery(connection, null, commandText);
		}

		/// <summary>
		/// Executes the stored procedure on the specified <see cref="SqliteConnection"/> within the specified <see cref="SqliteTransaction"/>.
		/// </summary>
		/// <param name="connection">The database connection to be used.</param>
		/// <param name="transaction">The transaction to participate in.</param>
		/// <param name="commandText">The stored procedure to execute.</param>
		public static void ExecuteNonQuery(SqliteConnection connection, SqliteTransaction transaction, String commandText)
		{
			if (connection.State == ConnectionState.Closed)
			{
				OpenDatabase(ref connection);
			}

			SqliteCommand command = CreateCommand(connection, transaction, commandText);
			command.ExecuteNonQuery();
		}

		/// <summary>
		/// Executes the stored procedure with the specified parameters.
		/// </summary>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <param name="parameters">The parameters of the stored procedure.</param>
		public static void ExecuteNonQuery(String commandText, ArrayList parameters)
		{
			SqliteConnection connection = GetConnection();
			ExecuteNonQuery(connection, commandText, parameters);

		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandText">Command text.</param>
		/// <param name="transaction">Transaction.</param>
		/// <param name="parameters">Parameters.</param>
		public static void ExecuteNonQuery(string commandText, SqliteTransaction transaction, ArrayList parameters)
		{
			SqliteConnection connection;

			if (transaction == null)
				connection = GetConnection();
			else
				connection = transaction.Connection;

			ExecuteNonQuery(connection, transaction, commandText, parameters);

		}

		/// <summary>
		/// Executes the stored procedure with the specified parameters on the specified connection.
		/// </summary>
		/// <param name="connection">The database connection to be used.</param>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <param name="parameters">The parameters of the stored procedure.</param>
		public static void ExecuteNonQuery(SqliteConnection connection, string commandText, ArrayList parameters)
		{
			ExecuteNonQuery(connection, null, commandText, parameters);
		}

		/// <summary>
		/// Executes the stored procedure with the specified parameters on the specified <see cref="SqliteConnection"/> within the specified <see cref="SqliteTransaction"/>.
		/// </summary>
		/// <param name="connection">The database connection to be used.</param>
		/// <param name="transaction">The transaction to participate in.</param>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <param name="parameters">The parameters of the stored procedure.</param>
		public static void ExecuteNonQuery(SqliteConnection connection, SqliteTransaction transaction, String commandText, ArrayList parameters)
		{
			if (connection.State == ConnectionState.Closed)
			{
				OpenDatabase(ref connection);
			}

			SqliteCommand command = CreateCommand(connection, transaction, commandText, parameters);

			command.ExecuteNonQuery();
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <returns>The non query.</returns>
		/// <param name="commandText">Command text.</param>
		/// <param name="parameters">Parameters.</param>
		/// <param name="ReturnParameter">Return parameter.</param>
		public static Int64 ExecuteNonQuery(String commandText, ArrayList parameters, String ReturnParameter)
		{
			SqliteConnection connection = GetConnection();

			return ExecuteNonQuery(commandText, parameters, connection, ReturnParameter);
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <returns>The non query.</returns>
		/// <param name="commandText">Command text.</param>
		/// <param name="parameters">Parameters.</param>
		/// <param name="connection">Connection.</param>
		/// <param name="ReturnParameter">Return parameter.</param>
		public static Int64 ExecuteNonQuery(String commandText, ArrayList parameters, SqliteConnection connection, String ReturnParameter)
		{
			Int64 vRetorno = 0;
			SqliteTransaction transaction = null;

			OpenDatabase(ref connection);

			SqliteCommand command = CreateCommand(connection, transaction, commandText, parameters);

			//SqliteParameter retVal = command.Parameters.Add(ReturnParameter, DbType.Int32);
			//retVal.Direction = ParameterDirection.Output;

			command.ExecuteNonQuery();

			//vRetorno = Convert.ToInt32(retVal.Value);

			return vRetorno;
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <returns>The non query.</returns>
		/// <param name="commandText">Command text.</param>
		/// <param name="transaction">Transaction.</param>
		/// <param name="parameters">Parameters.</param>
		/// <param name="ReturnParameter">Return parameter.</param>
		public static Int64 ExecuteNonQuery(string commandText, SqliteTransaction transaction, ArrayList parameters, String ReturnParameter)
		{
			Int64 vRetorno = 0;
			SqliteConnection connection;

			if (transaction == null)
			{
				connection = GetConnection();

				OpenDatabase(ref connection);
			}
			else
			{
				connection = transaction.Connection;
			}

			SqliteCommand command = CreateCommand(connection, transaction, commandText, parameters);

			//SqliteParameter retVal = command.Parameters.Add(ReturnParameter, DbType.Int32);
			//retVal.Direction = ParameterDirection.Output;

			//vRetorno = command.ExecuteNonQuery();
			command.ExecuteNonQuery();

			//vRetorno = Convert.ToInt32(retVal.Value);

			return vRetorno;
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandText">Command text.</param>
		/// <param name="transaction">Transaction.</param>
		public static void ExecuteNonQuery(String commandText, SqliteTransaction transaction)
		{
			SqliteCommand command = new SqliteCommand();

			command.Connection = transaction.Connection;
			command.CommandText = commandText;
			command.CommandTimeout = COMMAND_TIMEOUT;
			command.CommandType = CommandType.Text;
			command.Transaction = transaction;

			command.ExecuteNonQuery();
		}
		#endregion

		#region ExecuteReader functions
		/// <summary>
		/// Executes the stored procedure and returns the result as a <see cref="SqliteDataReader"/>.
		/// </summary>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <returns>A <see cref="SqliteDataReader"/> containing the results of the stored procedure execution.</returns>
		public static SqliteDataReader ExecuteReader(string commandText)
		{
			SqliteConnection connection = GetConnection();

			OpenDatabase(ref connection);

			return ExecuteReader(connection, commandText);
		}

		/// <summary>
		/// Executes the stored procedure on the specified <see cref="SqliteConnection"/> and returns the result as a <see cref="SqliteDataReader"/>.
		/// </summary>
		/// <param name="connection">The database connection to be used.</param>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <returns>A <see cref="SqliteDataReader"/> containing the results of the stored procedure execution.</returns>
		public static SqliteDataReader ExecuteReader(SqliteConnection connection, String commandText)
		{
			return ExecuteReader(connection, null, commandText);
		}

		/// <summary>
		/// Executes the stored procedure on the specified <see cref="SqliteConnection"/> within the specified <see cref="SqliteTransaction"/> and returns the result as a <see cref="SqliteDataReader"/>.
		/// </summary>
		/// <param name="connection">The database connection to be used.</param>
		/// <param name="transaction">The transaction to participate in.</param>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <returns>A <see cref="SqliteDataReader"/> containing the results of the stored procedure execution.</returns>
		public static SqliteDataReader ExecuteReader(SqliteConnection connection, SqliteTransaction transaction, String commandText)
		{
			if (connection.State == ConnectionState.Closed)
			{
				OpenDatabase(ref connection);
			}

			SqliteCommand command = CreateCommand(connection, transaction, commandText);

			return command.ExecuteReader(CommandBehavior.CloseConnection);
		}

		/// <summary>
		/// Executes the stored procedure with the specified parameters and returns the result as a <see cref="SqliteDataReader"/>.
		/// </summary>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <param name="parameters">The parameters of the stored procedure.</param>
		/// <returns>A <see cref="SqliteDataReader"/> containing the results of the stored procedure execution.</returns>
		public static SqliteDataReader ExecuteReader(String commandText, ArrayList parameters)
		{
			SqliteConnection connection = GetConnection();

			OpenDatabase(ref connection);

			return ExecuteReader(connection, commandText, parameters);
		}

		/// <summary>
		/// Executes the stored procedure on the specified <see cref="SqliteConnection"/> with the specified parameters and returns the result as a <see cref="SqliteDataReader"/>.
		/// </summary>
		/// <param name="connection">The database connection to be used.</param>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <param name="parameters">The parameters of the stored procedure.</param>
		/// <returns>A <see cref="SqliteDataReader"/> containing the results of the stored procedure execution.</returns>
		public static SqliteDataReader ExecuteReader(SqliteConnection connection, string commandText, ArrayList parameters)
		{
			return ExecuteReader(connection, null, commandText, parameters);
		}

		/// <summary>
		/// Executes the stored procedure on the specified <see cref="SqliteConnection"/> within the specified <see cref="SqliteTransaction"/> with the specified parameters and returns the result as a <see cref="SqliteDataReader"/>.
		/// </summary>
		/// <param name="connection">The database connection to be used.</param>
		/// <param name="transaction">The transaction to participate in.</param>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <param name="parameters">The parameters of the stored procedure.</param>
		/// <returns>A <see cref="SqliteDataReader"/> containing the results of the stored procedure execution.</returns>
		public static SqliteDataReader ExecuteReader(SqliteConnection connection, SqliteTransaction transaction, string commandText, ArrayList parameters)
		{
			if (connection.State == ConnectionState.Closed)
			{
				OpenDatabase(ref connection);
			}

			SqliteCommand command = CreateCommand(connection, transaction, commandText, parameters);

			return command.ExecuteReader(CommandBehavior.CloseConnection);
		}
		#endregion

		#region ExecuteScalar functions
		/// <summary>
		/// Executes the stored procedure, and returns the first column of the first row in the result set returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		public static object ExecuteScalar(string commandText)
		{
			using (SqliteConnection connection = GetConnection())
			{
				return ExecuteScalar(connection, commandText);
			}
		}

		/// <summary>
		/// Executes the stored procedure on the specified <see cref="SqliteConnection"/>, and returns the first column of the first row in the result set returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="connection">The database connection to be used.</param>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		public static object ExecuteScalar(SqliteConnection connection, string commandText)
		{
			return ExecuteScalar(connection, null, commandText);
		}

		/// <summary>
		/// Executes the stored procedure on the specified <see cref="SqliteConnection"/> within the specified <see cref="SqliteTransaction"/>, and returns the first column of the first row in the result set returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="connection">The database connection to be used.</param>
		/// <param name="transaction">The transaction to participate in.</param>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		public static object ExecuteScalar(SqliteConnection connection, SqliteTransaction transaction, string commandText)
		{

			if (connection.State == ConnectionState.Closed)
			{
				OpenDatabase(ref connection);
			}

			using (SqliteCommand command = CreateCommand(connection, transaction, commandText))
			{
				return command.ExecuteScalar();
			}

		}

		/// <summary>
		/// Executes the stored procedure with the specified parameters, and returns the first column of the first row in the result set returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <param name="parameters">The parameters of the stored procedure.</param>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		public static object ExecuteScalar(string commandText, ArrayList parameters)
		{
			using (SqliteConnection connection = GetConnection())
			{
				return ExecuteScalar(connection, commandText, parameters);
			}
		}

		/// <summary>
		/// Executes the stored procedure on the specified <see cref="SqliteTransaction"/> with the specified parameters, and returns the first column of the first row in the result set returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="connection">The database connection to be used.</param>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <param name="parameters">The parameters of the stored procedure.</param>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		public static object ExecuteScalar(SqliteConnection connection, string commandText, ArrayList parameters)
		{
			return ExecuteScalar(connection, null, commandText, parameters);
		}

		/// <summary>
		/// Executes the stored procedure on the specified <see cref="SqliteTransaction"/> within the specified <see cref="SqliteTransaction"/> with the specified parameters, and returns the first column of the first row in the result set returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="connection">The database connection to be used.</param>
		/// <param name="transaction">The transaction to participate in.</param>
		/// <param name="commandText">The stored procedure to execute.</param>
		/// <param name="parameters">The parameters of the stored procedure.</param>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		public static object ExecuteScalar(SqliteConnection connection, SqliteTransaction transaction, string commandText, ArrayList parameters)
		{
			if (connection.State == ConnectionState.Closed)
			{
				OpenDatabase(ref connection);
			}

			using (SqliteCommand command = CreateCommand(connection, transaction, commandText, parameters))
			{
				return command.ExecuteScalar();
			}
		}
		#endregion

		#region Utility functions
		/// <summary>
		/// Adds the parameter.
		/// </summary>
		/// <returns>The parameter.</returns>
		/// <param name="vParameter">V parameter.</param>
		/// <param name="oValor">O valor.</param>
		public static ArrayList AddParameter(string vParameter, object oValor)
		{
			vParameter = AddAt(vParameter);

			ArrayList parameters = new ArrayList();

			parameters.Add(new SqliteParameter(vParameter, oValor));

			return parameters;
		}

		/// <summary>
		/// Adds the parameter.
		/// </summary>
		/// <param name="parameters">Parameters.</param>
		/// <param name="vParameter">V parameter.</param>
		/// <param name="oValor">O valor.</param>
		public static void AddParameter(ref ArrayList parameters, string vParameter, object oValor)
		{
			vParameter = AddAt(vParameter);

			parameters.Add(new SqliteParameter(vParameter, oValor));
		}

		private static string AddAt(string vParameter)
		{
			if (vParameter.Substring(0, 1) != "@")
			{
				vParameter.Insert(0, "@");
			}

			return vParameter;
		}

		private static SqliteParameter CheckParameter(SqliteParameter parameter)
		{
			if (parameter.Value == null)
			{
				parameter.Value = DBNull.Value;
			}
			else if ((parameter.DbType == DbType.DateTime ||
						parameter.DbType == DbType.Date ||
						parameter.DbType == DbType.Time
					) && Convert.ToDateTime(parameter.Value) == new DateTime(1900, 1, 1))
			{
				parameter.Value = DBNull.Value;
			}

			return parameter;
		}
		#region CreateCommand
		/// <summary>
		/// Creates, initializes, and returns a <see cref="SqliteCommand"/> instance.
		/// </summary>
		/// <param name="connection">The <see cref="SqliteConnection"/> the <see cref="SqliteCommand"/> should be executed on.</param>
		/// <param name="commandText">The name of the stored procedure to execute.</param>
		/// <returns>An initialized <see cref="SqliteCommand"/> instance.</returns>
		private static SqliteCommand CreateCommand(SqliteConnection connection, string commandText)
		{
			return CreateCommand(connection, null, commandText);
		}

		/// <summary>
		/// Creates, initializes, and returns a <see cref="SqliteCommand"/> instance.
		/// </summary>
		/// <param name="connection">The <see cref="SqliteConnection"/> the <see cref="SqliteCommand"/> should be executed on.</param>
		/// <param name="transaction">The <see cref="SqliteTransaction"/> the stored procedure execution should participate in.</param>
		/// <param name="commandText">The name of the stored procedure to execute.</param>
		/// <returns>An initialized <see cref="SqliteCommand"/> instance.</returns>
		private static SqliteCommand CreateCommand(SqliteConnection connection, SqliteTransaction transaction, string commandText)
		{
			SqliteCommand command = new SqliteCommand();

			command.Connection = connection;
			command.CommandText = commandText;
			command.CommandTimeout = COMMAND_TIMEOUT;

			command.CommandType = CommandType.Text;

			command.Transaction = transaction;

			return command;
		}

		/// <summary>
		/// Creates, initializes, and returns a <see cref="SqliteCommand"/> instance.
		/// </summary>
		/// <param name="connection">The <see cref="SqliteConnection"/> the <see cref="SqliteCommand"/> should be executed on.</param>
		/// <param name="commandText">The name of the stored procedure to execute.</param>
		/// <param name="parameters">The parameters of the stored procedure.</param>
		/// <returns>An initialized <see cref="SqliteCommand"/> instance.</returns>
		private static SqliteCommand CreateCommand(SqliteConnection connection, string commandText, ArrayList parameters)
		{
			return CreateCommand(connection, null, commandText, parameters);
		}

		/// <summary>
		/// Creates, initializes, and returns a <see cref="SqliteCommand"/> instance.
		/// </summary>
		/// <param name="connection">The <see cref="SqliteConnection"/> the <see cref="SqliteCommand"/> should be executed on.</param>
		/// <param name="transaction">The <see cref="SqliteTransaction"/> the stored procedure execution should participate in.</param>
		/// <param name="commandText">The name of the stored procedure to execute.</param>
		/// <param name="parameters">The parameters of the stored procedure.</param>
		/// <returns>An initialized <see cref="SqliteCommand"/> instance.</returns>
		private static SqliteCommand CreateCommand(SqliteConnection connection, SqliteTransaction transaction, string commandText, ArrayList parameters)
		{
			SqliteCommand command = new SqliteCommand();

			command.Connection = connection;
			command.CommandText = commandText;
			command.CommandTimeout = COMMAND_TIMEOUT;
			command.CommandType = CommandType.Text;
			command.Transaction = transaction;

			// Append each parameter to the command
			foreach (SqliteParameter parameter in parameters)
			{
				command.Parameters.Add(CheckParameter(parameter));

			}

			return command;
		}
		#endregion CreateCommand

		private static void OpenDatabase(ref SqliteConnection connection)
		{
			Int32 nTry = 0;
			while (true)
			{
				try
				{
					connection.Open();

					break;
				}
				catch (SqliteException sex)
				{
					nTry++;

					if (nTry > 10)
						throw sex;
				}
				catch (Exception ex)
				{
					nTry++;

					if (nTry > 10)
						throw ex;
				}

				Thread.Sleep(2000);
			}
		}
		#endregion

		#region Exception functions
		/// <summary>
		/// Determines if the specified exception is the result of a foreign key violation.
		/// </summary>
		/// <param name="e">The exception to check.</param>
		/// <returns><code>true</code> if the exception is a foreign key violation, otherwise <code>false</code>.</returns>
		public static bool IsForeignKeyContraintException(Exception e)
		{
			SqliteException sqlex = e as SqliteException;

			if (sqlex != null && sqlex.ErrorCode == 547)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Determines if the specified exception is the result of a unique constraint violation.
		/// </summary>
		/// <param name="e">The exception to check.</param>
		/// <returns><code>true</code> if the exception is a unique constraint violation, otherwise <code>false</code>.</returns>
		public static bool IsUniqueConstraintException(Exception e)
		{
			SqliteException sqlex = e as SqliteException;

			if (sqlex != null && (sqlex.ErrorCode == 2627 || sqlex.ErrorCode == 2601))
			{
				return true;
			}

			return false;
		}
		#endregion
	}
}
