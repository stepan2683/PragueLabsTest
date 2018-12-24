using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PragueLabsTest.Class;
using PragueLabsTest.Models;

namespace PragueLabsTest.Controllers
{
    public class InvoiceItemsController : Controller
    {
        private Model db = new Model();

        // GET: InvoiceItems
        public ActionResult Index()
        {
            return View(db.DbInvoiceItem.ToList());
        }

        // GET: InvoiceItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.DbInvoiceItem.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Create
        public ActionResult Create()
        {
            var invoices = db.DbInvoice.ToList().ConvertAll(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.Name });
            ViewBag.InvoiceList = invoices;

            return View();
        }

        // POST: InvoiceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Count,PriceForOneItem")] InvoiceItem invoiceItem)
        {
            //var invoices = db.DbInvoice.ToList().ConvertAll(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.Name });
            //ViewBag.InvoiceList = invoices;

            if (ModelState.IsValid)
            {
                var invoiceId = Convert.ToInt32(Request.Form["Invoice.Id"]);
                invoiceItem.Invoice = db.DbInvoice.FirstOrDefault(a => a.Id == invoiceId);
                db.DbInvoiceItem.Add(invoiceItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoiceItem);
        }

        // GET: InvoiceItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.DbInvoiceItem.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Count,PriceForOneItem")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.DbInvoiceItem.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceItem invoiceItem = db.DbInvoiceItem.Find(id);
            db.DbInvoiceItem.Remove(invoiceItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
