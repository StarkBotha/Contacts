using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {

        }

        public virtual DbSet<Contact> Contacts {get; set;}
    }
}