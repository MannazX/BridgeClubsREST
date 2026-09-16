using BridgeClubLib.Models;

namespace BridgeClubLib.Interfaces
{
	public interface ISubClubRepository
	{
		Task<IEnumerable<SubClub>> GetSubClubsAsync();
	}
}