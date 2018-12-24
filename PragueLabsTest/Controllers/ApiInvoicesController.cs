using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PragueLabsTest.Class;
using PragueLabsTest.Models;
using System.IO;
using System.Text;

namespace PragueLabsTest.Controllers
{
    [RoutePrefix("api/Invoices")]
    public class ApiInvoicesController : ApiController
    {
        private Model db = new Model();
        private const string SECRET_KEY = "skey";

        [HttpGet]
        [Route("GetUnPaidInvoices")]
        public IEnumerable<Invoice> GetUnPaidInvoices()
        {
            IEnumerable<string> headerValues = null;
            if (Request.Headers.TryGetValues(SECRET_KEY, out headerValues))
            {
                var value = headerValues.FirstOrDefault();

                if (String.Compare(value, SECRET_KEY) == 0)
                {
                    return db.DbInvoice.Where(a => a.Status == Enums.PaidStatus.UnPaid).ToList();
                }
            }

            return null;
        }

        [HttpGet]
        [Route("SetPaidStatus/{id}")]
        public IHttpActionResult SetPaidStatus(int id)
        {
            IEnumerable<string> headerValues = null;
            if (Request.Headers.TryGetValues(SECRET_KEY, out headerValues))
            {
                var value = headerValues.FirstOrDefault();

                if (String.Compare(value, SECRET_KEY) == 0)
                {
                    var invoice = db.DbInvoice.FirstOrDefault(a => a.Id == id);
                    if (invoice != null)
                    {
                        invoice.Status = Enums.PaidStatus.Paid;
                        db.SaveChanges();
                    }
                }
            }
            return Ok();
        }

        [HttpPatch]
        [Route("UpdateInvoice")]
        public IHttpActionResult UpdateInvoice([FromBody] InvoiceRequest request)
        {
            IEnumerable<string> headerValues = null;
            if (Request.Headers.TryGetValues(SECRET_KEY, out headerValues))
            {
                var value = headerValues.FirstOrDefault();

                if (String.Compare(value, SECRET_KEY) == 0 && request != null)
                {
                    var invoice = db.DbInvoice.FirstOrDefault(c => c.Id == request.Id);
                    if (invoice == null)
                        return NotFound();
                    else
                    {
                        invoice.Name = request.Name;
                        invoice.Status = request.Status;
                        db.SaveChanges();
                    }
                }
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceExists(int id)
        {
            return db.DbInvoice.Count(e => e.Id == id) > 0;
        }
    }
}