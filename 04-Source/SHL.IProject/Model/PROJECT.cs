/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 28/03/2020 02:49:04
**********************************************************/
using System;
using System.Data;
using SHL.Data.Base;

namespace SHL.Project.Model
{
	[Serializable]
	public sealed partial class PROJECT : BaseModel<PROJECT>
	{
		//Create the Constructors/Destructors methods
		#region [ Constructors/Destructors ]
		public PROJECT()
		{
		}

		public PROJECT(IDataReader reader) : base(reader)
		{
		}
		#endregion

		// Create the class members variables
		#region [ Private Members ]
		Nullable<Int64> _ID_PROJECT;
		Nullable<DateTime> _START_DT;
		Nullable<DateTime> _END_DT;
		String _NAME;
		String _ALIAS;
		String _DESCRIPTION;
		#endregion

		#region [ Properties ]
		public Nullable<Int64> ID_PROJECT
		{
			get { return _ID_PROJECT; }
			set { _ID_PROJECT = value; }
		}				
		public Nullable<DateTime> START_DT
		{
			get { return _START_DT; }
			set { _START_DT = value; }
		}		
		public Nullable<DateTime> END_DT
		{
			get { return _END_DT; }
			set { _END_DT = value; }
		}		
		public String NAME
		{
			get { return _NAME; }
			set { _NAME = value; }
		}
		public String ALIAS
		{
			get { return _ALIAS; }
			set { _ALIAS = value; }
		}
		public String DESCRIPTION
		{
			get { return _DESCRIPTION; }
			set { _DESCRIPTION = value; }
		}
		#endregion

		//SHLSTUDIO_USER_AREA_START_1
		//SHLSTUDIO_USER_AREA_END_1

	}
}

