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
        [ResponseType(typeof(IEnumerable<Comp>))]
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

        /// <summary>
        /// Get a list of classes in the database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of classes associated with the team</returns>
        /// 

        [ResponseType(typeof(IEnumerable<Class>))]
        public IHttpActionResult GetClassesForComp(int id)
        {
            List<Class> Classes = db.Classes.Where(p => p.Comp.CompID == id).ToList();
            List<ClassDto> ClassDtos = new List<ClassDto> { };

            //choosing information exposed to API
            foreach (var Class in Classes)
            {
                ClassDto NewClass = new ClassDto
                {
                    ClassID = Class.ClassID,
                    ClassName = Class.ClassName,
                    ClassSpec = Class.ClassSpec,
                    ClassPic = Class.ClassPic
                };
                ClassDtos.Add(NewClass);
            }
            return Ok(ClassDtos);
        }

        /// <summary>
        /// Find a comp in the database using ID
        /// </summary>
        /// <param name="id"></param
        /// <returns>Information about the comp</returns>
        /// GET Request to api/CompData/FindComp/(id)

        [HttpGet]
        [ResponseType(typeof(Comp))]
        public IHttpActionResult FindComp(int id)
        {
            //Finding the data
            Comp Comp = db.Comps.Find(id);
            //if not found return 404
            if (Comp == null)
            {
                return NotFound();
            }

            //put into dto
            Comp CompDto = new Comp
            {
                CompID = Comp.CompID,
                CompName = Comp.CompName,
                CompClass1 = Comp.CompClass1,
                CompClass2 = Comp.CompClass2,
                CompClass3 = Comp.CompClass3
            };
            Debug.WriteLine(Comp);
            //pass along data
            return Ok(CompDto);
        }

        /// <summary>
        /// Update a Comp in the database given information about the comp
        /// </summary>
        /// <param name="id">Comp ID</param>
        /// <param name="comp">Comp object, received as POST data</param>
        /// <returns></returns>
        

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateComp(int id, [FromBody] Comp Comp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Comp.CompID)
            {
                return BadRequest();
            }

            db.Entry(Comp).State = EntityState.Modified;

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

        /// <summary>
        /// Add a comp to the database
        /// </summary>
        /// <param name="comp">Comp object</param>
        /// <returns></returns>
        // POST: api/Comps
        [ResponseType(typeof(Comp))]
        public IHttpActionResult AddComp([FromBody]Comp Comp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comps.Add(Comp);
            db.SaveChanges();

            return Ok(Comp.CompID);
        }

        /// <summary>
        /// Delete a comp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Comps/5
        [HttpPost]
        public IHttpActionResult DeleteComp(int id)
        {
            Comp Comp = db.Comps.Find(id);
            if (Comp == null)
            {
                return NotFound();
            }

            db.Comps.Remove(Comp);
            db.SaveChanges();

            return Ok(Comp);
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