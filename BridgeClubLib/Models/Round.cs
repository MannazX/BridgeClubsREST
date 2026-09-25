using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeClubLib.Models
{
	public class Round
	{
		#region Properties
		public int RoundID { get; set; }
		public int SectionID { get; set; }
		public int? RoundNo { get; set; }
		public int? HalfNo { get; set; }
		public int? TableNo { get; set; }
		public int? BoardSet { get; set; }
		public int? BoardSpec { get; set; }
		public int? NorthTeamNo { get; set; }
		public int? SouthTeamNo { get; set; }
		public int? EastTeamNo { get; set; }
		public int? WestTeamNo { get; set; }
		#endregion

		#region Constructor
		public Round()
		{
			
		}
		public Round(int roundId, int sectionId, int? roundNo, int? halfNo, int? tableNo, int? boardSet, int? boardSpec, int? northTeamNo, int? southTeamNo, int? eastTeamNo, int? westTeamNo)
		{
			RoundID = roundId;
			SectionID = sectionId;
			RoundNo = roundNo;
			HalfNo = halfNo;
			TableNo = tableNo;
			BoardSet = boardSet;
			BoardSpec = boardSpec;
			NorthTeamNo = northTeamNo;
			SouthTeamNo = southTeamNo;
			EastTeamNo = eastTeamNo;
			WestTeamNo = westTeamNo;
		}
		#endregion
	}
}
