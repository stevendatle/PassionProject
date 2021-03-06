﻿using System;
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
    public class ClassDataController : ApiController
    {
        //connection to DB
        private WowDbContext db = new WowDbContext();

        /// <summary>
        /// Gets a list of classes that are in the database
        /// </summary>
        /// <returns>List of classes</returns>
        // GET: api/ClassData
        [ResponseType(typeof(IEnumerable<Class>))]
        public IHttpActionResult GetClasses()
        {
            List<Class> Classes = db.Classes.ToList();
            List<ClassDto> ClassDtos = new List<ClassDto> { };

            //choosing information to be shown
            foreach (var Class in Classes)
            {
                ClassDto NewClass = new ClassDto
                {
                    ClassID = Class.ClassID,
                    ClassName = Class.ClassName,
                    ClassPic = Class.ClassPic
                };
                ClassDtos.Add(NewClass);
            }

            return Ok(ClassDtos);
        }

        /// <summary>
        /// Find a class in the database using its ID
        /// </summary>
        /// <param name="id">2</param>
        /// <returns>Returns the class with an id of 2</returns>

        // PUT: api/ClassData/5
        [HttpGet]
        [ResponseType(typeof(Class))]
        public IHttpActionResult FindClass(int id)
        {
            //Getting data from DB
            Class Class = db.Classes.Find(id);
            //returns 404 if not found
            if (Class == null)
            {
                return NotFound();
            }

            //DTO
            Class ClassDto = new Class
            {
                ClassID = Class.ClassID,
                ClassName = Class.ClassName,
                ClassPic = Class.ClassPic,
            };

            return Ok(ClassDto);
          
           
        }

        /// <summary>
        /// Update a class in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateClass(int id, [FromBody] Class Class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Class.ClassID)
            {
                return BadRequest();
            }

            db.Entry(Class).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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
        /// Delete a class from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

       
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteClass(int id)
        {
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return NotFound();
            }

            db.Classes.Remove(@class);
            db.SaveChanges();

            return Ok(@class);
        }

        /// <summary>
        /// Add a class to the database
        /// </summary>
        /// <param name="disposing"></param>
        [ResponseType(typeof(Class))]
        public IHttpActionResult AddClass ([FromBody]Class Class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Classes.Add(Class);
            db.SaveChanges();

            return Ok(Class.ClassID);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassExists(int id)
        {
            return db.Classes.Count(e => e.ClassID == id) > 0;
        }
    }
}