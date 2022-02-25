using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }

        public DateTime AnswerDateAndTime { get; set; }
        
        [Required]
        public string AnswerText { get; set; }

        public string UserId { get; set; }
        
        public virtual IdentityUser User { get; set; }


    }
}
