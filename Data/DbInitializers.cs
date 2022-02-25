using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Data
{
    public class DbInitializers
    {
        public static void Initialize(stackContext context)
        {
            context.Database.EnsureCreated();

            if (context.Question.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new Category[]
            {
            new Category{CategoryName="HTML"},
            new Category{CategoryName="CSS"},
            new Category{CategoryName="Javascript"}

            };
            foreach (Category c in categories)
            {
                if (context.Category.Where(c => c.CategoryName == c.CategoryName).FirstOrDefault()==null)
                {
                    context.Category.Add(c);
                }
            }
            context.SaveChanges();

            //var questions = new Question[]
            //{
            //new Question{QuestionID=1,QuestionName="Sedan", CategoryID =1},
            //new Question{QuestionID=2,QuestionName="SUV", CategoryID =2},
            //new Question{QuestionID=3,QuestionName="Sedan", CategoryID =3},
            //};
            //foreach (Question c in questions)
            //{
            //    context.Question.Add(c);
            //}
            //context.SaveChanges();
        }
    }
}













