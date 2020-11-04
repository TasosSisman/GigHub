using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.apis
{
	[Authorize]
	public class NotificationsController : ApiController
	{
		private readonly ApplicationDbContext _context;

		public NotificationsController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpGet]
		public IEnumerable<NotificationDto> GetNewNotifications()
		{
			var userId = User.Identity.GetUserId();
			var notifications = _context.UserNotifications
				.Where(u => u.UserId == userId && u.IsRead == false)
				.Select(u => u.Notification)
				.Include(u => u.Gig.Artist)
				.ToList();

			

			return notifications.Select(Mapper.Map<Notification, NotificationDto>);
		}
	}
}
