/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 28/03/2020 02:44:43
**********************************************************/
using System;
using System.Data;
using SHL.Data.Base;

namespace SHL.Parameter.Model
{

	[Serializable]
	public sealed partial class PARAMETER : BaseModel<PARAMETER>
	{
		//Create the Constructors/Destructors methods
		#region [ Constructors/Destructors ]
			public PARAMETER()
			{
			}

			public PARAMETER(IDataReader reader) : base(reader)
			{
			}
		#endregion

		// Create the class members variables
		#region [ Private Members ]
		String _KEY;
		String _VALUE;
		#endregion

		#region [ Properties ]
		public String KEY
		{
			get { return _KEY; }
			set { _KEY = value; }
		}				
		public String VALUE
		{
			get { return _VALUE; }
			set { _VALUE = value; }
		}		
		#endregion
		
		//SHLSTUDIO_USER_AREA_START_1
		//SHLSTUDIO_USER_AREA_END_1
		
	}
}

