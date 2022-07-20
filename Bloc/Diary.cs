using System;
using System.Collections.Generic;
using System.Linq;
using StudyWEB.Models;

namespace StudyDiary
{
    static class Diary
    {
        public static void NewTopic()
        {
            Topic buffer = new Topic();
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                Console.Clear();
                Console.Write("Give a title for topic: ");
                buffer.TopicTitle = Console.ReadLine();

                Console.Write("Give a description for topic: ");
                buffer.TopicDescription = Console.ReadLine();

                while (true)
                {
                    Console.Write("Enter estimated time to master: ");
                    string str = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(str) || !Double.TryParse(str, out double result)) { buffer.TopicEstimatedTimeToMaster = 1; break; }
                    buffer.TopicEstimatedTimeToMaster = Convert.ToDouble(str);
                    break;
                }

                Console.Write("Enter source used if any (if not, press Enter): ");
                buffer.TopicSource = Console.ReadLine();
                buffer.TopicInProgress = false;

                while (true)
                {
                    try
                    {
                        Console.Write("Enter date for completion (dd.mm.yyyy): ");
                        string str = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(str)) { buffer.TopicCompletionDate = new DateTime(DateTime.Now.Year + 1, 1, 1); break; }
                        else
                        {
                            try
                            {
                                string[] dtParser = new string[3];
                                dtParser = str.Split('.');
                                buffer.TopicCompletionDate = new DateTime(Convert.ToInt32(dtParser[2]), Convert.ToInt32(dtParser[1]), Convert.ToInt32(dtParser[0]));
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                throw;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Date must be formatted dd.mm.yyyy, or you can leave the input blank.");
                        Console.ReadKey();
                    }
                }

                while (true)
                {
                    try
                    {
                        Console.Write("Enter time for completion (hh:mm): ");
                        string str = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(str)) { buffer.TopicCompletionDate = buffer.TopicCompletionDate.AddHours(12); break; }
                        else
                        {
                            try
                            {
                                string[] dtParser = new string[2];
                                dtParser = str.Split(':');
                                buffer.TopicCompletionDate = buffer.TopicCompletionDate.AddHours(Convert.ToDouble(dtParser[0])).AddMinutes(Convert.ToDouble(dtParser[1]));
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                throw;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Time must be formatted as HH:MM, or you can leave the input blank.");
                        Console.ReadKey();
                    }
                }
                Console.Write("Press enter to continue...");
                Console.ReadKey();
                Console.Clear();
                db.Topics.Add(buffer);
                db.SaveChanges();
            }
        }
        public static void NewTask()
        {
            Task buffer = new Task();
            string topicIndex;
            using (StudyDiaryContext db = new StudyDiaryContext())
            {
                var topics = db.Topics.Select(x => x);
                var tIndex = from topic in topics select topic.TopicId;

                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("TOPICS: ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("---------");

                foreach (Topic topic in topics)
                {
                    Console.WriteLine("{0}. {1}", topic.TopicId, topic.TopicTitle);
                }

                Console.WriteLine("Choose a topic to add notes to (blank to return): ");

                Console.Write("\nYour selection: ");
                topicIndex = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(topicIndex) || !tIndex.Contains(Convert.ToInt32(topicIndex))) return;
            }

            using (StudyDiaryContext db = new StudyDiaryContext())
            {

                buffer.TopicId = Convert.ToInt32(topicIndex);

                Console.Clear();
                Console.Write("Give a title for task: ");
                buffer.TaskTitle = Console.ReadLine();

                while (true)
                {
                    try
                    {
                        Console.Write("Priority for this task(1=high, 2=medium, 3=low); ");
                        string str = Console.ReadLine();
                        if (!String.IsNullOrWhiteSpace(str) || int.TryParse(str, out int result)) { buffer.TaskPriority = Convert.ToInt32(str); break; }
                        else buffer.TaskPriority = 3;
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Something went wrong, try again");
                    }
                }

                buffer.TaskDone = false;
                
                while (true)
                {
                    try
                    {
                        Console.Write("Enter date for completion (dd.mm.yyyy): ");
                        string str = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(str)) { buffer.TaskDeadline = new DateTime(DateTime.Now.Year + 1, 1, 1); break; }
                        else
                        {
                            string[] dtParser = new string[3];
                            dtParser = str.Split('.');
                            buffer.TaskDeadline = new DateTime(Convert.ToInt32(dtParser[2]), Convert.ToInt32(dtParser[1]), Convert.ToInt32(dtParser[0]));
                        }
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Something went wrong, try again");
                        Console.ReadKey();
                    }
                }
                db.Tasks.Add(buffer);
                db.SaveChanges();
                AddNotes();
                
                Console.Write("Press enter to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private static void AddNotes()
        {
            while (true)
            {
                Note newNote = new Note();
                using (StudyDiaryContext db = new StudyDiaryContext())
                {
                    try
                    {
                        newNote.TaskId = (from task in db.Tasks orderby task.TaskId select task.TaskId).Last();
                        Console.Write("Enter notes for this task (blank note to move on): ");
                        string str = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(str)) break;
                        else
                        {
                            newNote.Note1 = str;
                            db.Notes.Add(newNote);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Something went wrong, try again");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}