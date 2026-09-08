using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeClubLib.Models
{
	public class Tournament
	{
		#region Properties
		public int TournamentID { get; set; }
		public int? MainTournamentID { get; set; }
		public int? GroupNo { get; set; }
		public int? TournamentType { get; set; }
		public int? NumberOfTeams { get; set; }
		public int? NumberOfSections { get; set; }
		public int? NumberOfRounds { get; set; }
		public int? NumberOfTables { get; set; }
		public int? BoardsPerRound { get; set; }
		public int? HalvesPerMatch { get; set; }
		#endregion

		#region Constructor
		public Tournament()
		{
			
		}
		public Tournament(int tournamentId, int? mainTournamentId, int? groupNo, int? tournamentType, int? numberOfTeams, int? numberOfSections, int? numberOfRounds, int? numberOfTables, int? boardsPerRound, int halvesPerMatch)
		{
			TournamentID = tournamentId;
			MainTournamentID = mainTournamentId;
			GroupNo = groupNo;
			TournamentType = tournamentType;
			NumberOfTeams = numberOfTeams;
			NumberOfSections = numberOfSections;
			NumberOfRounds = numberOfRounds;
			NumberOfTables = numberOfTables;
			BoardsPerRound = boardsPerRound;
			HalvesPerMatch = halvesPerMatch;
		}
		#endregion
	}
}
