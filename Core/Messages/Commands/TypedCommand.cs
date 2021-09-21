using Core.Messages.Commands.Base;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages.Integration
{
    public class TypedCommand<T> : BaseCommand, IRequest<T>
    {
        public T Property { get; set; }

        protected TypedCommand():base(DateTime.Now)
        {
            
        }

    }


    public class TypedCollectionCommand<T> : BaseCommand, IRequest<IEnumerable<T>>
    {

        public TypedCollectionCommand():base(DateTime.Now)
        {

        }
    }
}
