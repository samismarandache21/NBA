using NBA.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.repository
{
    internal class InMemoryRepo<ID, E> : IRepository<ID, E> where E: Entity<ID> where ID : IComparable<ID>
    {
        protected IDictionary<ID, E> entities;
        protected ID? lastId;

        public InMemoryRepo() { 
            entities = new Dictionary<ID, E>();
            lastId = default;
        }

        public ID? LastID
        {
            get { return lastId; }
        }

        protected virtual void updateLastId()
        {
            lastId = entities.Keys.Max<ID>();
        }

        public IEnumerable<E> FindAll()
        {
            return entities.Values;
        }

        public E FindOne(ID id)
        {
            if(id == null) throw new ArgumentNullException("id");
            if(entities.ContainsKey(id)) return entities[id];
            return null;
        }

        public E Add(E entity)
        {
            if (entity is null) throw new ArgumentNullException("entity");
            if (entity.Id is null) throw new ArgumentNullException("id");

            if (entities.TryGetValue(entity.Id, out E? value))
                return entity;

            entities.Add(entity.Id, entity);
            updateLastId();
            return null;
        }

        public E Update(E entity)
        {
            if (entity is null) throw new ArgumentNullException("entity");
            if (entity.Id is null) throw new ArgumentNullException("id");

            if (!entities.ContainsKey(entity.Id))
                return entity;

            entities[entity.Id] = entity;
            return null;

        }

        public E Delete(ID id)
        {
            if (id == null) throw new ArgumentNullException("id");

            if (!entities.ContainsKey(id))
                return null;

            E value = entities[id];
            entities.Remove(id);
            updateLastId();
            return value;
        }
        
    }
}
