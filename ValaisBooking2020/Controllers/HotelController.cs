using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BLL;

namespace ValaisBooking2020.Controllers
{
    public class HotelController : Controller
    {
        private IHotelManager HotelManager { get; }

        public HotelController (IHotelManager hotelManager)
        {
            HotelManager = hotelManager;
        } 

        //Get HotelController
        public IActionResult Index()
        {
            var hotels = HotelManager.GetHotels();
            return View(hotels);
        }

        //Get HotelContoller/Details
        public ActionResult Details(int id)
        {
            return View();
        }

        //Get HotelContoller/Create
        public ActionResult Create(int id)
        {
            return View();
        }

        //Get HotelContoller/Delete
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
