using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VirtualStore.Models
{
    public class Category
    {

        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }
        public string LiquidRequired { get; set; }
        public string NicotineLevel { get; set; }


    }
}