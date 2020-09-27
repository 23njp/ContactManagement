using ContactManagement.Entity.Entity;
using ContactManagement.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.DAL.Model
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(IUnitOfWork<AppDbContext> unitOfWork) : base(unitOfWork)
        {
        }
        public ContactRepository(AppDbContext context) : base(context)
        {
        }

        public Contact GetContactByEmail(string Email)
        {
            return Context.Contacts.Where(c => c.Email.Equals(Email)).FirstOrDefault();
        }

        public IEnumerable<Contact> GetActiveContacts()
        {
            return Context.Contacts.Where(c => c.IsActive);
        }

        public IEnumerable<Contact> GetInActiveContacts()
        {
            return Context.Contacts.Where(c => !c.IsActive);
        }
    }
}
