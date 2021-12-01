using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TranslationSystem.Models;
using TranslationSystem.Services;

namespace TranslationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITranslatorService _translatorService;

        public HomeController(ILogger<HomeController> logger, ITranslatorService translatorService)
        {
            _logger = logger;
            _translatorService = translatorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(TranslatorViewModel translatorViewModel)
        {
            var translatorResult = await _translatorService.Translate(translatorViewModel);

            if (translatorResult == null)
            {
                ViewBag.ErrorMessage = "A problem occured";
                return View();
            }

            return View(translatorResult);
        }


        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //ViewBag.ExceptionPath = exceptionDetails.Path;
            //ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            //ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;
            
            ErrorViewModel errorViewModel = new ErrorViewModel()
            {
                Message = exceptionDetails.Error.Message,
                StackTrace = exceptionDetails.Error.StackTrace,
                Path = exceptionDetails.Path
            };

            return View("Error", errorViewModel);
        }
    }
}
