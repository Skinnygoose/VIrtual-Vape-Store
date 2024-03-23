using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using VirtualStore.Models;
using System.Web.Script.Serialization;
using VirtualStore.Migrations;

namespace VirtualStore.Controllers
{
    public class SupplierController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static SupplierController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44311/api/");
        }








        // GET: Supplier
        public ActionResult List()
        {


            string url = "SupplierData/ListSupplier";
            HttpResponseMessage response = client.GetAsync(url).Result;


            IEnumerable<supplier> suppliers = response.Content.ReadAsAsync<IEnumerable<supplier>>().Result;



            return View(suppliers);




        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {

            //objective: communicate with our Vaporizer data api to retrieve one Vaporizer


            string url = "SupplierData/FindSupplier/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;



            supplier selectedSupplier = response.Content.ReadAsAsync<supplier>().Result;



            return View(selectedSupplier);
        }

        public ActionResult Error()
        {

            return View();
        }
        

        // GET: Supplier/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(supplier supplier)
        {


            Debug.WriteLine("the json payload is :");

            string url = "SupplierData/AddSupplier";


            string jsonpayload = jss.Serialize(supplier);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Supplier/List");
            }
            else
            {
                return RedirectToAction("Error");
            }









        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            

                //grab the Supplier information



            string url = "SupplierData/UpdateSupplier/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;



            supplier selectedSupplier = response.Content.ReadAsAsync<supplier>().Result;


            return View(selectedSupplier);



        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, supplier supplier)
        {

            try
            {


                //serialize into JSON
                //Send the request to the API

                string url = "/SupplierData/UpdateSupplier/" + id;


                string jsonpayload = jss.Serialize(supplier);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                
                //Header : Content-Type: application/json
                HttpResponseMessage response = client.PostAsync(url, content).Result;




                return RedirectToAction("Details/" + id);
            }
            catch
            {
                return View();
            }





        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            string url = "SupplierData/FindSupplier/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            supplier selectedSupplier = response.Content.ReadAsAsync<supplier>().Result;
            return View(selectedSupplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "SupplierData/DeleteSupplier/" + id;
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
