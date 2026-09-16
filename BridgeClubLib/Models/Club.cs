using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeClubLib.Models
{
	public class Club
	{
		#region Properties
		public int ClubNo { get; set; }
		public string? Name { get; set; }
		public string? Status { get; set; }
		public string? Address { get; set; }
		public string? PostalCode { get; set; }
		public string? City { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public string? Website { get; set; }
		public int? DistrictNo { get; set; }

		#endregion

		#region Constructors
		public Club()
		{
			
		}

		public Club(int clubNo, string? name, string? status, string? address, string? postalCode, string? city, string? phone, string? email, string? website, int? districtNo)
		{
			ClubNo = clubNo;
			Name = name;
			Status = status;
			Address = address;
			PostalCode = postalCode;
			City = city;
			Phone = phone;
			Email = email;
			Website = website;
			DistrictNo = districtNo;
		}
#endregion
	}
}
