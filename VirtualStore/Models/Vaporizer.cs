using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VirtualStore.Migrations;

namespace VirtualStore.Models
{
    public class Vaporizer
    {
        [Key]
        public int vaporizerId { get; set; }
        public string VaporizerName { get; set; }

        public string FlavourName { get; set; }
        //buying price is in Candadian Dollars
        public decimal BuyingPrice { get; set; }
        //Selling price is in canadian dollars
        public decimal SellingPrice { get; set; }
        //Customer rating has five levels (poor , average , okay, good , Very good)
        public string CustomerRatings { get; set; }
        //Profit reflects in percentage i.e. 23.64 reflects 23.64%
        public decimal Profit { get; set; }
        //A Vaporizer will have only one supplier


        [ForeignKey("supplier")]
        public int SupplierId { get; set; }

        public virtual supplier supplier { get; set; }



        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

    }


   


}