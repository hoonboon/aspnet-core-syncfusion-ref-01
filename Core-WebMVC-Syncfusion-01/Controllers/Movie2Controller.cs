using Syncfusion.EJ2.Base;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core_WebMVC_Syncfusion_01.Models;
using Core_WebMVC_Syncfusion_01.Data;

namespace Core_WebMVC_Syncfusion_01.Controllers
{
    public class Movie2Controller : Controller
    {
        private MyAppContext _context;

        public Movie2Controller(MyAppContext Context)
        {
            this._context = Context;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UrlDatasource([FromBody] DataManagerRequest dm)
        {

            IEnumerable DataSource = _context.Movie.ToList();

            DataOperations operation = new DataOperations();
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<Movie>().Count();
            if (dm.Skip != 0)//Paging
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return Json(new { result = DataSource, count = count });
        }
        public ActionResult Insert([FromBody] CRUDModel<Movie> value)
        {
            //do stuff
            _context.Movie.Add(value.Value);
            _context.SaveChanges();
            return Json(value);

        }
        public ActionResult Update([FromBody] CRUDModel<Movie> value)
        {
            //do stuff
            var ord = value;

            Movie val = _context.Movie.Where(or => or.Id == ord.Value.Id).FirstOrDefault();
            val.Id = ord.Value.Id;
            val.Title = ord.Value.Title;
            val.ReleaseDate = ord.Value.ReleaseDate;
            val.Genre = ord.Value.Genre;
            val.Price = ord.Value.Price;
            _context.SaveChanges();
            return Json(value);
        }
        public ActionResult Delete([FromBody] CRUDModel<Movie> value)
        {
            //do stuff
            Movie order = _context.Movie.Where(c => c.Id == (int)value.Key).FirstOrDefault();
            _context.Movie.Remove(order);
            _context.SaveChanges();
            return Json(order);
        }
    }
}