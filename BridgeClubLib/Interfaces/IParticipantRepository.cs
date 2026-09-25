using BridgeClubLib.Models;

namespace BridgeClubLib.Interfaces
{
	public interface IParticipantRepository
	{
		Task<IEnumerable<Participant>> GetParticipantsAsync(int sectionId);
	}
}