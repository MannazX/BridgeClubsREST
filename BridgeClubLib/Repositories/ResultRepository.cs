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
	public class ResultRepository : IResultRepository
	{
		private string connectString;
		private string selectSql = "SELECT ID, FKMATCHID, BOARDNO, BOARDGROUP, CONTRACT, LEAD, RESULT, CALCULATEDSCORENS, CALCULATEDSCORENSPCT, CALCULATEDSCOREEW, CALCULATEDSCOREEWPCT, DECLARER, DOUBLING, TRICKS, RESULTCOMPLETED, EXCLUDEGAME, BOARDCOMPARED FROM RESULT";

		public ResultRepository(string fdbFileNo)
		{
			connectString = new Secret(fdbFileNo).ConnectionString;
		}

		public async Task<IEnumerable<Result>> GetResultsAsync()
		{
			List<Result> results = new List<Result>();
			using (FbConnection connect = new FbConnection(connectString))
			{
				try
				{
					await connect.OpenAsync();
					using (FbCommand command = new FbCommand(selectSql, connect))
					{
						using (FbDataReader reader = (FbDataReader)await command.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								int resultId = reader.GetInt32("ID");
								int? matchId = reader.GetInt32("FKMATCHID");
								int? boardNo = reader.GetInt32("BOARDNO");
								int? boardGroup = reader.GetInt32("BOARDGROUP");
								string? contract = reader.GetString("CONTRACT");
								string? lead = reader.GetString("LEAD");
								int? result = reader.GetInt32("RESULT");
								double? calculatedScoreNs = reader.GetInt32("CALCULATEDSCORENS");
								double? calculatedScoreNspct = reader.GetInt32("CALCULATEDSCORENSPCT");
								double? calculatedScoreEw = reader.GetInt32("CALCULATEDSCOREEW");
								double? calculatedScoreEwpct = reader.GetInt32("CALCULATEDSCOREEWPCT");
								string? declarer = reader.GetString("DECLARER");
								string? doubling = reader.GetString("DOUBLING");
								int? tricks = reader.GetInt32("TRICKS");
								int? resultCompleted = reader.GetInt32("RESULTCOMPLETED");
								int? excludeGame = reader.GetInt32("EXCLUDEGAME");
								int? boardCompared = reader.GetInt32("BOARDCOMPARED");
								Result matchResult = new Result(resultId, matchId, boardNo, boardGroup, contract, lead, result, calculatedScoreNs, calculatedScoreNspct, calculatedScoreEw, calculatedScoreEwpct, declarer, doubling, tricks, resultCompleted, excludeGame, boardCompared);
								if (!results.Contains(results.Find(x => x.ResultID == resultId)))
								{
									results.Add(matchResult);
								}
								else
								{
									throw new Exception("Result exists in the list");
								}
							}
							reader.Close();
						}
					}
				}
				catch (FbException fbEx)
				{
					Console.WriteLine("Firebird DB Error - " + fbEx.Message);
				}
			}
			return results;
		}
	}
}
