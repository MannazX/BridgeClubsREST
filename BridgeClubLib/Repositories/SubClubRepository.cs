using BridgeClubLib.Interfaces;
using BridgeClubLib.Models;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeClubLib.Repositories
{
	public class SubClubRepository : ISubClubRepository
	{
		private string connectString;
		private string selectSql = "SELECT CLUB_ID, ORG_CLUB_ID, CLUB_NAME, LEADER_ID, GAME_DAY, NO_SMOKING, IS_VISIBLE, REMOTE_PROFILE_ID, LAST_CHANGED_BY, GAME_TIME FROM MEM_CLUB";

		public SubClubRepository(string fdbFileNo)
		{
			connectString = new Secret(fdbFileNo).ConnectionString;
		}

		public async Task<IEnumerable<SubClub>> GetSubClubsAsync()
		{
			List<SubClub> subClubs = new List<SubClub>();
			using (FbConnection connect = new FbConnection(connectString))
			{
				try
				{
					await connect.OpenAsync();
					using (FbCommand command = new FbCommand(selectSql, connect))
					{
						using (FbDataReader reader = (FbDataReader) await command.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								int clubId = reader.GetInt32("CLUB_ID");
								int? orgClubId = reader.IsDBNull("ORG_CLUB_ID") ? null : reader.GetInt32("ORG_CLUB_ID");
								string? clubName = reader.IsDBNull("CLUB_NAME") ? null : reader.GetString("CLUB_NAME");
								int? leaderId = reader.IsDBNull("LEADER_ID") ? null : reader.GetInt32("LEADER_ID");
								int? gameDay = reader.IsDBNull("GAME_DAY") ? null : reader.GetInt32("GAME_DAY");
								string? noSmoking = reader.IsDBNull("NO_SMOKING") ? null : reader.GetString("NO_SMOKING");
								string? isVisible = reader.IsDBNull("IS_VISIBLE") ? null : reader.GetString("IS_VISIBLE");
								int? remoteProfileId = reader.IsDBNull("REMOTE_PROFILE_ID") ? null : reader.IsDBNull("REMOTE_PROFILE_ID") ? null : reader.GetInt32("REMOTE_PROFILE_ID");
								string? lastChangedBy = reader.IsDBNull("LAST_CHANGED_BY") ? null : reader.GetString("LAST_CHANGED_BY");
								TimeOnly? gameTime = reader.IsDBNull(reader.GetOrdinal("GAME_TIME")) ? null : TimeOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("GAME_TIME")));
								SubClub subClub = new SubClub(clubId, orgClubId, clubName, leaderId, gameDay, noSmoking, isVisible, remoteProfileId, lastChangedBy, gameTime);
								subClubs.Add(subClub);
							}
						}
					}
				}
				catch (FbException fbEx)
				{
					Console.WriteLine("Firebird DB Error - ", fbEx.Message);
				}
			}
			return subClubs;
		}
	}
}
