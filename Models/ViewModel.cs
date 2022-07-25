using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyWEB.Models
{
    public class ViewModel
    {
        public List<Topic> Topics { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Note> Notes { get; set; }
        public Topic Topic { get; set; }
        public Task Task { get; set; }
        public Note Note { get; set; }
    }
}
