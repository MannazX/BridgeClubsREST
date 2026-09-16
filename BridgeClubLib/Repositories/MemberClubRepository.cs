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
	public class MemberClubRepository : IMemberClubRepository
	{
		private string connectString;
		private string selectSql = "SELECT MEMBER_CLUB_ID, MEMBER_ID, CLUB_ID, PARTNER_ID, PARTNER_FIRST, IS_SUBSTITUTE, GROUP_INDEX, LAST_CHANGED_BY, LAST_CHANGED_DATE FROM MEM_MEMBER_CLUB";

		public MemberClubRepository(string fdbNo)
		{
			connectString = new Secret(fdbNo).ConnectionString;
		}

		public async Task<IEnumerable<MemberClub>> GetMemberClubsAsync()
		{
			List<MemberClub> memberClubs = new List<MemberClub>();
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
								int memberClubId = reader.GetInt32("MEMBER_CLUB_ID");
								int? memberId = reader.IsDBNull("MEMBER_ID") ? null : reader.GetInt32("MEMBER_ID");
								int? clubId = reader.IsDBNull("CLUB_ID") ? null : reader.GetInt32("CLUB_ID");
								int? partnerId = reader.IsDBNull("PARTNER_ID") ? null : reader.GetInt32("PARTNER_ID");
								string? partnerFirst = reader.IsDBNull("PARTNER_FIRST") ? null : reader.GetString("PARTNER_FIRST");
								string? isSubstitute = reader.IsDBNull("IS_SUBSTITUTE") ? null : reader.GetString("IS_SUBSTITUTE");
								int? groupIndex = reader.IsDBNull("GROUP_INDEX") ? null : reader.GetInt32("GROUP_INDEX");
								string? lastChangedBy = reader.IsDBNull("LAST_CHANGED_BY") ? null : reader.GetString("LAST_CHANGED_BY");
								DateTime? lastChangedDate = reader.IsDBNull("LAST_CHANGED_DATE") ? null : reader.GetDateTime("LAST_CHANGED_DATE");
								MemberClub memberClub = new MemberClub(memberClubId, memberId, clubId, partnerId, partnerFirst, isSubstitute, groupIndex, lastChangedBy, lastChangedDate);
								memberClubs.Add(memberClub);
							}
						}
					}
				}
				catch (FbException fbEx)
				{
					Console.WriteLine("Firebird DB Error - " + fbEx.Message);
				}
			}
			return memberClubs;
		}
	}
}
