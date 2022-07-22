using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [StringLength(64)]
        public string TopicTitle { get; set; }
        
        [StringLength(256)]
        public string TopicDescription { get; set; }
        
        public double TopicEstimatedTimeToMaster { get; set; }
        
        public double? TopicTimeSpent { get; set; }
        
        [StringLength(256)]
        public string TopicSource { get; set; }
        
        public DateTime? TopicStartLearningDate { get; set; }
        
        public bool TopicInProgress { get; set; }
        
        public bool TopicIsDone { get; set; }
        
        public DateTime TopicCompletionDate { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
