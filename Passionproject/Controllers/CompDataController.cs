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
using System.Diagnostics;

namespace Passionproject.Controllers
{
    /// <summary>
    
    /// </summary>
    public class CompDataController : ApiController
    {
        //Database access point
        private WowDbContext db = new WowDbContext();

        /// <summary>
        /// Get a list of comps in the database
        /// </summary>
        /// <returns>List of comps including their id, name, and classes</returns>
        [ResponseType(typeof(IEnumerable<CompDto>))]
        public IHttpActionResult GetComps()
        {
            List<Comp> Comps = db.Comps.ToList();
            List<CompDto> CompDtos = new List<CompDto> { };

            //Choosing the information exposed to the API
            foreach (var Comp in Comps)
            {
                CompDto NewComp = new CompDto
                {
                    CompID = Comp.CompID,
                    CompName = Comp.CompName,
                    CompClass1 = Comp.CompClass1,
                    CompClass2 = Comp.CompClass2,
                    CompClass3 = Comp.CompClass3
                };
                CompDtos.Add(NewComp);
            }

            return Ok(CompDtos);
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