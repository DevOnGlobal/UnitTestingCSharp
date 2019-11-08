using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Models;
using VotingApp.Services;

namespace VotingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVoteService voteService;

        public HomeController(IVoteService voteService)
        {
            this.voteService = voteService;
        }

        public IActionResult Index()
        {
            var votes = voteService.GetVotes();

            var viewModel = new VoteViewModel
            {
                Yes = votes.Count(v => v.Choice == "Yes"),
                No = votes.Count(v => v.Choice == "No"),
                Winner = voteService.GetWinner()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Vote([Bind("Choice")] Vote vote)
        {
            // Try to save vote
            voteService.Add(vote);

            // Always redirect to Index
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset()
        {
            // Try to reset election
            voteService.Reset();

            // Always redirect to Index
            return RedirectToAction(nameof(Index));
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
