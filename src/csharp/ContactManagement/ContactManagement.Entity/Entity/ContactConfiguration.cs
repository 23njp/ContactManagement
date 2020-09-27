using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Entity.Entity
{
    public class ContactConfiguration: EntityTypeConfiguration<Contact>
    {
        public ContactConfiguration()
        {
            this.Property(contact => contact.FirstName)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(100);
            this.Property(contact => contact.LastName)
                .IsUnicode(false)
                .HasMaxLength(100);
            this.Property(contact => contact.Email)
                .IsUnicode(false)
                .HasMaxLength(255);
        }
    }
}
