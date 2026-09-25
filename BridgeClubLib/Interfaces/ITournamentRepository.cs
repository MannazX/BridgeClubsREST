using BridgeClubLib.Models;

namespace BridgeClubLib.Interfaces
{
	public interface ITournamentRepository
	{
		Task<IEnumerable<MainTournament>> GetMainTournamentByClubIdAsync(int clubId);
		Task<IEnumerable<GroupTournament>> GetTournamentByMaintournamentIdAsync(int mainTournamentId);
	}
}