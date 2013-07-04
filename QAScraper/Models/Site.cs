using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QAScraper.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Site
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
    }
}