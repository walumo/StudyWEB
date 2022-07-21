using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        StudyDiaryContext db = new StudyDiaryContext();
        public async Task<IActionResult> Index()
        {
            await using(db)
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

        public async Task<IActionResult> CreateTopic(Topic topic)
        {
            using (db)
            {
                db.Topics.Add(topic);
                await db.SaveChangesAsync();
            }

            ViewData["topic"] = topic;

            return View("TopicSaved");
        }
    }
}
