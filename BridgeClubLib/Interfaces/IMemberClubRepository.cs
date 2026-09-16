using BridgeClubLib.Models;

namespace BridgeClubLib.Interfaces
{
	public interface IMemberClubRepository
	{
		Task<IEnumerable<MemberClub>> GetMemberClubsAsync();
	}
}