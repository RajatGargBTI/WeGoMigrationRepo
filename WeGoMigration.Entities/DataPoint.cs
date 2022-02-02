using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMigration.Entities
{
	[DataContract]
	public class DataPoint
	{
		public DataPoint(string label, int y)
		{
			this.Label = label;
			this.Y = y;
		}
		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "label")]
		public string Label = "";

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public int Y = 0;
		
	
	}

	public class DataPointForDataWise
	{
		public string Month { get; set; }

		public int TotalPost { get; set; }
	}
}
