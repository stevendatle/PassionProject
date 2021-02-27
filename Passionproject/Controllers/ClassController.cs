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
    public class ClassController : Controller
    {
        //Connection to WEB API
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static ClassController()
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

        //get: comp/list
        public ActionResult List()
        {
            string url = "classdata/getclasses";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<Class> SelectedClasses = response.Content.ReadAsAsync<IEnumerable<Class>>().Result;
                return View(SelectedClasses);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    

        // GET: Class/Details/5
        public ActionResult Details(int id)
        {
            string url = "classdata/findclass/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //catching status code
            if (response.IsSuccessStatusCode)
            {
                //add data
                Class SelectedClass = response.Content.ReadAsAsync<Class>().Result;
                Debug.WriteLine(SelectedClass.ClassName);
                //  ViewModel.Class = SelectedClass;
                url = "compdata/getclassesforcomp/" + id;
                return View(SelectedClass);
            }
            return RedirectToAction("List");
        }

        // GET: Class/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Class/Create
        [HttpPost]
        public ActionResult Create(Class ClassInfo)
        {
            Debug.WriteLine(ClassInfo.ClassName);
            string url = "Classdata/addClass";
            Debug.WriteLine(jss.Serialize(ClassInfo));
            HttpContent content = new StringContent(jss.Serialize(ClassInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                int Classid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = Classid });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Class/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "classdata/findclass/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                //Put data into comp DTO
                Class SelectedClass = response.Content.ReadAsAsync<Class>().Result;
                return View(SelectedClass);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Class/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, Class ClassInfo)
        {
            Debug.WriteLine(ClassInfo.ClassName);
            string url = "classdata/updateclass/" + id;
            Debug.WriteLine(jss.Serialize(ClassInfo));
            HttpContent content = new StringContent(jss.Serialize(ClassInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Class/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "classdata/findclass/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                //Put data into comp dto
                Class SelectedClass = response.Content.ReadAsAsync<Class>().Result;
                return View(SelectedClass);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Class/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "classdata/delete/" + id;
            HttpContent content = new StringContent("");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}
