using BridgeClubLib.Interfaces;
using BridgeClubLib.Models;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BridgeClubLib.Repositories
{
	public class MemberRepository : IMemberRepository
	{
		private string connectString;
		private string selectSql = "SELECT MEMBER_ID, MEMBER_NO, NAME, ADDRESS_1, ADDRESS_2, COUNTRY_CODE, ZIP_CODE, CITY, PHONE_1, PHONE_2, PHONE_3, EMAIL, CLUB_START, TOTAL_BRONZE, TOTAL_SILVER, TOTAL_GOLD, TOTAL_MASTER FROM MEM_MEMBER";
		private string selectByMemNoSql = "SELECT MEMBER_ID, MEMBER_NO, NAME, ADDRESS_1, ADDRESS_2, COUNTRY_CODE, ZIP_CODE, CITY, PHONE_1, PHONE_2, PHONE_3, EMAIL, CLUB_START, TOTAL_BRONZE, TOTAL_SILVER, TOTAL_GOLD, TOTAL_MASTER FROM MEM_MEMBER WHERE MEMBER_NO = @MEMBER_NO";

		public MemberRepository(string fdbFileNo)
		{
			connectString = new Secret(fdbFileNo).ConnectionString;
		}

		public async Task<IEnumerable<Member>> GetMembersAsync()
		{
			List<Member> members = new List<Member>();
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
								int memberId = reader.GetInt32("MEMBER_ID");
								int? memberNo = reader.GetInt32("MEMBER_NO");
								string? name = reader.GetString("NAME");
								string? address1 = reader.GetString("ADDRESS_1");
								string? address2 = reader.GetString("ADDRESS_2");
								string? countryCode = reader.GetString("COUNTRY_CODE");
								string? zipCode = reader.GetString("ZIP_CODE");
								string? city = reader.GetString("CITY");
								string? phone1 = reader.GetString("PHONE_1");
								string? phone2 = reader.GetString("PHONE_2");
								string? phone3 = reader.GetString("PHONE_3");
								string? email = reader.GetString("EMAIL");
								DateOnly? clubStart = reader.IsDBNull(reader.GetOrdinal("CLUB_START")) ? null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("CLUB_START")));
								double? totalBronze = reader.GetDouble("TOTAL_BRONZE");
								double? totalSilver = reader.GetDouble("TOTAL_SILVER");
								double? totalGold = reader.GetDouble("TOTAL_GOLD");
								double? totalMaster = reader.GetDouble("TOTAL_MASTER");
								Member member = new Member(memberId, memberNo, name, address1, address2, countryCode, zipCode, city, phone1, phone2, phone3, email, clubStart, totalBronze, totalSilver, totalGold, totalMaster);
								if (!members.Contains(members.Find(x => x.MemberID == memberId)))
								{
									members.Add(member);
								}
								else
								{
									throw new Exception("Member exists in the list");
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
			return members;
		}

		public async Task<Member> GetMemberByMemberNoAsync(int memberNo)
		{
			using (FbConnection connect = new FbConnection(connectString))
			{
				Member member = new Member();
				try
				{
					FbCommand command = new FbCommand(selectByMemNoSql, connect);
					command.Parameters.AddWithValue("@MEMBER_NO", memberNo);
					await command.Connection.OpenAsync();
					FbDataReader reader = (FbDataReader) await command.ExecuteReaderAsync();
					while (await reader.ReadAsync())
					{
						int memberId = reader.GetInt32("MEMBER_ID");
						string? name = reader.GetString("NAME");
						string? address1 = reader.GetString("ADDRESS_1");
						string? address2 = reader.GetString("ADDRESS_2");
						string? countryCode = reader.GetString("COUNTRY_CODE");
						string? zipCode = reader.GetString("ZIP_CODE");
						string? city = reader.GetString("CITY");
						string? phone1 = reader.GetString("PHONE_1");
						string? phone2 = reader.GetString("PHONE_2");
						string? phone3 = reader.GetString("PHONE_3");
						string? email = reader.GetString("EMAIL");
						DateOnly? clubStart = reader.IsDBNull(reader.GetOrdinal("CLUB_START")) ? null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("CLUB_START")));
						double? totalBronze = reader.GetDouble("TOTAL_BRONZE");
						double? totalSilver = reader.GetDouble("TOTAL_SILVER");
						double? totalGold = reader.GetDouble("TOTAL_GOLD");
						double? totalMaster = reader.GetDouble("TOTAL_MASTER");
						member = new Member(memberId, memberNo, name, address1, address2, countryCode, zipCode, city, phone1, phone2, phone3, email, clubStart, totalBronze, totalSilver, totalGold, totalMaster);
					}
					reader.Close();
				}
				catch (FbException fbEx)
				{
					Console.WriteLine("Firebird DB Error - " + fbEx.Message);
				}
				return member;
			}
		}
	}
}
