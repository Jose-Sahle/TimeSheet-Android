/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 07/03/2019 21:26:05
**********************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using SHL.Data.Base;
using SHL.Task.Model;

namespace SHL.TimeSheet.Model
{
	[Serializable]
	public sealed partial class TIMESHEET : BaseModel<TIMESHEET>
	{
		
		//Create the Constructors/Destructors methods
		#region [ Constructors/Destructors ]
		public TIMESHEET()
		{
			//SHLSTUDIO_USER_AREA_START_1
			TASKS = new List<TASK>();
			//SHLSTUDIO_USER_AREA_END_1	
		}

		public TIMESHEET(IDataReader reader) : base(reader)
		{
			//SHLSTUDIO_USER_AREA_START_2
			TASKS = new List<TASK>();
			//SHLSTUDIO_USER_AREA_END_2
		}
		#endregion

		// Create the class members variables
		#region [ Private Members ]
		Nullable<Int64> _id_timesheet;
		Nullable<DateTime> _date_rg;
		Nullable<DateTime> _date_rg_end;
		String _start_am;
		String _end_am;
		String _start_pm;
		String _end_pm;
		String _description;
		#endregion

		#region [ Properties ]
		public Nullable<Int64> ID_TIMESHEET
		{
			get { return _id_timesheet; }
			set { _id_timesheet = value; }
		}		
			
		public Nullable<DateTime> DATE_RG
		{
			get { return _date_rg; }
			set { _date_rg = value; }
		}

		public Nullable<DateTime> DATE_RG_END
		{
			get { return _date_rg_end; }
			set { _date_rg_end = value; }
		}

		public String START_AM
		{
			get { return _start_am; }
			set { _start_am = value; }
		}		
		public String END_AM
		{
			get { return _end_am; }
			set { _end_am = value; }
		}		
		public String START_PM
		{
			get { return _start_pm; }
			set { _start_pm = value; }
		}		
		public String END_PM
		{
			get { return _end_pm; }
			set { _end_pm = value; }
		}		
		public String DESCRIPTION
		{
			get { return _description; }
			set { _description = value; }
		}
		#endregion

		//SHLSTUDIO_USER_AREA_START_3
		#region [TASKS]
		public List<TASK> TASKS	{ get; set;	}
		#endregion
		//SHLSTUDIO_USER_AREA_END_3		
	}
}

