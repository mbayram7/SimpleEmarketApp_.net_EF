using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EmarketDreamsBytes;
using EmarketDreamsBytes.Entity;
using Newtonsoft.Json;
using System.IO;

namespace EmarketDreamsBytes.Controllers
{
    public class ProductsController : Controller
    {
        private MarketDB db = new MarketDB();

        public dynamic IsLogin { get; private set; }

        // GET: Products
        public ActionResult AdminProductsIndex()
        {
            return View(db.Products.ToList());
        }

        // GET: Products
        public ActionResult Index()
        {
            ViewBag.IsLogin = this.IsLogin;
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,ProductCategory,ProductStock,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("/AdminProductsIndex");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,ProductCategory,ProductStock,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/AdminProductsIndex");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("/AdminProductsIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public ActionResult Json()
        //{

        //    var conn = new SqlConnection(@"Data Source =.\SQLEXPRESS; Initial Catalog = EmarketDreamsBytesDB; Integrated Security = True");
        //    SqlCommand com = new SqlCommand("select ProductName AS [product name ], ProductCategory AS [category], ProductStock AS[product Stock], Price AS[price] from Products for JSON PATH, ROOT('Products')");
        //    conn.Open();
        //    com.Parameters.AddWithValue("@MessageId", 1);

        //    string s = com.ExecuteScalar().ToString();
        //    return View(s);
        //}

        public void GetJson(object sender, EventArgs e)
        {
            string strConn = "Data Source =.\\SQLEXPRESS; Initial Catalog = EmarketDreamsBytesDB; Integrated Security = True";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand command = new SqlCommand("select ProductName AS [product name ], ProductCategory AS [category], ProductStock AS[product Stock], Price AS[price] from Products for JSON PATH, ROOT('Products')"))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        command.Connection = conn;
                        adapter.SelectCommand = command;
                        using (DataTable dtEmployee = new DataTable())
                        {
                            adapter.Fill(dtEmployee);
                            string str = string.Empty;
                            foreach (DataColumn column in dtEmployee.Columns)
                            {
                                // Add the header to the text file
                                str += column.ColumnName + "\t\t";
                            }
                            // Insert a new line.
                            str += "\r\n";
                            foreach (DataRow row in dtEmployee.Rows)
                            {
                                foreach (DataColumn column in dtEmployee.Columns)
                                {
                                    // Insert the Data rows.
                                    str += row[column.ColumnName].ToString() + "\t\t";
                                }
                                // Insert a  new line.
                                str += "\r\n";
                            }
                            // Download the Text file.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment;filename=ExportFromSQL.txt");
                            Response.Charset = "";
                            Response.ContentType = "application/text";
                            Response.Output.Write(str);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
        }



    }
}
