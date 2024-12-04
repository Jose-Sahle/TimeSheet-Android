/**********************************************************
  AUTHOR	: Jose Sahle Netto
  VERSION	: 1.0.0.0
  DATE		: 28/03/2020 02:49:05
**********************************************************/
using System;
using System.Data;
using SHL.Data.Base;

namespace SHL.Task.Model
{

	[Serializable]
	public sealed partial class TASK : BaseModel<TASK>
	{
		//Create the Constructors/Destructors methods
		#region [ Constructors/Destructors ]
			public TASK()
			{
			}

			public TASK(IDataReader reader) : base(reader)
			{
			}
		#endregion

		// Create the class members variables
		#region [ Private Members ]
		Nullable<Int64> _ID_TASK;
		String _START_TM;
		String _END_TM;
		String _DESCRIPTION;
		String _INDICE_PROJECT;
		Nullable<Int64> _ID_TIMESHEET;
		Nullable<Int64> _ID_PROJECT;
		String _PROJECTNAME;
		#endregion

		#region [ Properties ]
		public Nullable<Int64> ID_TASK
		{
			get { return _ID_TASK; }
			set { _ID_TASK = value; }
		}				
		public String START_TM
		{
			get { return _START_TM; }
			set { _START_TM = value; }
		}		
		public String END_TM
		{
			get { return _END_TM; }
			set { _END_TM = value; }
		}		
		public String DESCRIPTION
		{
			get { return _DESCRIPTION; }
			set { _DESCRIPTION = value; }
		}
		public String INDICE_PROJECT
		{
			get { return _INDICE_PROJECT; }
			set { _INDICE_PROJECT = value; }
		}
		public Nullable<Int64> ID_TIMESHEET
		{
			get { return _ID_TIMESHEET; }
			set { _ID_TIMESHEET = value; }
		}		
		public Nullable<Int64> ID_PROJECT
		{
			get { return _ID_PROJECT; }
			set { _ID_PROJECT = value; }
		}		
		public String PROJECTNAME
		{
			get { return _PROJECTNAME; }
			set { _PROJECTNAME = value; }
		}
		#endregion
		
		//SHLSTUDIO_USER_AREA_START_1
		//SHLSTUDIO_USER_AREA_END_1
		
	}
}

