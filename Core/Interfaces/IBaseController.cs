using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBaseController<TSrc>
    {

         Task<IEnumerable<TSrc>> Get();


         Task<ActionResult<TSrc>> GetById(Guid id);


         Task<ActionResult<TSrc>> Post(TSrc entity);


         Task<ActionResult<TSrc>> Put(Guid id, TSrc entity);


         Task<ActionResult<TSrc>> Delete(TSrc entity);




    }
}
