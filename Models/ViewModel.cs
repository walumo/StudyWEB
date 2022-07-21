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
    }
}
