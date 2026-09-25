using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeClubLib.Models
{
	public class Participant
	{
		#region Properties
		public int ParticipantID { get; set; }
		public string? PlayerName { get; set; }
		public double? StartHac { get; set; }
		public int? PlayerNo { get; set; }
		public int? PairNo { get; set; }
		public int Substitute { get; set; }
		public int IsCaptain { get; set; }
		public double? TotalBronze { get; set; }
		public double? TotalSilver { get; set; }
		public double? TotalGold { get; set; }
		public double? TotalMaster { get; set; }
		#endregion

		#region Constructor
		public Participant()
		{
			
		}

		public Participant(int participantId, string? playerName, double? startHac, int? playerNo, int? pairNo, int substitute, int isCaptain, double? totalBronze, double? totalSilver, double? totalGold, double? totalMaster)
		{
			ParticipantID = participantId;
			PlayerName = playerName;
			StartHac = startHac;
			PlayerNo = playerNo;
			PairNo = pairNo;
			Substitute = substitute;
			IsCaptain = isCaptain;
			TotalBronze = totalBronze;
			TotalSilver = totalSilver;
			TotalGold = totalGold;
			TotalMaster = totalMaster;
		}
		#endregion
	}
}
