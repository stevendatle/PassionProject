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
using Passionproject.Models;

namespace Passionproject.Controllers
{
    public class CompsController : ApiController
    {
        private WowDbContext db = new WowDbContext();

        // GET: api/Comps
        public IQueryable<Comp> GetComps()
        {
            return db.Comps;
        }

        // GET: api/Comps/5
        [ResponseType(typeof(Comp))]
        public IHttpActionResult GetComp(int id)
        {
            Comp comp = db.Comps.Find(id);
            if (comp == null)
            {
                return NotFound();
            }

            return Ok(comp);
        }

        // PUT: api/Comps/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComp(int id, Comp comp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comp.CompID)
            {
                return BadRequest();
            }

            db.Entry(comp).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompExists(id))
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

        // POST: api/Comps
        [ResponseType(typeof(Comp))]
        public IHttpActionResult PostComp(Comp comp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comps.Add(comp);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comp.CompID }, comp);
        }

        // DELETE: api/Comps/5
        [ResponseType(typeof(Comp))]
        public IHttpActionResult DeleteComp(int id)
        {
            Comp comp = db.Comps.Find(id);
            if (comp == null)
            {
                return NotFound();
            }

            db.Comps.Remove(comp);
            db.SaveChanges();

            return Ok(comp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompExists(int id)
        {
            return db.Comps.Count(e => e.CompID == id) > 0;
        }
    }
}