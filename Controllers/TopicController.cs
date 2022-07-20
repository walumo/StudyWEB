using Microsoft.AspNetCore.Mvc;
using StudyDiary;
using StudyWEB.Models;
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
            using(StudyDiaryContext db = new StudyDiaryContext())
            {
                ViewData["topics"] = db.Topics.Select(x => x).ToList();
                ViewData["tasks"] = db.Tasks.Select(x => x).ToList();
                ViewData["notes"] = db.Notes.Select(x => x).ToList();
            }
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
