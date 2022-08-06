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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpanseTrackerUnitOfWork UnitOfWork;
        private readonly UserMapper UserMapper;
        private readonly UserService Service;
        //private readonly ExpanseTrackerContext Context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            UnitOfWork = new ExpanseTrackerUnitOfWork();
            UserMapper = new UserMapper();
            Service = new UserService(UnitOfWork, UserMapper);
            //Context = context;
        }

        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel userModel)
        {
            try
            {
                Service.VerifyPasswords(userModel.Password, userModel.RepeatPassword);
            }
            catch
            {
                Debug.WriteLine("TO TRZEBA DOPISAĆ");
            }

            UserDto user = new UserDto();
            user.Login = userModel.Login;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Password = userModel.Password;

            try
            {
                Service.CreateUser(user);
            }
            catch
            {
                Debug.WriteLine("TO TRZEBA DOPISAĆ");
            }


            return View("Index");
        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserModel userModel)
        {
            //TYMCZASOWE    TYMCZASOWE  TYMCZASOWE  TYMCZASOWE  TYMCZASOWE  TYMCZASOWE  TYMCZASOWE  TYMCZASOWE  TYMCZASOWE  TYMCZASOWE  TYMCZASOWE
            try
            {
                User user = Service.Login(userModel.Login, userModel.Password);
            }
            catch
            {
                Debug.WriteLine("TO TRZEBA DOPISAĆ");
                User user = new User(new Guid(), new SimpleEventPublisher());
                return RedirectToAction("Main", "Account", user);
            }


            //return RedirectToAction("Main", "Account");
            return View();
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
