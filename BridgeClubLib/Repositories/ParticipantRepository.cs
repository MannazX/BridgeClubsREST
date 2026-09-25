using BridgeClubLib.Interfaces;
using BridgeClubLib.Models;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeClubLib.Repositories
{
	public class ParticipantRepository : IParticipantRepository
	{
		private string connectString;
		private string selectSql = "SELECT P.ID AS PARTICIPANTID, P.FKSECTIONID AS SECTIONID, P.PLAYERNAME, P.STARTHAC, P.PLAYERNO, P.PAIRNO, P.SUBSTITUTE, P.ISCAPTAIN, M.TOTAL_BRONZE, M.TOTAL_SILVER, M.TOTAL_GOLD, M.TOTAL_MASTER FROM SECTIONPLAYER P JOIN SECTIONTEAM T ON P.FKSECTIONTEAMID = T.ID JOIN MEM_MEMBER M ON P.FKPLAYERID = M.MEMBER_ID WHERE P.FKSECTIONID = @SECTIONID";

		public ParticipantRepository(string fdbNo)
		{
			connectString = new Secret(fdbNo).ConnectionString;
		}

		public async Task<IEnumerable<Participant>> GetParticipantsAsync(int sectionId)
		{
			List<Participant> participants = new List<Participant>();
			using (FbConnection connect = new FbConnection(connectString))
			{
				try
				{
					await connect.OpenAsync();
					using (FbCommand command = new FbCommand(selectSql, connect))
					{
						command.Parameters.AddWithValue("@SECTIONID", sectionId);
						using (FbDataReader reader = (FbDataReader)await command.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								int participantId = reader.GetInt32("PARTICIPANTID");
								int? fksectionId = reader.GetInt32("SECTIONID");
								string? playerName = reader.IsDBNull("P.PLAYERNAME") ? null : reader.GetString("P.PLAYERNAME");
								int? startHac = reader.IsDBNull("P.STARTHAC") ? null : reader.GetInt32("P.STARTHAC");
								int? playerNo = reader.IsDBNull("P.PLAYERNO") ? null : reader.GetInt32("P.PLAYERNO");
								int? pairNo = reader.IsDBNull("P.PAIRNO") ? null : reader.GetInt32("P.PAIRNO");
								int substitute = reader.GetInt32("P.SUBSTITUTE");
								int isCaptain = reader.GetInt32("P.ISCAPTAIN");
								int? totalBronze = reader.IsDBNull("M.TOTAL_BRONZE") ? null : reader.GetInt32("M.TOTAL_BRONZE");
								int? totalSilver = reader.IsDBNull("M.TOTAL_SILVER") ? null : reader.GetInt32("M.TOTAL_SILVER");
								int? totalGold = reader.IsDBNull("M.TOTAL_GOLD") ? null : reader.GetInt32("M.TOTAL_GOLD");
								int? totalMaster = reader.IsDBNull("M.TOTAL_MASTER") ? null : reader.GetInt32("M.TOTAL_MASTER");
								if (fksectionId != null && fksectionId == sectionId)
								{
									Participant participant = new Participant(participantId, playerName, startHac, playerNo, pairNo, substitute, isCaptain, totalBronze, totalSilver, totalGold, totalMaster);
									participants.Add(participant);
								}
							}
						}
					}
				}
				catch (FbException fbEx)
				{
					Console.WriteLine("Firebird DB Error - " + fbEx.Message);
				}
			}
			return participants;
		}
	}
}
