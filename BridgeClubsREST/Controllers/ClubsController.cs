using BridgeClubLib.Interfaces;
using BridgeClubLib.Models;
using BridgeClubLib.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BridgeClubsREST.Controllers
{
	[Route("api/Clubs")]
	public class ClubsController : Controller
	{
		private IMemberRepository memberRepo;
		private IClubRepository clubRepo;
		private ISubClubRepository subClubRepo;
		private IMemberClubRepository memberClubRepo;
		private IDatabaseNoRepository databaseNoRepo;
		private ITournamentRepository tournamentRepo;


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public ActionResult<IEnumerable<string>> GetDatabaseNos()
		{
			databaseNoRepo = new DatabaseNoRepository();
			IEnumerable<string> result = databaseNoRepo.GetDatabaseNos();
			if (result.Count() == 0)
			{
				return NoContent();
			}
			else
			{
				return Ok(result);
			}
		}

		[HttpGet("{fdbNo}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Club>> GetClub(string fdbNo)
		{
			clubRepo = new ClubRepository(fdbNo);
			Club result = await clubRepo.GetClubAsync();
			if (result == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(result);
			}
		}
		
		[HttpGet("{fdbNo}/SubClubs")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult<IEnumerable<SubClub>>> GetSubClub(string fdbNo)
		{
			subClubRepo = new SubClubRepository(fdbNo);
			IEnumerable<SubClub> result = await subClubRepo.GetSubClubsAsync();
			if (result.Count() == 0)
			{
				return NoContent(); 
			}
			else
			{
				return Ok(result);
			}
		}

		[HttpGet("{fdbNo}/Members")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult<IEnumerable<Member>>> GetMembers(string fdbNo)
		{
			memberRepo = new MemberRepository(fdbNo);
			IEnumerable<Member> result = await memberRepo.GetMembersAsync();
			if (result.Count() == 0)
			{
				return NoContent();
			}
			else
			{
				return Ok(result);
			}
		}

		[HttpGet("{fdbNo}/Members/{memberNo}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Member>> GetMembersByNo(string fdbNo, int memberNo)
		{
			memberRepo = new MemberRepository(fdbNo);
			Member result = await memberRepo.GetMemberByMemberNoAsync(memberNo);
			if (result == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(result);
			}
		}

		[HttpGet("{fdbNo}/MemberClubs")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult<MemberClub>> GetMemberClubs(string fdbNo)
		{
			memberClubRepo = new MemberClubRepository(fdbNo);
			IEnumerable<MemberClub> result = await memberClubRepo.GetMemberClubsAsync();
			if (result.Count() == 0)
			{
				return NoContent();
			}
			else
			{
				return Ok(result);
			}
		}

		[HttpGet("{fdbNo}/Tournaments")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult<IEnumerable<GroupTournament>>> GetTournaments(string fdbNo)
		{
			tournamentRepo = new TournamentRepository(fdbNo);
			IEnumerable<GroupTournament> result = await tournamentRepo.GetTournamentAsync();
			if (result.Count() == 0)
			{
				return NoContent();
			}
			else
			{
				return Ok(result);
			}
		}
	}
}
