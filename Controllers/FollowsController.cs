using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers
{
	public class FollowsController : ApiController
	{
		private readonly ApplicationDbContext _context;

		public FollowsController()
		{
			_context = new ApplicationDbContext();
		}

		[Authorize]
		[HttpPost]
		public IHttpActionResult Follow(FollowDto dto)
		{
			var userId = User.Identity.GetUserId();

			if(_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
			{
				return BadRequest("Already Following.");
			}

			var following = new Following
			{
				FollowerId = userId,
				FolloweeId = dto.FolloweeId
			};

			_context.Followings.Add(following);
			_context.SaveChanges();

			return Ok();
		}


	}
}
