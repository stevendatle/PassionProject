using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Passionproject.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace Passionproject.Controllers
{
    public class CompController : Controller
    {

        //using HTTP Client to connect to web api
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;
        
        static CompController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("http://localhost:50956/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }



        // GET: Comp/List
        public ActionResult List()
        {
            string url = "compdata/getcomps";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<CompDto> SelectedComps = response.Content.ReadAsAsync<IEnumerable<CompDto>>().Result;
                return View(SelectedComps);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Comp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comp/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
