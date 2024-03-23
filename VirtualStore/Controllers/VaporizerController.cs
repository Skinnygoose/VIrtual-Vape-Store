using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using VirtualStore.Models;
using System.Web.Script.Serialization;


namespace VirtualStore.Controllers
{
    public class VaporizerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static VaporizerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44311/api/");
        }

        // GET: Vaporizer/List

        
        public ActionResult List()
        {
            


            string url = "Vaporizerdata/listVaporizer";
            HttpResponseMessage response = client.GetAsync(url).Result;


            IEnumerable<Vaporizer> vaporizers = response.Content.ReadAsAsync<IEnumerable<Vaporizer>>().Result;
            


            return View(vaporizers);
        }

        // GET: Vaporizer/Details/5
        
        public ActionResult Details(int id)
        {
            //objective: communicate with our Vaporizer data api to retrieve one Vaporizer
            

            string url = "VaporizerData/FindVaporizer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

           

            Vaporizer selectedVaporizer = response.Content.ReadAsAsync<Vaporizer>().Result;
           


            return View(selectedVaporizer);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Vaporizer/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Vaporizer/Create
        [HttpPost]
        
        
        public ActionResult Create(Vaporizer vaporizer)
        {
            Debug.WriteLine("the json payload is :");
            
            string url = "VaporizerData/AddVaporizer";


            string jsonpayload = jss.Serialize(vaporizer);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Vaporizer/List");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: Vaporizer/Edit/5
        public ActionResult Edit(int id)
        {
            //grab the Vaporizer information

          

            string url = "VaporizerData/UpdateVaporizer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            

            Vaporizer selectedVaporizer = response.Content.ReadAsAsync<Vaporizer>().Result;
            

            return View(selectedVaporizer);
        }

        // POST: Vaporizer/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Vaporizer vaporizer)
        {
            try
            {
               

                //serialize into JSON
                //Send the request to the API

                string url = "/VaporizerData/UpdateVaporizer/" + id;


                string jsonpayload = jss.Serialize(vaporizer);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                
                HttpResponseMessage response = client.PostAsync(url, content).Result;




                return RedirectToAction("Details/" + id);
            }
            catch
            {
                return View();
            }









        }

        // GET: Vaporizer/Delete/5
        public ActionResult Delete(int id)
        {
            string url = "VaporizerData/FindVaporizer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Vaporizer selectedVaporizer = response.Content.ReadAsAsync<Vaporizer>().Result;
            return View(selectedVaporizer);
        }

        // POST: Vaporizer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "VaporizerData/DeleteVaporizer/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
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
    }
}
