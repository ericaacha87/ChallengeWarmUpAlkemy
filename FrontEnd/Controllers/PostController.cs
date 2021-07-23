using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            // call api and get all posts
            IEnumerable<GetAll> posts = null;
            using (var client = new HttpClient())
            {
               
                var result = client.GetAsync("https://localhost:44307/api/Post/GetAll").Result;

                if (result.IsSuccessStatusCode)
                {
                    posts = result.Content.ReadAsAsync<List<GetAll>>().Result;
                    
                }
                else
                {
                    posts = Enumerable.Empty<GetAll>();
                    ModelState.AddModelError(string.Empty, "Try Later.");
                }
            }
                return View("Index",posts);
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            GetDetails objGetDetails = null;
            using (var client = new HttpClient())
            {
                var result = client.GetAsync("https://localhost:44307/api/Post/GetDetails/"+ id).Result;

                if (result.IsSuccessStatusCode)
                {
                    objGetDetails = result.Content.ReadAsAsync<GetDetails>().Result;
                }
                else
                {
                   
                    ModelState.AddModelError(string.Empty, "Try Later.");
                }
            }
                return View(objGetDetails);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create(Create objCreate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        var json = JsonConvert.SerializeObject(objCreate);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");
                        var result = client.PostAsync("https://localhost:44307/api/Post/Create/", data).Result;

                        if (!result.IsSuccessStatusCode)
                        {

                            ModelState.AddModelError(string.Empty, "Try Later.");
                            return View();
                        }
                        return RedirectToAction("Index");
                    }
                   
                }
                else
                {
                    return View(objCreate);
                }
                    // TODO: Add insert logic here

                   
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Post/Edit/5
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

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Post/Delete/5
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
