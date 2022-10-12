﻿using CardGameDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CardGameDemo.Controllers
{
	public class HomeController : Controller
	{

		private readonly ILogger<HomeController> _logger;
		// This worked at first, but we had all sorts of problems.
		// We only had one deck shared across all users of our site.
		// Let's give each user their own deck.
		// Instead of keeping a single, static deck ID, we'll 
		// "pass it around" through our views and links.
		//static public string DeckId = "";

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		async public Task<IActionResult> Index()
		{
			// Old code before refactoring with CardAPI DAL
			//HttpClient web = new HttpClient();
			//web.BaseAddress = new Uri("https://www.deckofcardsapi.com/api/deck/");

			//// First use of our HttpClient instance
			//var connection = await web.GetAsync("new/shuffle/?deck_count=1");
			//CardResponse resp = await connection.Content.ReadAsAsync<CardResponse>();

			////DeckId = resp.deck_id; // We're not doing this after all.

			//// Second use of our HttpClient instance
			//connection = await web.GetAsync($"{resp.deck_id}/draw/?count=5");
			//resp = await connection.Content.ReadAsAsync<CardResponse>();

			string deck_id = await CardAPI.GetNewDeck();
			CardResponse resp = await CardAPI.GetCards(deck_id, 5);

			return View(resp);
		}

		async public Task<IActionResult> DrawFive(string deck_id)
		{
			// Old code before refactoring with CardAPI DAL
			//HttpClient web = new HttpClient();
			//web.BaseAddress = new Uri("https://www.deckofcardsapi.com/api/deck/");
			//var connection = await web.GetAsync($"{deck_id}/draw/?count=5");
			//if (connection.StatusCode.ToString() == "NotFound")
			//{
			//	return Content(connection.RequestMessage.RequestUri.OriginalString);
			//}
			//CardResponse resp = await connection.Content.ReadAsAsync<CardResponse>();
			CardResponse resp = await CardAPI.GetCards(deck_id, 5);
			return View("index", resp);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}