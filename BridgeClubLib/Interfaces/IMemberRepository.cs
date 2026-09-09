using BridgeClubLib.Models;

namespace BridgeClubLib.Interfaces
{
	public interface IMemberRepository
	{
		Task<Member> GetMemberByMemberNoAsync(int memberNo);
		Task<IEnumerable<Member>> GetMembersAsync();
	}
}