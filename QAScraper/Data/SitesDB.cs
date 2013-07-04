using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QAScraper.Data
{
    using System.Data.Entity;

    using QAScraper.Models;

    public class SitesDB : DbContext
    {
        public DbSet<Site> Sites { get; set; }
    }
}