using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SingleRDemo.Models;
using SingleRDemo.Hubs;

namespace SingleRDemo.Controllers
{
    public class ProductsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Get() {
            
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("Select * from Products",connection);
            command.Notification = null;
            SqlDependency dependency = new SqlDependency(command);
            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

            //if (connection.State == ConnectionState.Closed)
            //{
               // connection.Open();
                SqlDataReader reader = command.ExecuteReader();
            var listPd = reader.Cast<IDataRecord>()
                  .Select(x => new { 
                      Id=Convert.ToInt32(x["Id"]),
                      Name=(string)x["Name"],
                      Price =Convert.ToInt32(x["Price"]),
                      ImagePath =(string)x["ImagePath"],
                      Category_ID = (string)x["Category_Id"],
                      Brand_Id = Convert.ToInt32(x["Brand_Id"])
                  
                  
                  }).ToList();
            //var da = db.Products.ToList();
            return Json(new { listPd = listPd },JsonRequestBehavior.AllowGet);
            //}
        }


        public void dependency_OnChange(object sender,SqlNotificationEventArgs e) {
            System.Diagnostics.Debug.WriteLine("inside onchange method");
            ProductHub h = new ProductHub();
            h.Show();
          
        }

    }
}