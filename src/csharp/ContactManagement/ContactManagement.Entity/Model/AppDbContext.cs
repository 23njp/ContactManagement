using ContactManagement.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Entity.Model
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(): base("ContactMgmt")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Adds configurations for Student from separate class
            modelBuilder.Configurations.Add(new ContactConfiguration());           
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
