using System;
using System.Collections.Generic;
using System.Linq;
using StudyWEB.Models;
using Microsoft.EntityFrameworkCore;

namespace StudyDiary
{
    static class Delete
    {
        public static void Topic()
        {
            IQueryable<Topic> topics;
            while (true)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("DELETING:\n");
                Console.BackgroundColor = ConsoleColor.Black;

                using (StudyDiaryContext db = new StudyDiaryContext())
                {
                    topics = db.Topics.Select(x => x);
                    var tIndex = (from topic in topics select topic.TopicId).ToList();

                    foreach (Topic topic in topics)
                    {
                        Console.WriteLine(topic.TopicId + ". " + topic.TopicTitle.ToUpper());
                    }
                    Console.Write("\nChoose topic to delete (blank to return, 'all' to delete all topics): ");
                    string input = Console.ReadLine();

                    if (string.Equals(input, "all", StringComparison.OrdinalIgnoreCase)) { Delete.All(); break; }

                    if (String.IsNullOrWhiteSpace(input)) return;

                    if (!String.IsNullOrWhiteSpace(input)
                        && !int.TryParse(input, out int result)
                        || !tIndex.Contains(Convert.ToInt32(input)))
                    {
                        Console.WriteLine("Invalid input!");
                        Console.ReadKey();
                    }
                    else
                    {
                        var topicToDelete = db.Topics.Where(x => x.TopicId == Convert.ToInt32(input)).First();
                        db.Entry(topicToDelete).State = EntityState.Deleted;
                        DeleteTasks(topicToDelete.TopicId);
                        db.SaveChanges();
                    }
                }
            }
        }

        private static void DeleteTasks(int topicIndex)
        {
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                var tasksToDelete = db.Tasks.Where(x => x.TopicId == topicIndex);
                foreach (Task task in tasksToDelete)
                {
                    DeleteNotes(task.TaskId);
                    db.Entry(task).State = EntityState.Deleted;
                }
                db.SaveChanges();
            }
        }
        private static void DeleteNotes(int taskIndex)
        {
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                var notesToDelete = db.Notes.Where(x => x.TaskId == taskIndex);
                foreach (Note note in notesToDelete)
                {
                    db.Entry(note).State = EntityState.Deleted;
                }
                db.SaveChanges();
            }
        }

        public static void All()
        {
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                db.Topics.RemoveRange(db.Topics.Select(x => x));
                db.SaveChanges();
            }
        }

        public static void CleanUp()
        {
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                var topicsToClean =  db.Topics.Where(topic => topic.TopicCompletionDate.CompareTo(DateTime.Now) < 0);

                if (topicsToClean.Count() < 1)
                {
                    Console.WriteLine("\nDid not find any topics with past deadlines.");
                    Console.ReadKey();
                }
                else
                {
                    foreach (Topic topic in topicsToClean)
                    {
                        db.Entry(topic).State = EntityState.Deleted;
                        DeleteTasks(topic.TopicId);
                    }
                Console.WriteLine($"\nDeleted {topicsToClean.Count()} topics with past deadline...");
                db.SaveChanges();
                Console.ReadKey();
                }
            }
        }
    }
}