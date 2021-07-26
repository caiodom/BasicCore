using Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class MainContext : DbContext, IUnitOfWork
    {
        public MainContext(DbContextOptions options):base(options)
        {

        }

        public virtual async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries()
                                              .Where(entry => entry.Entity
                                                                 .GetType()
                                                                 .GetProperty("RegistrationDate") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("RegistrationDate").CurrentValue = DateTime.Now;


                if (entry.State == EntityState.Modified)
                    entry.Property("RegistrationDate").IsModified = false;

            }


            var success = await base.SaveChangesAsync() > 0;

            return success;
        }
    }
}
