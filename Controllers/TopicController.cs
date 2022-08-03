using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyDiary;
using StudyWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public async Task<IActionResult> AddTopic()
        {
            return View();
        }

        public async Task<IActionResult> CreateTopic(Topic topic)
        {
            using (db)
            {
                if(topic.TopicCompletionDate.CompareTo(DateTime.Now.AddHours(topic.TopicEstimatedTimeToMaster)) < 0)
                    topic.TopicCompletionDate = DateTime.Now.AddHours(topic.TopicEstimatedTimeToMaster+1);
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

        public async Task<IActionResult> MarkAsDone(int topicId)
        {
            using (db)
            {
                var topic = db.Topics.Where(x => x.TopicId == topicId).FirstOrDefault();
                if (!topic.TopicIsDone)
                {
                    topic.TopicIsDone = true;
                    db.Topics.Update(topic);
                }
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Pause(int topicId)
        {
            using (db)
            {
                var topic = db.Topics.Where(x => x.TopicId == topicId).FirstOrDefault();
                if (topic.TopicInProgress)
                {
                    topic.TopicInProgress = false;
                    db.Topics.Update(topic);
                }
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Start(int topicId)
        {
            using (db)
            {
                var topic = db.Topics.Where(x => x.TopicId == topicId).FirstOrDefault();
                if (!topic.TopicIsDone && !topic.TopicInProgress)
                {
                    topic.TopicInProgress = true;
                    topic.TopicStartLearningDate = DateTime.Now;
                    db.Topics.Update(topic);
                }
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            ViewModel data = new ViewModel();
            await using (db)
            {
                List<int> topicIndex = new List<int>();
                List<int> taskIndex = new List<int>();
                
                data.Topics = db.Topics.Where(x => x.TopicTitle.Contains(searchTerm)).ToList();
                
                data.Topics.ForEach(x => topicIndex.Add(x.TopicId));

                data.Tasks = db.Tasks.Select(x => x).Where(x => topicIndex.Contains(x.TopicId)).ToList();

                data.Tasks.ForEach(x => taskIndex.Add(x.TaskId));

                data.Notes = db.Notes.Select(x => x).Where(x => taskIndex.Contains(x.TaskId)).ToList();
            
            }
            return View("SearchResults", data);
        }

        public async Task<IActionResult> RemoveTask(int taskId)
        {
            using (db)
            {
                db.Tasks.Remove(db.Tasks.Where(x => x.TaskId == taskId).FirstOrDefault());
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        } 
        
        public async Task<IActionResult> DeleteNote(int noteId)
        {
            using (db)
            {
                db.Notes.Remove(db.Notes.Where(x => x.Id == noteId).FirstOrDefault());
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> EditTopicPartial(ViewModel vm)
        {

            using (db)
            {
                var topic = db.Topics.Where(x => x.TopicId == vm.Topic.TopicId).FirstOrDefault();

                if (!String.IsNullOrWhiteSpace(vm.Topic.TopicTitle))
                    topic.TopicTitle = vm.Topic.TopicTitle;
                if (!String.IsNullOrWhiteSpace(vm.Topic.TopicDescription))
                    topic.TopicDescription = vm.Topic.TopicDescription;
                if (!String.IsNullOrWhiteSpace(vm.Topic.TopicEstimatedTimeToMaster.ToString()) && double.TryParse(vm.Topic.TopicEstimatedTimeToMaster.ToString(), out double result))
                    topic.TopicEstimatedTimeToMaster = vm.Topic.TopicEstimatedTimeToMaster;
                if (!String.IsNullOrWhiteSpace(vm.Topic.TopicSource))
                    topic.TopicSource = vm.Topic.TopicSource;
                if (vm.Topic.TopicCompletionDate != null && vm.Topic.TopicCompletionDate != topic.TopicCompletionDate && vm.Topic.TopicCompletionDate.CompareTo(DateTime.Now.AddHours(topic.TopicEstimatedTimeToMaster)) > 0)
                    topic.TopicCompletionDate = vm.Topic.TopicCompletionDate;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> AddTask(ViewModel vm)
        {
            using (db)
            {
                var task = new Models.Task();
                task.TaskTitle = (!String.IsNullOrWhiteSpace(vm.Task.TaskTitle)) ? vm.Task.TaskTitle : "No title";
                task.TaskPriority = vm.Task.TaskPriority;
                task.TaskDone = vm.Task.TaskDone;
                task.TopicId = vm.Task.TopicId;
                db.Tasks.Add(task);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> AddNote(ViewModel vm)
        {
            using (db)
            {
                var note = new Note();
                note.TaskId = vm.Note.TaskId;
                note.Note1 = (!String.IsNullOrWhiteSpace(vm.Note.Note1))? vm.Note.Note1 : "undefined";
                db.Notes.Add(note);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        public ViewResult Help()
        {
            return View();
        }


    }
}
