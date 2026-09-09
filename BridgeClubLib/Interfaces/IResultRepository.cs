using BridgeClubLib.Models;

namespace BridgeClubLib.Interfaces
{
	public interface IResultRepository
	{
		Task<IEnumerable<Result>> GetResultsAsync();
	}
}