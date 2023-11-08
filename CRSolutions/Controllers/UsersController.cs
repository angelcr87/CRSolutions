using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRSolutions.Data;
using CRSolutions.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Plugins;
using System.Security.Cryptography;
using System.Text;
using CRSolutions.Extensions;

namespace CRSolutions.Controllers
{
    public class UsersController : Controller
    {
        private readonly CRSolutionsDBContext _context;

        public UsersController(CRSolutionsDBContext context)
        {
            _context = context;
        }

        // GET: Users
        //public async Task<IActionResult> Index()
        //{
        //    var cRSolutionsDBContext = _context.Users.Include(u => u.Company).Include(u => u.Role);
        //    return View(await cRSolutionsDBContext.ToListAsync());
        //}

        // GET: Users/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .Include(u => u.Company)
        //        .Include(u => u.Role)
        //        .FirstOrDefaultAsync(m => m.IdUser == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        // GET: Users/Create
        public IActionResult Create()
        {
            User user = HttpContext.Session.GetObject<User>("User");
            if (user != null)
            {
                if (user.IdRol == Guid.Parse("4A74DA66-BCD1-4662-8625-CB7C3BF2A837")) //Admin
                {
                    ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName");
                    ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "RoleName");
                    return View();
                }
                else
                {
                    return Unauthorized();
                    //return RedirectToAction("Login", "Users");
                }
            }
            else
            {
                return Unauthorized();
            }


        }

        //POST: Users/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUser,FullName,UserName,Password,IdRol,IdCompany")] User user)
        {
            //if (ModelState.IsValid)
            //{
                user.Status = true;
                user.Password = Encrypt(user.Password);
                user.IdUser = Guid.NewGuid();
                _context.Add(user);
                await _context.SaveChangesAsync();
                return View("Login");
            //}
            ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName", user.IdCompany);
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "RoleName", user.IdRol);
            return View("Login");
        }

        // GET: Users/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName", user.IdCompany);
        //    ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "RoleName", user.IdRol);
        //    return View(user);
        //}

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("IdUser,FullName,UserName,Password,Status,IdRol,IdCompany")] User user)
        //{
        //    if (id != user.IdUser)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.IdUser))
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
        //    ViewData["IdCompany"] = new SelectList(_context.Companies, "IdCompany", "CompanyName", user.IdCompany);
        //    ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "RoleName", user.IdRol);
        //    return View(user);
        //}

        // GET: Users/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .Include(u => u.Company)
        //        .Include(u => u.Role)
        //        .FirstOrDefaultAsync(m => m.IdUser == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        // POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    if (_context.Users == null)
        //    {
        //        return Problem("Entity set 'CRSolutionsDBContext.Users'  is null.");
        //    }
        //    var user = await _context.Users.FindAsync(id);
        //    if (user != null)
        //    {
        //        _context.Users.Remove(user);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        // GET: Users/Login/
        public IActionResult Login()
        {
            //var cRSolutionsDBContext = _context.Users.Include(u => u.Company).Include(u => u.Role);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login login /*string UserName, string Password*/)
        {
            if(ModelState.IsValid)
            {
                string passEncrypt = Encrypt(login.Password);
                User loginUser =  _context.Users.Where(u => u.UserName == login.UserName && u.Password == passEncrypt).FirstOrDefault();
                
                
                //ViewBag["RolName"] = rol.RoleName;

                if (loginUser != null)
                {
                    Role rol = _context.Roles.Find(loginUser.IdRol);

                    HttpContext.Session.SetObject("User", loginUser);

                    if(rol.IdRol == Guid.Parse("4A74DA66-BCD1-4662-8625-CB7C3BF2A837")) //admin
                    {
                        return RedirectToAction("Create", "Candidates");
                    }
                    if (rol.IdRol == Guid.Parse("789A3411-ED70-42BD-9681-B0D9AE800583")) //admin cliente
                    {
                        return RedirectToAction("Index", "Candidates");
                    }
                    if(rol.IdRol == Guid.Parse("882E1047-29E1-4276-8BCE-6F2372670AE1")) //cliente
                    {
                        return RedirectToAction("Index", "Candidates");
                    }

                    
                }
                else
                {
                    ViewData["errorLogin"] = "Los datos ingresados son incorrectos";
                    return View("Login");
                }
            }
            //var cRSolutionsDBContext = _context.Users.Include(u => u.Company).Include(u => u.Role);
            return View();
        }

        public string Encrypt(string password)
        {
            using(SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
           // HttpContext.Session.Clear();
            return View("Login");
        }

        private bool UserExists(Guid id)
        {
          return (_context.Users?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
