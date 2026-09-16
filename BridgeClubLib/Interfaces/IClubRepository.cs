using BridgeClubLib.Models;

namespace BridgeClubLib.Interfaces
{
	public interface IClubRepository
	{
		Task<Club> GetClubAsync();
	}
}