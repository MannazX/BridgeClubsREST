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
	public class RoundRepository
	{
		private string connectString;
		private string selectSql = "SELECT R.ID AS ROUNDID, S.ID AS SECTIONID, R.ROUNDNO, R.HALFNO, M.TABLENO, M.BOARDSET, M.BOARDSPEC, M.NORTHTEAMNO, M.SOUTHTEAMNO, M.EASTTEAMNO, M.WESTTEAMNO FROM SECTION S JOIN ROUND R ON R.FKSECTIONID = S.ID JOIN ROUNDMATCH M ON M.FKROUNDID = R.ID";

		public RoundRepository(string fdbNo)
		{
			connectString = new Secret(fdbNo).ConnectionString;
		}

		public async Task<List<Round>> GetRoundsAsync()
		{
			List<Round> rounds = new List<Round>();
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
								int roundId = reader.GetInt32("ROUNDID");
								int sectionId = reader.GetInt32("SECTIONID");
								int? roundNo = reader.GetInt32("R.ROUNDNO");
								int? halfNo = reader.GetInt32("R.HALFNO");
								int? tableNo = reader.GetInt32("M.TABLENO");
								int? boardSet = reader.GetInt32("M.BOARDSET");
								int? boardSpec = reader.GetInt32("M.BOARDSPEC");
								int? northTeamNo = reader.GetInt32("M.NORTHTEAMNO");
								int? southTeamNo = reader.GetInt32("M.SOUTHTEAMNO");
								int? eastTeamNo = reader.GetInt32("M.EASTTEAMNO");
								int? westTeamNo = reader.GetInt32("M.WESTTEAMNO");
								Round round = new Round(roundId, sectionId, roundNo, halfNo, tableNo, boardSet, boardSpec, northTeamNo, southTeamNo, eastTeamNo, westTeamNo);
								rounds.Add(round);
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
