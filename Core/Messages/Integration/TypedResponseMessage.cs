using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages.Integration
{
    public class TypedResponseMessage<T>:ResponseMessage
    {

        public T Property { get; set; }
        public IEnumerable<T> Collection { get; set; }

        public TypedResponseMessage(T property,
                                    IEnumerable<T> collection,ValidationResult validation):base(validation)
        {
            Property = property;
            Collection = collection;
        }

    }
}
