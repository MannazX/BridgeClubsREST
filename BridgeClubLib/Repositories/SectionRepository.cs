using BridgeClubLib.Models;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeClubLib.Repositories
{
	public class SectionRepository
	{
		private string connectString;
		private string selectSql = "SELECT S.ID AS SECTIONID, G.ID AS GROUPTOURNAMENTID, G.FKMAINTOURNAMENTID AS MAINTOURNAMENTID, S.SECTIONNO, S.STARTTIME, S.ENDTIME, S.STARTROUNDNO, S.ENDROUNDNO, S.WAVESTARTINDEX, S.WAVELENGTH, S.WAVEEQWAVEINDEX, S.PAYMENTSTATUS, S.SECTIONSCOREVALID, S.TOTALSCOREVALID FROM GROUPTOURNAMENT G JOIN SECTION S ON G.ID = S.FKGROUPTOURNAMENTID";

		public SectionRepository(string fdbFileNo)
		{
			connectString = new Secret(fdbFileNo).ConnectionString;
		}

		public async Task<IEnumerable<Section>> GetSectionsAsync()
		{
			List<Section> sections = new List<Section>();
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
								int sectionId = reader.GetInt32("SECTIONID");
								int groupTournamentId = reader.GetInt32("GROUPTOURNAMENTID");
								int mainTournamentId = reader.GetInt32("MAINTOURNAMENTID");
								int? sectionNo = reader.IsDBNull("S.SECTIONNO") ? null : reader.GetInt32("S.SECTIONNO");
								DateTime? startTime = reader.IsDBNull("S.STARTTIME") ? null : reader.GetDateTime("S.STARTTIME");
								DateTime? endTime = reader.IsDBNull("S.ENDTIME") ? null : reader.GetDateTime("S.ENDTIME");
								int? startRoundNo = reader.IsDBNull("S.STARTROUNDNO") ? null : reader.GetInt32("S.STARTROUNDNO");
								int? endRoundNo = reader.IsDBNull("S.ENDROUNDNO") ? null : reader.GetInt32("S.ENDROUNDNO");
								int? waveStartIndex = reader.IsDBNull("S.WAVESTARTINDEX") ? null : reader.GetInt32("S.WAVESTARTINDEX");
								int? waveLength = reader.IsDBNull("S.WAVELENGTH") ? null : reader.GetInt32("S.WAVELENGTH");
								int? waveEqWaveIndex = reader.IsDBNull("S.WAVEEQWAVEINDEX") ? null : reader.GetInt32("S.WAVEEQWAVEINDEX");
								int? paymentStatus = reader.IsDBNull("S.PAYMENTSTATUS") ? null : reader.GetInt32("S.PAYMENTSTATUS");
								int? sectionScoreValid = reader.IsDBNull("S.SECTIONSCOREVALID") ? null : reader.GetInt32("S.SECTIONSCOREVALID");
								int? totalScoreValid = reader.IsDBNull("S.TOTALSCOREVALID") ? null : reader.GetInt32("S.TOTALSCOREVALID");
								Section section = new Section(sectionId, groupTournamentId, mainTournamentId, sectionNo, startTime, endTime, startRoundNo, endRoundNo, waveStartIndex, waveLength, waveEqWaveIndex, paymentStatus, sectionScoreValid, totalScoreValid);
								sections.Add(section);
							}
						}
					}
				}
				catch (FbException fbEx)
				{
					Console.WriteLine("Firebird DB Error - " + fbEx.Message);
				}
			}
			return sections;
		}
	}
}
