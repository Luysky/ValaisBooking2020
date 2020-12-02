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

        public HotelDB hotelDB;
        // GET: SimpleSearchController
        [HttpGet]
        public ActionResult Index()
        {
          
            List<SelectListItem> Cities = new List<SelectListItem>
            {
                new SelectListItem{Value = "1", Text = "Martigny"},
                new SelectListItem{Value = "2", Text = "Sierre"},
                new SelectListItem{Value = "3", Text = "Sion"},
                new SelectListItem{Value = "4", Text = "Brig"},
            };
           
            foreach(var City in Cities)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Value = City.Value.ToString()
                };
                Cities.Add(selectList);
            }
            SimpleSearchViewModel simpleSearchViewModel = new SimpleSearchViewModel()
            {
                CitiesResult = Cities
            };
            return View();
 
        }

        [HttpPost]
        public string Index(IEnumerable<string> SelectedCities)
        {
            if(SelectedCities == null)
            {
                return "No cities selected";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("You selected - " + string.Join(",", SelectedCities));
                return sb.ToString();
            }
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
            return Choose();
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
