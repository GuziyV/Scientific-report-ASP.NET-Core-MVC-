﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ScientificReport.Data.Models;
using ScientificReport.Services;

namespace ScientificReport.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IUserService _userService;

        public HomeController(IReportService reportService, IUserService userService)
        {
            _reportService = reportService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateReport(CreateReportModel model)
        {
            var currentUser = _userService.CurrentUser;
            var report = _reportService.CreateReport(model, currentUser);

            return Ok(report);
        }
    }
}
