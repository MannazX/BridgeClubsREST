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
	public class ClubRepository : IClubRepository
	{
		private string connectString;
		private string selectSql = "SELECT ORG_CLUB_NO, NAME, USE_CLUB_STATUS, ADDRESS_1, ADDRESS_2, ZIP_CODE, CITY, PHONE_1, PHONE_2, EMAIL, HOMEPAGE, DISTRICT_NO FROM SYS_MAINCLUB";

		public ClubRepository(string fdbNo)
		{
			connectString = new Secret(fdbNo).ConnectionString;
		}

		public async Task<Club> GetClubAsync()
		{
			Club club = new Club();
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
								int orgClubNo = reader.GetInt32("ORG_CLUB_NO");
								string? name = reader.IsDBNull("NAME") ? null : reader.GetString("NAME");
								string? clubStatus = reader.IsDBNull("USE_CLUB_STATUS") ? null : reader.GetString("USE_CLUB_STATUS");
								string address = (reader.IsDBNull("ADDRESS_1") ? "" : reader.GetString("ADDRESS_1")) + ", " + (reader.IsDBNull("ADDRESS_2") ? "" : reader.GetString("ADDRESS_2"));
								string? postalCode = reader.IsDBNull("ZIP_CODE") ? null : reader.GetString("ZIP_CODE");
								string? city = reader.IsDBNull("CITY") ? null : reader.GetString("CITY");
								string phone = (reader.IsDBNull("NAME") ? "" : reader.GetString("NAME")) + ", " + (reader.IsDBNull("NAME") ? "" : reader.GetString("PHONE_2"));
								string? email = reader.IsDBNull("EMAIL") ? null : reader.GetString("EMAIL");
								string? website = reader.IsDBNull("HOMEPAGE") ? null : reader.GetString("HOMEPAGE");
								int? districtNo = reader.IsDBNull("DISTRICT_NO") ? null : reader.GetInt32("DISTRICT_NO");
								club = new Club(orgClubNo, name, clubStatus, address, postalCode, city, phone, email, website, districtNo);
							}
							reader.Close();
						}
					}
				}
				catch (FbException fbEx)
				{
					Console.WriteLine("Firebird DB Error - ", fbEx);
				}
			}
			return club;
		}
	}
}
