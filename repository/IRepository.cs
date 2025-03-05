using NBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.repository
{
    internal interface IRepository<ID, E> where E: Entity<ID> where ID :IComparable<ID>
    {
        IEnumerable<E> FindAll();

        E FindOne(ID id);

        E Add(E entity);

        E Update(E entity);

        E Delete(ID id);

    }
}
