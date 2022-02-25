using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    public class AnswerViewModel
    {
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }

        public DateTime AnswerDateAndTime { get; set; }

        [Required]
        public string AnswerText { get; set; }

        public string UserEmail { get; set; }
    }
}
