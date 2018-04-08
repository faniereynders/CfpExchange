﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CfpExchange.Models;
using CfpExchange.Data;
using System.Linq;
using CfpExchange.ViewModels;

namespace CfpExchange.Controllers
{
	public class HomeController : Controller
	{
		private readonly CfpContext _cfpContext;

		public HomeController(CfpContext cfpContext)
		{
			_cfpContext = cfpContext;
		}

		public IActionResult Index()
		{
			var indexViewModel = new IndexViewModel();

			// Set most viewed
			var maxViews = _cfpContext.Cfps.Max(cfp => cfp.Views);
			indexViewModel.MostViewedCfp = _cfpContext.Cfps.FirstOrDefault(cfp => cfp.Views == maxViews);

			// Set latest Cfp
			indexViewModel.NewestCfp = _cfpContext.Cfps.OrderByDescending(cfp => cfp.CfpAdded).FirstOrDefault();

			indexViewModel.CfpList = _cfpContext.Cfps.OrderByDescending(cfp => cfp.CfpAdded).Take(10).ToArray();

			return View(indexViewModel);
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}