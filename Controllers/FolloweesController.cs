using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
		private readonly ApplicationDbContext _context;

		public FolloweesController()
		{
            _context = new ApplicationDbContext();
		}

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var followees = _context.Followings
                .Where(u => u.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();

            return View(followees);
        }
    }
}