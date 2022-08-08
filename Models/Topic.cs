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

        [Required(ErrorMessage = "Topic title can not be empty!")]
        [StringLength(64)]
        public string TopicTitle { get; set; }
        
        [StringLength(256)]
        public string TopicDescription { get; set; }
        
        [Required(ErrorMessage = "Estimated time can not be empty!")]
        public double TopicEstimatedTimeToMaster { get; set; }
        
        public double? TopicTimeSpent { get; set; }
        
        [StringLength(256)]
        public string TopicSource { get; set; }

        public DateTime? TopicStartLearningDate { get; set; }
        
        public bool TopicInProgress { get; set; }
        
        public bool TopicIsDone { get; set; }

        [Required(ErrorMessage = "Enter deadline for this topic!")]
        public DateTime TopicCompletionDate { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
