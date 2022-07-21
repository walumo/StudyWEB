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
            ViewModel data = new ViewModel();
            await using(db)
            {
                data.Topics = db.Topics.Select(x => x).ToList();
                data.Tasks = db.Tasks.Select(x => x).ToList();
                data.Notes = db.Notes.Select(x => x).ToList();
            }
            return View(data);
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

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int topicId)
        {
            using (db)
            {
                db.Topics.Remove(db.Topics.Where(x => x.TopicId == topicId).FirstOrDefault());
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
