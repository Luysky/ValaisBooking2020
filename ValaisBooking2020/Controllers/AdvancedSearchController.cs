using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValaisBooking2020.Models;

namespace ValaisBooking2020.Controllers
{
    public class AdvancedSearchController : Controller
    {

        private IRoomManager RoomManager { get; }

        public AdvancedSearchController(IRoomManager roomManager)
        {
            RoomManager = roomManager;
        }


        // GET: AdvancedSearchController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Index(AdvancedSearchController asvm)
        {
            return RedirectToAction("Index", "RoomAdvanced", asvm);
        }
        

        // GET: AdvancedSearchController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdvancedSearchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvancedSearchController/Create
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

        // GET: AdvancedSearchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdvancedSearchController/Edit/5
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

        // GET: AdvancedSearchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdvancedSearchController/Delete/5
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
