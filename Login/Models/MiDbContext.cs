using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Login.Models
{
    public class MiDbContext: DbContext
    {
        public DbSet<Usuario> usuario { get; set; }
    }
}