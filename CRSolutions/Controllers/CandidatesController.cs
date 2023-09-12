using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRSolutions.Data;
using CRSolutions.Models;
using CRSolutions.Extensions;

namespace CRSolutions.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly CRSolutionsDBContext _context;

        public CandidatesController(CRSolutionsDBContext context)
        {
            _context = context;
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            User user = HttpContext.Session.GetObject<User>("User");
            if (user != null)
            {
                if (user.IdRol == Guid.Parse("882E1047-29E1-4276-8BCE-6F2372670AE1")) //Cliente
                {

                    var cRSolutionsDBContext = _context.Candidates.Include(c => c.Company).Include(c => c.User);
                    return View(await cRSolutionsDBContext.ToListAsync());
                }
                if (user.IdRol == Guid.Parse("789A3411-ED70-42BD-9681-B0D9AE800583")) //Cliente Admin
                {

                    var cRSolutionsDBContext = _context.Candidates.Include(c => c.Company).Include(c => c.User);
                    return View(await cRSolutionsDBContext.ToListAsync());
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET: Candidates/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null || _context.Candidates == null)
        //    {
        //        return NotFound();
        //    }

        //    var candidate = await _context.Candidates
        //        .Include(c => c.Company)
        //        .Include(c => c.User)
        //        .FirstOrDefaultAsync(m => m.IdCantidate == id);
        //    if (candidate == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(candidate);
        //}

        // GET: Candidates/Create
        public IActionResult Create()
        {
            ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName");
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "FullName");
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCantidate,FullName,EvaluatedPosition,IdRiskScore,EvaluationDate,ReportFile,AudioFile,CreditFile,IdTypeTest,RecordEvaluation,BlackList,Status,IdUser,IdCompany,CURP")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {                
                candidate.IdCantidate = Guid.NewGuid();                
                _context.Add(candidate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName", candidate.IdCompany);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "FullName", candidate.IdUser);
            return View(candidate);
            //return View("Create");
        }

        // GET: Candidates/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null || _context.Candidates == null)
        //    {
        //        return NotFound();
        //    }

        //    var candidate = await _context.Candidates.FindAsync(id);
        //    if (candidate == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName", candidate.IdCompany);
        //    ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "FullName", candidate.IdUser);
        //    return View(candidate);
        //}

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("IdCantidate,FullName,EvaluatedPosition,IdRiskScore,EvaluationDate,ReportFile,AudioFile,PhotoFile,IdTypeTest,RecordEvaluation,BlackList,Status,IdUser,IdCompany")] Candidate candidate)
        //{
        //    if (id != candidate.IdCantidate)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(candidate);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CandidateExists(candidate.IdCantidate))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName", candidate.IdCompany);
        //    ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "FullName", candidate.IdUser);
        //    return View(candidate);
        //}

        // GET: Candidates/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null || _context.Candidates == null)
        //    {
        //        return NotFound();
        //    }

        //    var candidate = await _context.Candidates
        //        .Include(c => c.Company)
        //        .Include(c => c.User)
        //        .FirstOrDefaultAsync(m => m.IdCantidate == id);
        //    if (candidate == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(candidate);
        //}

        // POST: Candidates/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    if (_context.Candidates == null)
        //    {
        //        return Problem("Entity set 'CRSolutionsDBContext.Candidates'  is null.");
        //    }
        //    var candidate = await _context.Candidates.FindAsync(id);
        //    if (candidate != null)
        //    {
        //        _context.Candidates.Remove(candidate);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool CandidateExists(Guid id)
        {
          return (_context.Candidates?.Any(e => e.IdCantidate == id)).GetValueOrDefault();
        }
    }
}
