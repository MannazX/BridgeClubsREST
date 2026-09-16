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
	public class TournamentRepository : ITournamentRepository
	{
		private string connectString;
		private string selectSql = "SELECT ID, NAME, TOURNAMENTFORM, COMMONTOP, FKCLUBID, INCLUDECLUBNAME, USELEADS, STRENGTHGROUPCOUNT, LAST_CHANGED_BY, LAST_CHANGED_DATE, NUMBEROFGROUPS, NUMBEROFPLAYINGDAYS FROM MAINTOURNAMENT";

		public TournamentRepository(string fdbFileNo)
		{
			connectString = new Secret(fdbFileNo).ConnectionString;
		}

		public async Task<IEnumerable<Tournament>> GetTournamentAsync()
		{
			List<Tournament> tournaments = new List<Tournament>();
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
								int tournamentId = reader.GetInt32("ID");
								string? name = reader.IsDBNull("NAME") ? null : reader.GetString("NAME");
								int? tournementForm = reader.IsDBNull("TOURNAMENTFORM") ? null : reader.GetInt32("TOURNAMENTFORM");
								int? commonTop = reader.IsDBNull("COMMONTOP") ? null : reader.GetInt32("COMMONTOP");
								int? clubId = reader.IsDBNull("FKCLUBID") ? null : reader.GetInt32("FKCLUBID");
								int? includeName = reader.IsDBNull("INCLUDECLUBNAME") ? null : reader.GetInt32("INCLUDECLUBNAME");
								int? useLeads = reader.IsDBNull("USELEADS") ? null : reader.GetInt32("USELEADS");
								int? strengthGroupCount = reader.IsDBNull("STRENGTHGROUPCOUNT") ? null : reader.GetInt32("STRENGTHGROUPCOUNT");
								string? lastChangedBy = reader.IsDBNull("LAST_CHANGED_BY") ? null : reader.GetString("LAST_CHANGED_BY");
								DateOnly? lastChangedDate = reader.IsDBNull("LAST_CHANGED_DATE") ? null : reader.IsDBNull(reader.GetOrdinal("LAST_CHANGED_DATE")) ? null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("LAST_CHANGED_DATE")));
								int? numberOfGroups = reader.IsDBNull("NUMBEROFGROUPS") ? null : reader.GetInt32("NUMBEROFGROUPS");
								int? numberOfPlayingDays = reader.IsDBNull("NUMBEROFPLAYINGDAYS") ? null : reader.GetInt32("NUMBEROFPLAYINGDAYS");
								Tournament tournament = new Tournament(tournamentId, name, tournementForm, commonTop, clubId, includeName, useLeads, strengthGroupCount, lastChangedBy, lastChangedDate, numberOfGroups, numberOfPlayingDays);
								if (!tournaments.Contains(tournaments.Find(x => x.TournamentID == tournamentId)))
								{
									tournaments.Add(tournament);
								}
								else
								{
									throw new Exception("Tournament exists in the list");
								}
							}
						}
					}
				}
				catch (FbException fbEx)
				{
					Console.WriteLine("Firebird DB Error - " + fbEx.Message);
				}
				return tournaments;
			}
		}
	}
}
