using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebStudyDiary.Models;

namespace StudyDiary
{
    static class Search
    {
        public static void Topic()
        {
            while (true)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("SEARCH:\n");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("Search for topic by id or name (leave blank return): ");

                string input = Console.ReadLine();
                Console.Clear();

                using (StudyDiaryContext db = new StudyDiaryContext())
                {
                    if (String.IsNullOrWhiteSpace(input)) break;
                    else if (input.StartsWith('#')) Load.SmartSearch(input);
                    else if (!String.IsNullOrWhiteSpace(input) && int.TryParse(input, out int result)) Load.GetTopics(result);
                    else if (!String.IsNullOrWhiteSpace(input)) Load.GetTopics(input);
                } 
            }
        }
    }
}