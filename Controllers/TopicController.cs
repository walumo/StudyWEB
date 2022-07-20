﻿using Microsoft.AspNetCore.Mvc;
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
            List<Topic> topics;
            using(StudyDiaryContext db = new StudyDiaryContext())
            {
                topics = db.Topics.Select(x => x).ToList();
            }

            ViewData["topics"] = topics;
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
