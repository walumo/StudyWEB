using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyWEB.Controllers
{
    public class TopicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddTopic()
        {
            return View();
        }

        public IActionResult ManageTopics()
        {
            return View();
        }
    }
}
