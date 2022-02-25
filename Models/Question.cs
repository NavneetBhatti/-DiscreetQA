using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models
{

   

    public class Question
    {
        public int QuestionID { get; set; }

        [Display(Name ="Question")]
        public string QuestionName { get; set; }

        [Display(Name = "Added Date")]
        public string QuestionDateAndTime { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public ICollection<Answer> Answer { get; set; }


    }
}
