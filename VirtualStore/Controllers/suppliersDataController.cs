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

namespace VirtualStore.Controllers
{
    public class suppliersDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


         //list vaporizers

        // GET: api/supplierData/ListSupplier
        [HttpGet]
        [Route("api/SupplierData/ListSupplier")]
        public IEnumerable<supplier> ListSupplierss()
        {
            List<supplier> Supplierss = db.Supplierss.ToList();
            List<SupplierDto> SupplierDtos = new List<SupplierDto>();

            Supplierss.ForEach(s => SupplierDtos.Add(new SupplierDto()
            {
                SupplierId = s.SupplierId,
                SupplierName = s.SupplierName,
                SupplierAddress = s.SupplierAddress,
                SupplierEmail= s.SupplierEmail,



            }));

            return (IEnumerable<supplier>)SupplierDtos;
        }

        //find vaporizers
        // GET: api/SupplierData/FindSupplier/5
        // GET: api/suppliers/5
        [ResponseType(typeof(supplier))]
        [HttpGet]
        [Route("api/SupplierData/FindSupplier/{id}")]

        public IHttpActionResult FindSuppliers(int id)
        {
            supplier supplier = db.Supplierss.Find(id);
            SupplierDto SupplierDto = new SupplierDto()
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                SupplierAddress = supplier.SupplierAddress,
                SupplierEmail = supplier.SupplierEmail,
            };
            if (supplier == null)

            {
                return NotFound();
            }



            return Ok(SupplierDto);
        }

        //Update Supplier
        // POST: api/SupplierData/DeleteSupplier/5
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/SupplierData/UpdateSupplier/{id}")]
        public IHttpActionResult UpdateSupplier(int id, supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplier.SupplierId)
            {

                return BadRequest();
            }

            db.Entry(supplier).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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
        private bool SupplierExists(int id)
        {
            return db.Supplierss.Any(s => s.SupplierId == id);
        }


        //add supplier

        // POST: api/SupplierData/AddSupplier
        // POST: api/suppliers
        [ResponseType(typeof(supplier))]
        [HttpPost]
        [Route("api/SupplierData/AddSupplier")]

        public IHttpActionResult AddSupplier(supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Check if SupplierId is null or empty
            if (supplier.SupplierId == 0)
            {
                // If supplierId is not set correctly, return BadRequest
                ModelState.AddModelError("supplierId", "supplierId cannot be null or empty.");
                return BadRequest(ModelState);
            }


            db.Supplierss.Add(supplier);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = supplier.SupplierId }, supplier);
        }

        //delete supplier
        // POST: api/VaporizerData/DeleteVaporizer/5
        [ResponseType(typeof(supplier))]
        [HttpPost]
        [Route("api/SupplierData/DeleteSupplier/{id}")]
        public IHttpActionResult DeleteSupplier(int id)
        {
            supplier supplier = db.Supplierss.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            db.Supplierss.Remove(supplier);
            db.SaveChanges();

            return Ok();
        }
    }
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierEmail { get; set; }
    }
}