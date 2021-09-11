using Core.DomainObjects;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class UnitOfWork<T>:IUnitOfWork<T> where T : BaseEntity, new()
    {
        private readonly MainContext _context;
        public UnitOfWork(MainContext ctx)
          {
            _context = ctx;
          }

        private IRepository<T> _baseRepo;
        public IRepository<T> BaseRepo
        {
            get => _baseRepo ?? (_baseRepo = new Repository<T>(_context));
        }



        public virtual async Task<bool> CommitAsync(params Action[] actionValidations)
        {
            ValidationHandler(actionValidations);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual bool Commit(params Action[] actionValidations)
        {
            ValidationHandler(actionValidations);
            return _context.SaveChanges() > 0;
        }

       

        private void ValidationHandler(params Action[] actionValidations)
        {
            if (actionValidations != null)
                foreach (var actionValidation in actionValidations)
                    actionValidation.Invoke();


        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
