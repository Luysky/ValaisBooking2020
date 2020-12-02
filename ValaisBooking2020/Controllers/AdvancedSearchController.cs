﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ValaisBooking2020.Controllers
{
    public class AdvancedSearchController : Controller
    {
        // GET: AdvancedController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdvancedController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdvancedController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvancedController/Create
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

        // GET: AdvancedController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdvancedController/Edit/5
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

        // GET: AdvancedController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdvancedController/Delete/5
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