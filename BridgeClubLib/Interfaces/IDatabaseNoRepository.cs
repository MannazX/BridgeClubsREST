namespace BridgeClubLib.Interfaces
{
	public interface IDatabaseNoRepository
	{
		IEnumerable<string> GetDatabaseNos();
	}
}