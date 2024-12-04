/**********************************************************
  AUTHOR	: 
  VERSION	: 1.0.0.0
  DATE		: 05/04/2020 13:05:43
**********************************************************/
using System;
using System.Data;
using SHL.Data.Base;

namespace SHL.Syncronism.Model
{
    [Serializable]
    public sealed partial class SYNCRONISM : BaseModel<SYNCRONISM>
    {
        //Create the Constructors/Destructors methods
        #region [ Constructors/Destructors ]
        public SYNCRONISM()
        {
            //SHLSTUDIO_USER_AREA_START_1
            //SHLSTUDIO_USER_AREA_END_1	
        }

        public SYNCRONISM(IDataReader reader) : base(reader)
        {
            //SHLSTUDIO_USER_AREA_START_2
            //SHLSTUDIO_USER_AREA_END_2
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public String TABLENAME { get; set; }

        /// <summary>
        /// 1 -> Insert
        /// 2 -> Update
        /// 3 -> Delete
        /// </summary>
        public Int32? OPERATION { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String DATA { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DATE_MODIFICATION { get; set; }
    }
}
