using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.apis
{
	[Authorize]
    public class GigsController : ApiController
    {
		private readonly ApplicationDbContext _context;
		
		public GigsController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpDelete]
		public IHttpActionResult Cancel(int id)
		{
			var userId = User.Identity.GetUserId();
			var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

			if (gig.IsCanceled)
			{
				return NotFound();
			}

			gig.IsCanceled = true;

			var notification = new Notification
			{
				Type = NotificationType.GigCanceled,
				DateTime = DateTime.Now,
				Gig = gig
			};

			var attendeeIds = _context.Attendances
				.Where(a => a.GigId == gig.Id)
				.Select(a => a.AttendeeId)
				.ToList();

			foreach (var attendeeId in attendeeIds)
			{
				var userNotification = new UserNotification
				{
					UserId = attendeeId,
					NotificationId = notification.Id
				};
			}

			_context.SaveChanges();

			return Ok();
		}
    }
}
