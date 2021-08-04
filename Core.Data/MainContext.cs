

using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class MainContext : DbContext, IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        public MainContext(DbContextOptions options,
                           IConfiguration configuration) 
                :base(options)
        {
            this._configuration = configuration;
        }

        public virtual void BaseOnConfiguringSqlServer(DbContextOptionsBuilder optionsBuilder,string connectionString)
        {
            optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(connectionString);
        }

     
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                    => optionsBuilder
                            .UseLazyLoadingProxies()
                            .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));*/








        private void RegistrationDateHandler()
        {
            foreach (var entry in ChangeTracker.Entries()
                                             .Where(entry => entry.Entity
                                                                .GetType()
                                                                .GetProperty("RegistrationDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("RegistrationDate").CurrentValue = DateTime.Now;

                    if (entry.GetType().GetProperty("Active") != null)
                        entry.Property("Active").CurrentValue = true;
                }


                if (entry.State == EntityState.Modified)
                {
                    entry.Property("RegistrationDate").IsModified = false;
                    entry.Property("ChangeDate").CurrentValue = DateTime.Now;
                }
            }
        }

        public virtual async Task<bool> CommitAsync()
        {
            RegistrationDateHandler();
            return await base.SaveChangesAsync() > 0;
        }

        public virtual bool Commit()
        {
            RegistrationDateHandler();

            return base.SaveChanges() > 0;
        }


        
    }
}
