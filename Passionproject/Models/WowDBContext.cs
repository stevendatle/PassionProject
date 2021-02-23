using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;

namespace Passionproject.Models
{

    public class WowDbContext : DbContext
    {

        //Instructions to set the models as tables in our DB
        public DbSet<Class> Classes { get; set; }

        public DbSet<Comp> Comps { get; set; }

        public WowDbContext() : base("name=WowDBContext")
        {
            
        }

    }

}