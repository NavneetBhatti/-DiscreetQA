using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Assignment2.Controllers
{
    [Authorize]
    public class AnswersController : Controller
    {
        private readonly stackContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AnswersController(stackContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Answer.ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer
                .FirstOrDefaultAsync(m => m.AnswerID == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answers/Create
        public async Task<IActionResult> Create(int? QuestionID)
        {
            if (QuestionID == null)
            {
                return NotFound();
            }

            var question = await _context.Question.FindAsync(QuestionID); 
            if (question == null)
            {
                return NotFound();
            }

           //var query = _context.Answer.AsQueryable();
           //query = query.Where(d => d.QuestionID == QuestionID);

           // var previousAnswers = query.ToList();

            var answers = await _context.Answer.Where(d => d.QuestionID == QuestionID).ToListAsync();
            List<AnswerViewModel> previousAnswers = new List<AnswerViewModel>();
            if (answers != null)
            {
                foreach (var ans in answers)
                {
                    AnswerViewModel answerViewModel = new AnswerViewModel();
                    answerViewModel.AnswerID = ans.AnswerID;
                    answerViewModel.QuestionID = ans.QuestionID;
                    answerViewModel.AnswerDateAndTime = ans.AnswerDateAndTime;
                    answerViewModel.AnswerText = ans.AnswerText;
                    answerViewModel.UserEmail = await _userManager.Users.Where(t => t.Id == ans.UserId).Select(t => t.Email).FirstOrDefaultAsync();
                    previousAnswers.Add(answerViewModel);
                }
            }

            ViewBag.QuestionName = question.QuestionName;
            ViewBag.QuestionID = QuestionID;
            ViewBag.previousAnswers = previousAnswers;
            ViewBag.QuestionDateAndTime = question.QuestionDateAndTime;
            ViewBag.AnswerCount = (answers != null ? answers.Count() : 0);
            //ViewData["QuestionName"] = question.QuestionName;
            return View();


            //return PartialView("ProductList", objCategory);
           // return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswerID,AnswerText,QuestionID")] Answer answer)
        {
           // string questionID = HttpContext.Request.Query["QuestionID"];
            DateTime localDate = DateTime.Now;

           // answer.QuestionID = Int32.Parse(questionID);
            answer.AnswerDateAndTime = localDate;
            answer.UserId = (await _userManager.GetUserAsync(User)).Id;
                

            // Request.QueryString["UserID"];
            if (ModelState.IsValid)
            {
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Answers", new { QuestionID = answer.QuestionID });
                
            }
            return View();
            
         
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswerID,QuestionID,AnswerDateAndTime,AnswerText")] Answer answer)
        {
            if (id != answer.AnswerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.AnswerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(answer);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer
                .FirstOrDefaultAsync(m => m.AnswerID == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _context.Answer.FindAsync(id);
            _context.Answer.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerExists(int id)
        {
            return _context.Answer.Any(e => e.AnswerID == id);
        }
    }
}
