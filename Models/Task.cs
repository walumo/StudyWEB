using System;
using System.Collections.Generic;

#nullable disable

namespace StudyWEB.Models
{
    public partial class Task
    {
        public Task()
        {
            Notes = new HashSet<Note>();
        }

        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public int? TaskPriority { get; set; }
        public DateTime? TaskDeadline { get; set; }
        public bool? TaskDone { get; set; }
        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
