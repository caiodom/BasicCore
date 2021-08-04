

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

      
        public virtual async Task<bool> CommitAsync(params Action[] actionValidations)
        {
            ValidationHandler(actionValidations);
            return await base.SaveChangesAsync() > 0;
        }

        public virtual bool Commit(params Action[] actionValidations)
        {
            ValidationHandler(actionValidations);
            return base.SaveChanges() > 0;
        }

        private void ValidationHandler(params Action[] actionValidations)
        {
            if (actionValidations != null)
                foreach (var actionValidation in actionValidations)
                    actionValidation.Invoke();

            
        }
        
    }
}
