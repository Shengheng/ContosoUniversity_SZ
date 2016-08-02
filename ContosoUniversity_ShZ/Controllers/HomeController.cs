using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity_ShZ.DAL;
using ContosoUniversity_ShZ.ViewModels;

namespace ContosoUniversity_ShZ.Controllers
{
   //[RoutePrefix("newHome")]
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            //IQueryable<EnrollmentDateGroup> data = from student in db.Students
            //                                       group student by student.EnrollmentDate into dateGroup
            //                                       select new EnrollmentDateGroup()
            //                                       {
            //                                           EnrollmentDate = dateGroup.Key,
            //                                           StudentCount = dateGroup.Count()
            //                                       };

            // SQL version of the above LINQ code. 
            string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount " 
                + "FROM Person "
                + "WHERE Discriminator = 'Student' "
                + "GROUP BY EnrollmentDate";
            IEnumerable<EnrollmentDateGroup> data = 
                db.Database.SqlQuery<EnrollmentDateGroup>(query);

            var dataCustomized = db.Students
                .GroupBy(stu => stu.EnrollmentDate)
                .Select(enrolldate => new EnrollmentDateGroup()
                {
                    EnrollmentDate = enrolldate.Key,
                    StudentCount = enrolldate.Count()
                });

            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}