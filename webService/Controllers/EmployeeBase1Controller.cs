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
using webService;

namespace webService.Controllers
{
    public class EmployeeBase1Controller : ApiController
    {
        private Entities db = new Entities();

        // GET: api/EmployeeBase1
        public IQueryable<EmployeeBase1> GetEmployeeBase1()
        {
            return db.EmployeeBase1;
        }

        // GET: api/EmployeeBase1/5
        [ResponseType(typeof(EmployeeBase1))]
        public IHttpActionResult GetEmployeeBase1(string id)
        {
            EmployeeBase1 employeeBase2 = null;
            if (Int32.TryParse(id, out int c))
            {
                int temp = Int32.Parse(id);
                List<EmployeeBase1> employeeBase1 = db.EmployeeBase1.Where(d => d.DepartmentId == temp).ToList();
                if (employeeBase1 == null)
                {
                    return NotFound();
                }
                return Ok(employeeBase1);
            }
            else
                
                if (id != "")
            {
                if (db.EmployeeBase1.Where(d => d.Name == id).Any())
                    employeeBase2 = db.EmployeeBase1.Where(d => d.Name == id).First();
                else if (db.EmployeeBase1.Where(d => d.Lastname == id).Any())
                    employeeBase2 = db.EmployeeBase1.Where(d => d.Lastname == id).First();
                else return NotFound();
            }
            return Ok(employeeBase2);
        }

        // PUT: api/EmployeeBase1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeBase1(int id, EmployeeBase1 employeeBase1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeBase1.Id)
            {
                return BadRequest();
            }

            db.Entry(employeeBase1).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeBase1Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmployeeBase1
        [ResponseType(typeof(EmployeeBase1))]
        public IHttpActionResult PostEmployeeBase1(EmployeeBase1 employeeBase1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeBase1.Add(employeeBase1);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeBase1.Id }, employeeBase1);
        }

        // DELETE: api/EmployeeBase1/5
        [ResponseType(typeof(EmployeeBase1))]
        public IHttpActionResult DeleteEmployeeBase1(int id)
        {
            EmployeeBase1 employeeBase1 = db.EmployeeBase1.Find(id);
            if (employeeBase1 == null)
            {
                return NotFound();
            }

            db.EmployeeBase1.Remove(employeeBase1);
            db.SaveChanges();

            return Ok(employeeBase1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeBase1Exists(int id)
        {
            return db.EmployeeBase1.Count(e => e.Id == id) > 0;
        }
    }
}