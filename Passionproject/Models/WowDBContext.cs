using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Passionproject.Models
{

    public class WowDbContext : DbContext
    {
        public WowDbContext() : base("name=WowDBContext")
        {

        }

        //Instructions to set the models as tables in our DB
        public DbSet<Class> Classes { get; set; }

        public DbSet<Comp> Comps { get; set; }
    }

}