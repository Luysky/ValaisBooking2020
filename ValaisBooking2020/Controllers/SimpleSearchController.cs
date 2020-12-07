using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using BLL;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using ValaisBooking2020.Models;
using System.Text;

namespace ValaisBooking2020.Controllers
{
    

    public class SimpleSearchController : Controller
    {

        private IRoomManager RoomManager { get; }

        public SimpleSearchController(IRoomManager roomManager)
        {
            RoomManager = roomManager;
        }

        public HotelDB hotelDB;
        // GET: SimpleSearchController
        [HttpGet]
        public ActionResult Index()
        {      

            return View();
        }

    

        [HttpPost]
        public ActionResult Index(SimpleSearchViewModel ssvm)
        {
            return RedirectToAction("Index", "Room", ssvm);
        }


            public ActionResult City(string id, string city)
        {
            HttpContext.Session.SetString(id, city);
            return City(id, city);
        }

        // GET: SimpleSearchController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SimpleSearchController/Create
        public ActionResult Choose()
        {
            return RedirectToAction("Index", "Home");
        }



        // POST: SimpleSearchController/Create
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

        // GET: SimpleSearchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SimpleSearchController/Edit/5
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

        // GET: SimpleSearchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

       

        // POST: SimpleSearchController/Delete/5
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
