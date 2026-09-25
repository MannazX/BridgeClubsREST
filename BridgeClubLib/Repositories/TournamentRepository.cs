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
		private string selectSql = "SELECT M.ID AS MAINTOURNAMENTID, G.ID AS GROUPTOURNAMENTID, M.NAME, M.TOURNAMENTFORM, M.FKCLUBID, M.STRENGTHGROUPCOUNT, M.NUMBEROFGROUPS, M.NUMBEROFPLAYINGDAYS, G.GROUPNO, G.TOURNAMENTTYPE, G.NUMBEROFTEAMS, G.NUMBEROFSECTIONS, G.NUMBEROFROUNDS, G.NUMBEROFTABLES, G.BOARDSPERROUND, G.HALVESPERMATCH FROM MAINTOURNAMENT M JOIN GROUPTOURNAMENT G ON G.FKMAINTOURNAMENTID = M.ID";

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
						using (FbDataReader reader = (FbDataReader) await command.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								int mainTournamentId = reader.GetInt32("MAINTOURNAMENTID");
								int groupTournementId = reader.GetInt32("GROUPTOURNAMENTID");
								string? name = reader.IsDBNull("M.NAME") ? null : reader.GetString("M.NAME");
								int? tournementForm = reader.IsDBNull("M.TOURNAMENTFORM") ? null : reader.GetInt32("M.TOURNAMENTFORM");
								int? clubId = reader.IsDBNull("M.FKCLUBID") ? null : reader.GetInt32("M.FKCLUBID");
								int? strengthCount = reader.IsDBNull("M.STRENGTHGROUPCOUNT") ? null : reader.GetInt32("M.STRENGTHGROUPCOUNT");
								int? numberOfGroups = reader.IsDBNull("M.NUMBEROFGROUPS") ? null : reader.GetInt32("M.NUMBEROFGROUPS");
								int? numberOfPlayingDays = reader.IsDBNull("M.NUMBEROFPLAYINGDAYS") ? null : reader.GetInt32("M.NUMBEROFPLAYINGDAYS");
								int? groupNo = reader.IsDBNull("G.GROUPNO") ? null : reader.GetInt32("G.GROUPNO");
								int? tournamentType = reader.IsDBNull("G.TOURNAMENTTYPE") ? null : reader.GetInt32("G.TOURNAMENTTYPE");
								int? numberOfTeams = reader.IsDBNull("G.NUMBEROFTEAMS") ? null : reader.GetInt32("G.NUMBEROFTEAMS");
								int? numberOfSections = reader.IsDBNull("G.NUMBEROFSECTIONS") ? null : reader.GetInt32("G.NUMBEROFSECTIONS");
								int? numberOfRounds = reader.IsDBNull("G.NUMBEROFROUNDS") ? null : reader.GetInt32("G.NUMBEROFROUNDS");
								int? numberOfTables = reader.IsDBNull("G.NUMBEROFTABLES") ? null : reader.GetInt32("G.NUMBEROFTABLES");
								int? boardsPerRound = reader.IsDBNull("G.BOARDSPERROUND") ? null : reader.GetInt32("G.BOARDSPERROUND");
								int? halvesPerMatch = reader.IsDBNull("G.HALVESPERMATCH") ? null : reader.GetInt32("G.HALVESPERMATCH");
								Tournament tournament = new Tournament(mainTournamentId, groupTournementId, name, tournementForm, clubId, strengthCount, numberOfGroups, numberOfPlayingDays, groupNo, tournamentType, numberOfTeams, numberOfSections, numberOfRounds, numberOfTables, boardsPerRound, halvesPerMatch);
								tournaments.Add(tournament);
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
