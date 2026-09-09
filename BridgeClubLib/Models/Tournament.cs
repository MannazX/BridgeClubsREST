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
		public string? Name { get; set; }
		public int? TournamentForm { get; set; }
		public int? CommonTop { get; set; }
		public int? ClubID { get; set; }
		public int? IncludeClubName { get; set; }
		public int? UseLeads { get; set; }
		public int? StrengthGroupCount { get; set; }
		public string? LastChangedBy { get; set; }
		public DateOnly? LastChangedDate { get; set; }
		public int? NumberOfGroups { get; set; }
		public int? NumberOfPlayingDays { get; set; }
		#endregion

		#region Constructor
		public Tournament()
		{
			
		}
		public Tournament(int tournamentId, string? name, int? tournamentForm, int? commonTop, int? clubId, int? includeClubName, int? useLeads, int? strengthGroupCount, string? lastChangedBy, DateOnly? lastChangedDate, int? numberOfGroups, int? numberOfPlayingDays)
		{
			TournamentID = tournamentId;
			Name = name;
			TournamentForm = tournamentForm;
			CommonTop = commonTop;
			ClubID = clubId;
			IncludeClubName = includeClubName;
			UseLeads = useLeads;
			StrengthGroupCount = strengthGroupCount;
			LastChangedBy = lastChangedBy;
			LastChangedDate = lastChangedDate;
			NumberOfGroups = numberOfGroups;
			NumberOfPlayingDays = numberOfPlayingDays;
		}
		#endregion
	}
}
