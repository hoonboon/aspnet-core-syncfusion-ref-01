using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core_WebMVC_Syncfusion_01.Models;
using Core_WebMVC_Syncfusion_01.Data;

namespace Core_WebMVC_Syncfusion_01.Controllers
{

   public class Movie1Controller : Controller
   {
        private MyAppContext _context;

		public Movie1Controller(MyAppContext Context)
		{
            this._context=Context;
		}

        public ActionResult Index()
        {

            ViewBag.dataSource = _context.Movie.ToList();

            return View();
        }

    }
}