using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.ApplicationLayer.Mappers;
using ExpanseTrackerDDD.ApplicationLayer.Services;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.InfrastructureLayer;
using ExpanseTrackerGUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExpanseTrackerGUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ExpanseTrackerUnitOfWork UnitOfWork;
        private readonly UserMapper UserMapper;
        private readonly UserService Service;
        //private readonly ExpanseTrackerContext Context;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
            UnitOfWork = new ExpanseTrackerUnitOfWork();
            UserMapper = new UserMapper();
            Service = new UserService(UnitOfWork, UserMapper);
            //Context = context;
        }
        #region Main
        public IActionResult Main(User? user)
        {
            return View();
            Debug.WriteLine("dziala lalalalal");
        }
        #endregion




        #region default
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
