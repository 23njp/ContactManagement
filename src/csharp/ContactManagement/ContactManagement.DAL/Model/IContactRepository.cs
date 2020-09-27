using ContactManagement.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.DAL.Model
{
    public interface IContactRepository: IGenericRepository<Contact>
    {
        Contact GetContactByEmail(string Email);
        IEnumerable<Contact> GetActiveContacts();
        IEnumerable<Contact> GetInActiveContacts();
    }
}
