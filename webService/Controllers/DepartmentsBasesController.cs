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
    public class DepartmentsBasesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/DepartmentsBases
        public IQueryable<DepartmentsBase> GetDepartmentsBase()
        {
            return db.DepartmentsBase;
        }

        // GET: api/DepartmentsBases/5
        [ResponseType(typeof(DepartmentsBase))]
        public IHttpActionResult GetDepartmentsBase(string id)
        {
            DepartmentsBase departmentsBase = null;
            if (Int32.TryParse(id, out int s))
            {
                departmentsBase = db.DepartmentsBase.Find(Int32.Parse(id));
                //return Ok(db.DepartmentsBase.Find(Int32.Parse(id)));
            }
            else if (id != "")
            {
                if(db.DepartmentsBase.Where(d => d.DepartmentName == id).Any())
                departmentsBase = db.DepartmentsBase.Where(d => d.DepartmentName == id).First();
                else return NotFound();
            }
            //return Ok(db.DepartmentsBase.Where(d => d.DepartmentName==id)); }
            else return NotFound();
            return Ok(departmentsBase);
        }

        // PUT: api/DepartmentsBases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartmentsBase(int id, DepartmentsBase departmentsBase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departmentsBase.Id)
            {
                return BadRequest();
            }

            db.Entry(departmentsBase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentsBaseExists(id))
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

        // POST: api/DepartmentsBases
        [ResponseType(typeof(DepartmentsBase))]
        public IHttpActionResult PostDepartmentsBase(DepartmentsBase departmentsBase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DepartmentsBase.Add(departmentsBase);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = departmentsBase.Id }, departmentsBase);
        }

        // DELETE: api/DepartmentsBases/5
        [ResponseType(typeof(DepartmentsBase))]
        public IHttpActionResult DeleteDepartmentsBase(int id)
        {
            DepartmentsBase departmentsBase = db.DepartmentsBase.Find(id);
            if (departmentsBase == null)
            {
                return NotFound();
            }

            db.DepartmentsBase.Remove(departmentsBase);
            db.SaveChanges();

            return Ok(departmentsBase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentsBaseExists(int id)
        {
            return db.DepartmentsBase.Count(e => e.Id == id) > 0;
        }
        private bool DepartmentsBaseExists(string id)
        {
            return db.DepartmentsBase.Count(e => e.DepartmentName == id) > 0;
        }
    }
}