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


         Task<ActionResult> GetById(Guid id);


         Task<ActionResult> Post(TSrc entity);


         Task<ActionResult> Put(Guid id, TSrc entity);


         Task<ActionResult> Delete(TSrc entity);

        Task<ActionResult> Delete(Guid id);




    }
}
