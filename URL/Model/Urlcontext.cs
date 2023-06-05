using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace URL.Model
{
    public class Urlcontext:DbContext
    {

        public DbSet<Url> Urls { get; set; }
        public Urlcontext(DbContextOptions option) : base(option)
        {

        }
    }
}
