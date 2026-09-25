using BridgeClubLib.Models;

namespace BridgeClubLib.Interfaces
{
	public interface IRoundRepository
	{
		Task<List<Round>> GetRoundsAsync(int sectionId);
	}
}