using System;
using System.Collections.Generic;

#nullable disable

namespace StudyWEB.Models
{
    public partial class Topic
    {
        public Topic()
        {
            Tasks = new HashSet<Task>();
        }

        public int TopicId { get; set; }
        public string TopicTitle { get; set; }
        public string TopicDescription { get; set; }
        public double TopicEstimatedTimeToMaster { get; set; }
        public double? TopicTimeSpent { get; set; }
        public string TopicSource { get; set; }
        public DateTime? TopicStartLearningDate { get; set; }
        public bool TopicInProgress { get; set; }
        public DateTime TopicCompletionDate { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
