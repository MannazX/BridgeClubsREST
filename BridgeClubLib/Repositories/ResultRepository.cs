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
		private string selectSql = "SELECT RE.ID AS RESULTID, S.ID AS SECTIONID, RE.BOARDNO, RE.BOARDGROUP, RE.BIDDINGSEQUENCE, RE.CONTRACT, RE.LEAD, RE.RESULT, RE.CALCULATEDSCORENS, RE.CALCULATEDSCORENSPCT, RE.CALCULATEDSCOREEW, RE.CALCULATEDSCOREEWPCT, RE.DECLARER, RE.DOUBLING, RE.TRICKS, RE.RESULTCOMPLETED, RE.EXCLUDEGAME, RE.BOARDCOMPARED FROM SECTION S JOIN ROUND R ON R.FKSECTIONID = S.ID JOIN ROUNDMATCH M ON R.ID = M.FKROUNDID JOIN RESULT RE ON RE.FKMATCHID = M.ID WHERE S.ID = @SECTIONID";

		public ResultRepository(string fdbFileNo)
		{
			connectString = new Secret(fdbFileNo).ConnectionString;
		}

		public async Task<IEnumerable<Result>> GetResultsAsync(int sectionId)
		{
			List<Result> results = new List<Result>();
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
								int resultId = reader.GetInt32("RESULTID");
								int? fksectionId = reader.IsDBNull("SECTIONID") ? null : reader.GetInt32("SECTIONID");
								int? boardNo = reader.IsDBNull("RE.BOARDNO") ? null : reader.GetInt32("RE.BOARDNO");
								int? boardGroup = reader.IsDBNull("RE.BOARDGROUP") ? null : reader.GetInt32("RE.BOARDGROUP");
								string? contract = reader.IsDBNull("RE.CONTRACT") ? null : reader.GetString("RE.CONTRACT");
								string? lead = reader.IsDBNull("RE.LEAD") ? null : reader.GetString("RE.LEAD");
								int? result = reader.IsDBNull("RE.RESULT") ? null : reader.GetInt32("RE.RESULT");
								double? calculatedScoreNs = reader.IsDBNull("RE.CALCULATEDSCORENS") ? null : reader.GetInt32("RE.CALCULATEDSCORENS");
								double? calculatedScoreNspct = reader.IsDBNull("RE.CALCULATEDSCORENSPCT") ? null : reader.GetInt32("RE.CALCULATEDSCORENSPCT");
								double? calculatedScoreEw = reader.IsDBNull("RE.CALCULATEDSCOREEW") ? null : reader.GetInt32("RE.CALCULATEDSCOREEW");
								double? calculatedScoreEwpct = reader.IsDBNull("RE.CALCULATEDSCOREEWPCT") ? null : reader.GetInt32("RE.CALCULATEDSCOREEWPCT");
								string? declarer = reader.IsDBNull("RE.DECLARER") ? null : reader.GetString("RE.DECLARER");
								string? doubling = reader.IsDBNull("RE.DOUBLING") ? null : reader.GetString("RE.DOUBLING");
								int? tricks = reader.IsDBNull("RE.TRICKS") ? null : reader.GetInt32("RE.TRICKS");
								int? resultCompleted = reader.IsDBNull("RE.RESULTCOMPLETED") ? null : reader.GetInt32("RE.RESULTCOMPLETED");
								int? excludeGame = reader.IsDBNull("RE.EXCLUDEGAME") ? null : reader.GetInt32("RE.EXCLUDEGAME");
								int? boardCompared = reader.IsDBNull("RE.BOARDCOMPARED") ? null : reader.GetInt32("RE.BOARDCOMPARED");
								Result matchResult = new Result(resultId, sectionId, boardNo, boardGroup, contract, lead, result, calculatedScoreNs, calculatedScoreNspct, calculatedScoreEw, calculatedScoreEwpct, declarer, doubling, tricks, resultCompleted, excludeGame, boardCompared);
								results.Add(matchResult);
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
