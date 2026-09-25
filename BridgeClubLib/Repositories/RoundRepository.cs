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
	public class RoundRepository : IRoundRepository
	{
		private string connectString;
		private string selectSql = "SELECT R.ID AS ROUNDID, S.ID AS SECTIONID, R.ROUNDNO, R.HALFNO, M.TABLENO, M.BOARDSET, M.BOARDSPEC, M.NORTHTEAMNO, M.SOUTHTEAMNO, M.EASTTEAMNO, M.WESTTEAMNO FROM SECTION S JOIN ROUND R ON R.FKSECTIONID = S.ID JOIN ROUNDMATCH M ON M.FKROUNDID = R.ID WHERE S.ID = @SECTIONID";

		public RoundRepository(string fdbNo)
		{
			connectString = new Secret(fdbNo).ConnectionString;
		}

		public async Task<List<Round>> GetRoundsAsync(int sectionId)
		{
			List<Round> rounds = new List<Round>();
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
								int roundId = reader.GetInt32("ROUNDID");
								int fksectionId = reader.GetInt32("SECTIONID");
								int? roundNo = reader.IsDBNull("R.ROUNDNO") ? null : reader.GetInt32("R.ROUNDNO");
								int? halfNo = reader.IsDBNull("R.HALFNO") ? null : reader.GetInt32("R.HALFNO");
								int? tableNo = reader.IsDBNull("M.TABLENO") ? null : reader.GetInt32("M.TABLENO");
								int? boardSet = reader.IsDBNull("M.BOARDSET") ? null : reader.GetInt32("M.BOARDSET");
								int? boardSpec = reader.IsDBNull("M.BOARDSPEC") ? null : reader.GetInt32("M.BOARDSPEC");
								int? northTeamNo = reader.IsDBNull("M.NORTHTEAMNO") ? null : reader.GetInt32("M.NORTHTEAMNO");
								int? southTeamNo = reader.IsDBNull("M.SOUTHTEAMNO") ? null : reader.GetInt32("M.SOUTHTEAMNO");
								int? eastTeamNo = reader.IsDBNull("M.EASTTEAMNO") ? null : reader.GetInt32("M.EASTTEAMNO");
								int? westTeamNo = reader.IsDBNull("M.WESTTEAMNO") ? null : reader.GetInt32("M.WESTTEAMNO");
								if (fksectionId == sectionId)
								{
									Round round = new Round(roundId, sectionId, roundNo, halfNo, tableNo, boardSet, boardSpec, northTeamNo, southTeamNo, eastTeamNo, westTeamNo);
									rounds.Add(round);
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
			return rounds;
		}
	}
}
