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
using VirtualStore.Models;
using System.Diagnostics;
using VirtualStore.Migrations;


namespace VirtualStore.Controllers
{
    public class VaporizerDataController : ApiController
    {
        //utilize database connection
        private ApplicationDbContext db = new ApplicationDbContext();


        //list vaporizers

        // GET: api/VaporizerData/ListVaporizer
        [HttpGet]
        [Route("api/VaporizerData/ListVaporizer")]
        public IEnumerable<VaporizerDto> ListVaporizer()
        {
            List<Vaporizer> Vaporizers = db.Vaporizers.ToList();
            List<VaporizerDto> VaporizerDtos = new List<VaporizerDto>();

            Vaporizers.ForEach(v => VaporizerDtos.Add(new VaporizerDto()
            {
                vaporizerID = v.vaporizerId,
                VaporizerName = v.VaporizerName,
                Profit = v.Profit,
                FlavourName = v.FlavourName,
                SupplierName = v.supplier.SupplierName,
                CategoryName = v.Category.CategoryName,
                CustomerRatings = v.CustomerRatings,

            }));

            return VaporizerDtos;
        }

        //find vaporizers
        // GET: api/VaporizerData/FindVaporizer/5

        [ResponseType(typeof(Vaporizer))]
        [HttpGet]
        [Route("api/VaporizerData/FindVaporizer/{id}")]
        
        public IHttpActionResult FindVaporizer(int id)
        {
            Vaporizer Vaporizer = db.Vaporizers.Find(id);
            VaporizerDto VaporizerDto = new VaporizerDto()
            {
                vaporizerID = Vaporizer.vaporizerId,
                VaporizerName = Vaporizer.VaporizerName,
                Profit = Vaporizer.Profit,
                FlavourName = Vaporizer.FlavourName,
                SupplierName = Vaporizer.supplier.SupplierName,
                CategoryName=Vaporizer.Category.CategoryName,
                CustomerRatings=Vaporizer.CustomerRatings,  
            };
            if (Vaporizer == null)

            {
                return NotFound();
            }



            return Ok(VaporizerDto);
        }

        //add vaporizer

        // POST: api/VaporizerData/AddVaporizer
        [ResponseType(typeof(Vaporizer))]
        [HttpPost]
        [Route("api/VaporizerData/AddVaporizer")]
       
        public IHttpActionResult AddVaporizer(Vaporizer Vaporizer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Check if vaporizerId is null or empty
            if (Vaporizer.vaporizerId==0)
            {
                // If vaporizerId is not set correctly, return BadRequest
                ModelState.AddModelError("vaporizerId", "vaporizerId cannot be null or empty.");
                return BadRequest(ModelState);
            }


            db.Vaporizers.Add(Vaporizer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Vaporizer.vaporizerId }, Vaporizer);
        }
        //Update Vaporizer

        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/VaporizerData/UpdateVaporizer/{id}")]
        public IHttpActionResult UpdateVaporizer(int id, Vaporizer vaporizer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vaporizer.vaporizerId)
            {

                return BadRequest();
            }

            db.Entry(vaporizer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vaporizerExists(id))
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
        private bool vaporizerExists(int id)
        {
            return db.Vaporizers.Any(v => v.vaporizerId == id);
        }





        //delete vaporizer
        // POST: api/VaporizerData/DeleteVaporizer/5
        [ResponseType(typeof(Vaporizer))]
        [HttpPost]
        [Route("api/VaporizerData/DeleteVaporizer/{id}")]
        public IHttpActionResult DeleteVaporizer(int id)
        {
            Vaporizer vaporizer = db.Vaporizers.Find(id);
            if (vaporizer == null)
            {
                return NotFound();
            }

            db.Vaporizers.Remove(vaporizer);
            db.SaveChanges();

            return Ok();
        }

        
    }

    //Database transfer class
    public class VaporizerDto
    {
        public int vaporizerID { get; set; }
        public string VaporizerName { get; set; }

        //profit is in percentage
        public decimal Profit { get; set; }

        public string FlavourName { get; set; }
        public string SupplierName { get; set; }
        public string CategoryName { get; set; }
        public string CustomerRatings { get; set; }

        




    }

   

       
        
        
        






    
}

