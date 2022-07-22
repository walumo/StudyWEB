using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace StudyWEB.Models
{
    public partial class Note
    {
        public int Id { get; set; }
        
        [Required]
        public string Note1 { get; set; }
        public int TaskId { get; set; }

        public virtual Task Task { get; set; }
    }
}
