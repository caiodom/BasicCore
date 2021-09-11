

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
    public class MainContext : DbContext
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

      
       
        
    }
}
