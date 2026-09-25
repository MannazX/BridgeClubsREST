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
		private string selectByClubIdSql = "SELECT ID AS MAINTOURNMENTID, NAME, DESCRIPTION, TOURNAMENTFORM, COMMONTOP, FKCLUBID, USELEADS, STRENGTHGROUPCOUNT FROM MAINTOURNAMENT WHERE FKCLUBID = @CLUBID";
		private string selectByMainTournamentIdSql = "SELECT M.ID AS MAINTOURNAMENTID, G.ID AS GROUPTOURNAMENTID, M.NAME, M.TOURNAMENTFORM, M.STRENGTHGROUPCOUNT, M.NUMBEROFGROUPS, M.NUMBEROFPLAYINGDAYS, G.GROUPNO, G.TOURNAMENTTYPE, G.NUMBEROFTEAMS, G.NUMBEROFSECTIONS, G.NUMBEROFROUNDS, G.NUMBEROFTABLES, G.BOARDSPERROUND, G.HALVESPERMATCH FROM MAINTOURNAMENT M JOIN GROUPTOURNAMENT G ON G.FKMAINTOURNAMENTID = M.ID WHERE M.ID = @MAINTOURNAMENTID";

		public TournamentRepository(string fdbFileNo)
		{
			connectString = new Secret(fdbFileNo).ConnectionString;
		}

		public async Task<IEnumerable<MainTournament>> GetMainTournamentByClubIdAsync(int clubId)
		{
			List<MainTournament> mainTournaments = new List<MainTournament>();
			using (FbConnection connect = new FbConnection(connectString))
			{
				try
				{
					await connect.OpenAsync();
					using (FbCommand command = new FbCommand(selectByClubIdSql, connect))
					{
						command.Parameters.AddWithValue("@CLUBID", clubId);
						using (FbDataReader reader = (FbDataReader) await command.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								int mainTournamentId = reader.GetInt32("MAINTOURNAMENTID");
								string? name = reader.IsDBNull("NAME") ? null : reader.GetString("NAME");
								string? description = reader.IsDBNull("DESCRIPTION") ? null : reader.GetString("DESCRIPTION");
								int? tournamentForm = reader.IsDBNull("TOURNAMENTFORM") ? null : reader.GetInt32("TOURNAMENTFORM");
								int? commonTop = reader.IsDBNull("COMMONTOP") ? null : reader.GetInt32("COMMONTOP");
								int? fkClubId = reader.IsDBNull("FKCLUBID") ? null : reader.GetInt32("FKCLUBID");
								int? useLeads = reader.IsDBNull("USELEADS") ? null : reader.GetInt32("USELEADS");
								int? numberOfPlayingDays = reader.IsDBNull("NUMBEROFPLAYINGDAYS") ? null : reader.GetInt32("NUMBEROFPLAYINGDAYS");
								if (fkClubId != null && fkClubId == clubId)
								{
									MainTournament mainTournament = new MainTournament(mainTournamentId, name, description, tournamentForm, commonTop, fkClubId, useLeads, numberOfPlayingDays);
									mainTournaments.Add(mainTournament);
								}
							}
						}
					}
				}
				catch (FbException fbEx)
				{

				}
			}
			return mainTournaments;
		}

		public async Task<IEnumerable<GroupTournament>> GetTournamentByMaintournamentIdAsync(int mainTournamentId)
		{
			List<GroupTournament> groupTournaments = new List<GroupTournament>();
			using (FbConnection connect = new FbConnection(connectString))
			{
				try
				{
					await connect.OpenAsync();
					using (FbCommand command = new FbCommand(selectByMainTournamentIdSql, connect))
					{
						command.Parameters.AddWithValue("@MAINTOURNAMENTID", mainTournamentId);
						using (FbDataReader reader = (FbDataReader) await command.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								int fkmainTournamentId = reader.GetInt32("MAINTOURNAMENTID");
								int groupTournementId = reader.GetInt32("GROUPTOURNAMENTID");
								string? name = reader.IsDBNull("M.NAME") ? null : reader.GetString("M.NAME");
								int? tournementForm = reader.IsDBNull("M.TOURNAMENTFORM") ? null : reader.GetInt32("M.TOURNAMENTFORM");
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
								if (mainTournamentId != null && fkmainTournamentId == mainTournamentId)
								{
									GroupTournament tournament = new GroupTournament(fkmainTournamentId, groupTournementId, name, tournementForm, strengthCount, numberOfGroups, numberOfPlayingDays, groupNo, tournamentType, numberOfTeams, numberOfSections, numberOfRounds, numberOfTables, boardsPerRound, halvesPerMatch);
									groupTournaments.Add(tournament);
								}
							}
						}
					}
				}
				catch (FbException fbEx)
				{
					Console.WriteLine("Firebird DB Error - " + fbEx.Message);
				}
				return groupTournaments;
			}
		}
	}
}
