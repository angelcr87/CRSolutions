﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRSolutions.Views.Candidates
{
    public class ejemploController : Controller
    {
        // GET: ejemploController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ejemploController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ejemploController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ejemploController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ejemploController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ejemploController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ejemploController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ejemploController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}