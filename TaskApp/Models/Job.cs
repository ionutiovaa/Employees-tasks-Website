using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskApp.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public float NumberOfHours { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Project { get; set; }
    }
}