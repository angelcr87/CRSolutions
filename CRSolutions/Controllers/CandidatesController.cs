﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRSolutions.Data;
using CRSolutions.Models;
using CRSolutions.Extensions;
using System;
using System.IO;

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
                    return View("Index",await cRSolutionsDBContext.ToListAsync());
                }
                if (user.IdRol == Guid.Parse("789A3411-ED70-42BD-9681-B0D9AE800583")) //Cliente Admin
                {

                    var cRSolutionsDBContext = _context.Candidates.Include(c => c.Company).Include(c => c.User);
                    return View("Index",await cRSolutionsDBContext.ToListAsync());
                }
                else
                {
                     return Unauthorized();
                    //return RedirectToAction("Login", "Users");
                }
            }
            else
            {
               // return Unauthorized();
                return  RedirectToAction("Login","Users");
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
            User user = HttpContext.Session.GetObject<User>("User");
            if (user != null)
            {
                if (user.IdRol == Guid.Parse("4A74DA66-BCD1-4662-8625-CB7C3BF2A837")) //Admin
                {
                    ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName");
                    ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "FullName");
                    return View();

                }
                else
                {
                    return Unauthorized();
                }

            }
            else
            {
                return RedirectToAction("Login", "Users");

            }


        }

        // POST: Candidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCantidate,FullName,EvaluatedPosition,IdRiskScore,EvaluationDate,ReportFile,AudioFile,CreditFile,IdTypeTest,RecordEvaluation,BlackList,Status,IdUser,IdCompany,CURP")] Candidate candidate, IFormFile ReportFile, IFormFile CreditFile, IFormFile AudioFile)
        {

            if (ModelState.IsValid)
            {                
                candidate.IdCantidate = Guid.NewGuid();


                //Procesamos Audio
                if (AudioFile != null && AudioFile.Length > 0)
                {
                    try
                    {
                        // Leer el archivo de audio en un arreglo de bytes
                        byte[] bytesDelAudio;
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            AudioFile.CopyTo(memoryStream);
                            bytesDelAudio = memoryStream.ToArray();
                            candidate.AudioFile = bytesDelAudio;
                        }

                        // Guardar los bytes del audio en la base de datos
                        // Aquí debes implementar la lógica para guardar el archivo en la base de datos.

                        // Luego, realiza cualquier otra lógica que necesites, como redirigir al usuario a una página de confirmación.
                        //return RedirectToAction("Confirmacion");
                    }
                    catch (Exception ex)
                    {
                        // Maneja cualquier excepción que pueda ocurrir al procesar el archivo
                        ViewBag.Error = "Ocurrió un error al procesar el archivo de audio: " + ex.Message;
                    }
                }
                else
                {
                    // En caso de que no se haya proporcionado un archivo válido, puedes manejar el error aquí.
                    ViewBag.Error = "Por favor, selecciona un archivo de audio válido.";
                }

                //Procesamos documento de credito
                if (CreditFile != null && CreditFile.Length > 0)
                {
                    try
                    {
                        // Leer el archivo de audio en un arreglo de bytes
                        byte[] bytesDelCredito;
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            CreditFile.CopyTo(memoryStream);
                            bytesDelCredito = memoryStream.ToArray();
                            candidate.CreditFile = bytesDelCredito;
                        }

                        // Guardar los bytes del audio en la base de datos
                        // Aquí debes implementar la lógica para guardar el archivo en la base de datos.

                        // Luego, realiza cualquier otra lógica que necesites, como redirigir al usuario a una página de confirmación.
                        //return RedirectToAction("Confirmacion");
                    }
                    catch (Exception ex)
                    {
                        // Maneja cualquier excepción que pueda ocurrir al procesar el archivo
                        ViewBag.Error = "Ocurrió un error al procesar el archivo de audio: " + ex.Message;
                    }
                }
                else
                {
                    // En caso de que no se haya proporcionado un archivo válido, puedes manejar el error aquí.
                    ViewBag.Error = "Por favor, selecciona un archivo de audio válido.";
                }

                //Procesamos documento Reporte
                if (ReportFile != null && ReportFile.Length > 0)
                {
                    try
                    {
                        // Leer el archivo de audio en un arreglo de bytes
                        byte[] bytesDelReporte;
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            ReportFile.CopyTo(memoryStream);
                            bytesDelReporte = memoryStream.ToArray();
                            candidate.ReportFile = bytesDelReporte;
                        }

                        // Guardar los bytes del audio en la base de datos
                        // Aquí debes implementar la lógica para guardar el archivo en la base de datos.

                        // Luego, realiza cualquier otra lógica que necesites, como redirigir al usuario a una página de confirmación.
                        //return RedirectToAction("Confirmacion");
                    }
                    catch (Exception ex)
                    {
                        // Maneja cualquier excepción que pueda ocurrir al procesar el archivo
                        ViewBag.Error = "Ocurrió un error al procesar el archivo de audio: " + ex.Message;
                    }
                }
                else
                {
                    // En caso de que no se haya proporcionado un archivo válido, puedes manejar el error aquí.
                    ViewBag.Error = "Por favor, selecciona un archivo de audio válido.";
                }

                candidate.Status = true;
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
