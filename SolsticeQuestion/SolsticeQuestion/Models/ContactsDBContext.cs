using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SolsticeQuestion.Models
{
    public class ContactsDBContext : DbContext
    {
        public ContactsDBContext(DbContextOptions<ContactsDBContext> options)
           : base(options)
        {
        }

        public DbSet<ContactItem> ContactItems { get; set; }
    }
}
