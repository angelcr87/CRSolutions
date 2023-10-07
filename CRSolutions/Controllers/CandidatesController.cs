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
using System;
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.InteropServices;
using System.Collections;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public  IActionResult Index()
        {
            User user = HttpContext.Session.GetObject<User>("User");
            if (user != null)
            {
                if (user.IdRol == Guid.Parse("882E1047-29E1-4276-8BCE-6F2372670AE1")) //Cliente
                {

                    var cRSolutionsDBContext = _context.Candidates.Where(c => c.IdUser==user.IdUser).OrderByDescending(c => c.EvaluationDate).Include(c => c.Company).Include(c => c.User);
                    foreach (var item in cRSolutionsDBContext)
                    {
                        item.antiquity = CompareDates(item.EvaluationDate, DateTime.Now);
                    }

                    //return View("Index",await cRSolutionsDBContext.ToListAsync());
                    return View("Index", cRSolutionsDBContext);
                }
                if (user.IdRol == Guid.Parse("789A3411-ED70-42BD-9681-B0D9AE800583")) //Cliente Admin
                {

                    var cRSolutionsDBContext = _context.Candidates.Include(c => c.Company).Where(c => c.IdCompany == user.IdCompany).OrderByDescending(c => c.EvaluationDate).Include(c => c.User);
                    foreach (var item in cRSolutionsDBContext)
                    {
                        item.antiquity = CompareDates(item.EvaluationDate, DateTime.Now);
                    }
                    //return View("Index",await cRSolutionsDBContext.ToListAsync());
                    return View("Index", cRSolutionsDBContext.ToList());
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

        public IActionResult GetFile(Guid id, string typeFile, string file)
        {
            //typeFile: "audio/mpeg" o  "application/pdf"
            byte[]? FileB = null;
            if (file == "ReportFile")
            {
                byte[] FileBytes = _context.Candidates.Find(id).ReportFile ;
                FileB = FileBytes;
            }
            if (file == "AudioFile")
            {
                byte[] FileBytes = _context.Candidates.Find(id).AudioFile;
                FileB = FileBytes;
            }
            if (file == "CreditFile")
            {
                byte[] FileBytes = _context.Candidates.Find(id).CreditFile;
                FileB = FileBytes;
            }      
            
            //byte[] FileBytes = Archivo;
            var fileStream = new System.IO.MemoryStream();
            fileStream.Write(FileB, 0, FileB.Length);
            fileStream.Position = 0;
            return new FileStreamResult(fileStream, typeFile);
        }


        public IFormFile GetFormFile(Guid id, string typeFile, string file)
        {
            //typeFile: "audio/mpeg" o  "application/pdf" o "image/jpeg"
            byte[]? FileB = null;
            if (file == "ReportFile")
            {
                byte[] FileBytes = _context.Candidates.Find(id).ReportFile;
                FileB = FileBytes;
            }
            if (file == "AudioFile")
            {
                byte[] FileBytes = _context.Candidates.Find(id).AudioFile;
                FileB = FileBytes;
            }
            if (file == "CreditFile")
            {
                byte[] FileBytes = _context.Candidates.Find(id).CreditFile;
                FileB = FileBytes;
            }
            if (file == "Photp")
            {
                byte[] FileBytes = _context.Candidates.Find(id).CreditFile;
                FileB = FileBytes;
            }

            //byte[] FileBytes = Archivo;
            var fileStream = new System.IO.MemoryStream();
            fileStream.Write(FileB, 0, FileB.Length);
            fileStream.Position = 0;
            return new FormFile(fileStream, 0, FileB.Length, file, file);
        }



        public string CompareDates (DateTime fechaInicio, DateTime fechaFin)
        {
            TimeSpan diferencia = fechaFin - fechaInicio;
            string comparacionFechas = "";

            if (diferencia.TotalDays < 1)
            {
                comparacionFechas = "0 días";
                return comparacionFechas;
            }
            else if (diferencia.TotalDays < 30)
            {
                comparacionFechas = $"{Math.Truncate(diferencia.TotalDays)} días.";
                return comparacionFechas;
            }
            else if (diferencia.TotalDays < 365)
            {
                int meses = (int)(Math.Truncate(diferencia.TotalDays / 30));
                comparacionFechas = $"{meses} meses.";
                return comparacionFechas;
            }
            else
            {
                int años = (int)(Math.Truncate(diferencia.TotalDays / 365));
                comparacionFechas = $"{años} años.";
                return comparacionFechas;
            }

            //return View();
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

        //GET: Candidates/Edit/ehhfiefhei
        public IActionResult EditCandidate(string? CURP,Guid idCompany)
        {
            if (CURP == null || _context.Candidates == null)
            {
                
                ViewData["NotFound"] = "CURP no encontrado en la Base de datos";
                ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName");
                ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "FullName");
                ViewData["disabled"] = false;
                return View("Create");
            }

            //var candidate =  (from c in _context.Candidates
            //where c.CURP == curp
            //select c) ;

            var candidate = _context.Candidates.Where(c => c.CURP == CURP && c.IdCompany == idCompany).FirstOrDefault();

            if (candidate == null)
            {
                ViewData["NotFound"] = "CURP no encontrado en la Base de datos";
                ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName");
                ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "FullName");

                ViewData["disabled"] = false;
                return View("Create", candidate);
                //return NotFound();
            }
            else
            {
                ViewData["NotFound"] = "";
                ViewData["disabled"] = true;

            }
            ViewData["DisabledButton"] = "disabled";
            ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName", candidate.IdCompany);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "FullName", candidate.IdUser);
            return View("Create",candidate);
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(10L * 1024L * 1024L * 1024L)]
        [RequestFormLimits(MultipartBodyLengthLimit = 10L * 1024L * 1024L * 1024L)]
        public IActionResult UpdateCandidate(Guid IdCantidate, [Bind("IdCantidate,FullName,EvaluatedPosition,IdRiskScore,EvaluationDate,ReportFile,AudioFile,CreditFile,IdTypeTest,RecordEvaluation,BlackList,Status,IdUser,IdCompany,CURP, Photo")] Candidate candidate, IFormFile? ReportFile, IFormFile? CreditFile, IFormFile? AudioFile, IFormFile? Photo)
        {
            if (IdCantidate != candidate.IdCantidate)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Candidate candidateOriginal =  _context.Candidates.Find(candidate.IdCantidate);
                
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
                    //ViewBag.Error = "Por favor, selecciona un archivo de audio válido.";
                    candidate.AudioFile = candidateOriginal.AudioFile;

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
                    //ViewBag.Error = "Por favor, selecciona un archivo de audio válido.";
                    candidate.CreditFile = candidateOriginal.CreditFile;
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
                    // ViewBag.Error = "Por favor, selecciona un archivo de audio válido.";
                    candidate.ReportFile = candidateOriginal.ReportFile;
                }

                //Procesamos Foto
                if (Photo != null && Photo.Length > 0)
                {
                    try
                    {
                        // Leer el archivo de audio en un arreglo de bytes
                        byte[] bytesFoto;
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            Photo.CopyTo(memoryStream);
                            bytesFoto = memoryStream.ToArray();
                            candidate.Photo = bytesFoto;
                        }

                        // Guardar los bytes del audio en la base de datos
                        // Aquí debes implementar la lógica para guardar el archivo en la base de datos.

                        // Luego, realiza cualquier otra lógica que necesites, como redirigir al usuario a una página de confirmación.
                        //return RedirectToAction("Confirmacion");
                    }
                    catch (Exception ex)
                    {
                        // Maneja cualquier excepción que pueda ocurrir al procesar el archivo
                        ViewBag.Error = "Ocurrió un error al procesar la foto " + ex.Message;

                    }
                }
                else
                {
                    // En caso de que no se haya proporcionado un archivo válido, puedes manejar el error aquí.
                    // ViewBag.Error = "Por favor, selecciona un archivo de audio válido.";
                    candidate.Photo = candidateOriginal.Photo;
                }

                try
                {
                   
                   // _context.Entry(candidate).State = EntityState.Modified;
                    //_context.Update(candidate);
                    _context.Entry(candidateOriginal).CurrentValues.SetValues(candidate);
                     _context.SaveChanges();
                    ViewBag.Confirmacion = "El Registro ha sido Guardado con Exito";
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                //return RedirectToAction(nameof(Index));
                //return View("Create", candidate);
            }
            ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName", candidate.IdCompany);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "FullName", candidate.IdUser);
            return View("Create", candidate);
        }



    


        // POST: Candidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(10L * 1024L * 1024L * 1024L)]
        [RequestFormLimits(MultipartBodyLengthLimit = 10L * 1024L * 1024L * 1024L)]
        public async Task<IActionResult> Create([Bind("FullName,EvaluatedPosition,IdRiskScore,EvaluationDate,ReportFile,AudioFile,CreditFile,IdTypeTest,RecordEvaluation,BlackList,Status,IdUser,IdCompany,CURP,Photo")] Candidate candidate, IFormFile ReportFile, IFormFile CreditFile, IFormFile AudioFile, IFormFile? Photo)
        {

            if (ModelState.IsValid)
            {                
                candidate.IdCantidate = Guid.NewGuid();

                bool validaCurp =  _context.Candidates.Where(c => c.CURP == candidate.CURP).Count() > 0;
                if (!validaCurp)
                {
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


                    //Procesamos Foto
                    if (Photo != null && Photo.Length > 0)
                    {
                        try
                        {
                            // Leer el archivo de foto en un arreglo de bytes
                            byte[] bytesFoto;
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                Photo.CopyTo(memoryStream);
                                bytesFoto = memoryStream.ToArray();
                                candidate.Photo = bytesFoto;
                            }

                            // Guardar los bytes del audio en la base de datos
                            // Aquí debes implementar la lógica para guardar el archivo en la base de datos.

                            // Luego, realiza cualquier otra lógica que necesites, como redirigir al usuario a una página de confirmación.
                            //return RedirectToAction("Confirmacion");
                        }
                        catch (Exception ex)
                        {
                            // Maneja cualquier excepción que pueda ocurrir al procesar el archivo
                            ViewBag.Error = "Ocurrió un error al procesar la foto de perfil: " + ex.Message;
                        }
                    }
                    else
                    {
                        // En caso de que no se haya proporcionado un archivo válido, puedes manejar el error aquí.
                        ViewBag.Error = "Por favor, selecciona una foto de perfil";
                    }


                    candidate.Status = true;
                    _context.Add(candidate);
                    await _context.SaveChangesAsync();
                    ViewBag.Confirmacion = "El Registro ha sido Guardado con Exito";
                    //return RedirectToAction(nameof(Create));

                }
                else
                {
                    candidate.IdCantidate = Guid.Parse("00000000-0000-0000-0000-000000000000");
                    ViewBag.NotFound = "CURP Existente";
                    ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName", candidate.IdCompany);
                    ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "FullName", candidate.IdUser);
                    return View(candidate);
                }
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
