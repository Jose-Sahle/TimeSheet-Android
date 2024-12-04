using System;

using System.Data;

using SHL.Data.Base;

namespace SHL.Settings.Model
{
    /// <summary>
    /// Objeto de parâmetros
    /// </summary>
	[Serializable]
    public sealed partial class SETTINGS : BaseModel<SETTINGS>
    {
		#region [ Constructors/Destructors ]
		public SETTINGS()
		{
		}

		public SETTINGS(IDataReader reader) : base(reader)
		{
		}
		#endregion
		public string KEY { get; set; }
        public string VALUE { get; set; }
    }
}
