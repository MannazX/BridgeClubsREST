using BridgeClubLib.Models;

namespace BridgeClubLib.Interfaces
{
	public interface ITournamentRepository
	{
		Task<IEnumerable<Tournament>> GetTournamentAsync();
	}
}