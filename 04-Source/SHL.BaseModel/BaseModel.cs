using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace SHL.Data.Base
{
	/// <summary>
	/// Base model.
	/// </summary>
	public abstract class BaseModel<T>
	{
		#region [ Constructors ] 
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SHL.TokenSecurity.Model.BaseModel`1"/> class.
		/// </summary>
		public BaseModel()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SHL.TokenSecurity.Model.BaseModel`1"/> class.
		/// </summary>
		/// <param name="dataReader">Data reader.</param>
		public BaseModel(IDataReader dataReader)
		{
			this.LoadProperties(dataReader);
		}
		#endregion

		#region [ Load Methods]
		private void LoadProperties(IDataReader dataReader)
		{
			String ColumnName = String.Empty;

			try
			{
				//map all class properties
				PropertyInfo[] properties = this.GetType().GetProperties();

				//map the fields inside the DataReader
				string[] fieldsDr = new string[dataReader.FieldCount + 1];
				for (int i = 0; i <= dataReader.FieldCount - 1; i++)
				{
					fieldsDr[i] = dataReader.GetName(i);
				}

				foreach (PropertyInfo propertyInfo in properties)
				{
					if (propertyInfo.PropertyType.IsClass & propertyInfo.PropertyType.Namespace == typeof(T).Namespace)
					{
						object child = null;

						child = Activator.CreateInstance(propertyInfo.PropertyType, dataReader);
						propertyInfo.SetValue(this, child, null);
					}
					else
					{
						if (Array.IndexOf(fieldsDr, propertyInfo.Name) >= 0)
						{
							ColumnName = propertyInfo.Name;

							if (!dataReader[propertyInfo.Name].Equals(System.DBNull.Value))
							{
								String value = dataReader[propertyInfo.Name].ToString();
								try
								{
									propertyInfo.SetValue(this, dataReader[propertyInfo.Name], null);
								}
								catch (ArgumentException)
								{
									//verifica se o campo foi preenchido com espa??os em branco.
									if (!string.IsNullOrEmpty(dataReader[propertyInfo.Name].ToString()))
									{										
										propertyInfo.SetValue(this, Convert.ChangeType(dataReader[propertyInfo.Name], Nullable.GetUnderlyingType(propertyInfo.PropertyType)), null);
									}
								}
							}
						}
					}

				}
			}
			catch (Exception ex)
			{
				String msg = String.Format("BaseModel: ColumnaName{0} \n\nError: {1}", ColumnName, ex.Message);
				Exception exto = new Exception(msg);
				throw exto;
			}
		}
		#endregion
	}
}