﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata.Internal;
using Rotativa.AspNetCore;
using ScientificReportData.Models;
using ScientificReportServices;

namespace ScientificReport.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly UserManager<User> _userManager;
        public static DateTime st;
        public static DateTime nd;
		public ReportController(IReportService reportService, IUserService userService, UserManager<User> userManager)
        {
            this._reportService = reportService;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
	        var currentUser = await _userManager.GetUserAsync(User);
			return View(currentUser);
        }

        public async Task<IActionResult> CreateReportByDateAsync(string start, string end)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var report = _reportService.CreateReport(currentUser);
            st = start != null ? st = Convert.ToDateTime(start) : st = new DateTime(1, 1, 1);
            nd = end != null ? nd = Convert.ToDateTime(end) : nd = new DateTime(2150, 12, 12); 
            var viewModel = _reportService.CreateViewModel(currentUser,st,nd);

            return new ViewAsPdf("CreateReport", viewModel) { FileName = "Report.pdf" };
        }

        public async Task<IActionResult> CreateReport()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var report = _reportService.CreateReport(currentUser);
            if (nd == DateTime.MinValue)
            {
                nd = new DateTime(2150, 12, 12);
            }
            var viewModel = _reportService.CreateViewModel(currentUser, st, nd);
            return new ViewAsPdf("CreateReport", viewModel) { FileName = "Report.pdf" };
        }
    }
}
