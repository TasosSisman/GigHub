using GigHub.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.ViewModels
{
	public class HomeViewModel
	{
		public IEnumerable<Gig> upcomingGigs { get; set; }
		public bool ShowActions { get; set; }
	}
}