using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.ApplicationLayer.Mappers;
using ExpanseTrackerDDD.ApplicationLayer.Services;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Factories;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.InfrastructureLayer;
using ExpanseTrackerDDD.InfrastructureLayer.Data;
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

        // domain event publisher
        private readonly SimpleEventPublisher DomainEventPublisher;
        // infrastructure service

        // event listeners

        //unitOfWork
        private readonly ExpanseTrackerUnitOfWork UnitOfWork;

        // factories
        UserFactory UserFactory;

        // domain service

        // mappers
        private readonly UserMapper UserMapper;

        // application services
        private readonly UserService Service;

        // context
        private readonly ETContext Context;

        public HomeController(ILogger<HomeController> logger, ETContext context)
        {
            _logger = logger;
            UserMapper = new UserMapper();
            DomainEventPublisher = new SimpleEventPublisher();
            UserFactory = new UserFactory(DomainEventPublisher);
            Context = context;
            UnitOfWork = new ExpanseTrackerUnitOfWork(Context);
            Service = new UserService(UnitOfWork, UserFactory);
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
                Debug.WriteLine("złe hasło - TO TRZEBA DOPISAĆ");
            }

            try
            {
                UserDto userDto = new UserDto()
                {
                    Id = new Guid(),
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Login = userModel.Login,
                    Password = userModel.Password,
                    AccountDtos = new List<AccountDto>()
                };
                try
                {
                    var a = Context.Database.CanConnect();
                    Console.WriteLine(a);
                }
                catch
                {
                    Debug.WriteLine("aaaaaaaaaaaaaaaaaaaaaaa");
                }
                Debug.WriteLine("czy to dziala?");
                Service.CreateUser(userDto);
             
            }
            catch
            {
                Debug.WriteLine("utworzenie usera - TO TRZEBA DOPISAĆ");
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
