using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainObjects
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
       
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }


        public override bool Equals(object obj)
        {
            var compareTo = obj as BaseEntity;

            if (ReferenceEquals(this, compareTo)) 
                return true;

            if (ReferenceEquals(null, compareTo))
                return false;


            return Id.Equals(compareTo);
        }

        public static bool operator ==(BaseEntity a,BaseEntity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if(ReferenceEquals(a,null) || ReferenceEquals(b,null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntity a, BaseEntity b)
                        => !(a == b);

        public override int GetHashCode()
                        => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString()
                        => $"{GetType().Name} [Id={Id}]";
        

    }
}
