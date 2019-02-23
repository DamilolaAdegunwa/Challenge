using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using FWK.Domain.Interfaces.Entities;

namespace FWK.Domain.Entities
{ 

    [Serializable]
    public abstract class Entity : Entity<int>, IEntity, IEntity<int>
    {
    }


    [Serializable]
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey IdValue { get; set; }

        public virtual bool IsTransient()
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(this.IdValue, default(TPrimaryKey)))
                return true;
            if (typeof(TPrimaryKey) == typeof(int))
                return Convert.ToInt32((object)this.IdValue) <= 0;
            if (typeof(TPrimaryKey) == typeof(long))
                return Convert.ToInt64((object)this.IdValue) <= 0L;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || (object)(obj as Entity<TPrimaryKey>) == null)
                return false;
            if ((object)this == obj)
                return true;
            Entity<TPrimaryKey> entity = (Entity<TPrimaryKey>)obj;
            if (this.IsTransient() && entity.IsTransient())
                return false;
            Type type1 = this.GetType();
            Type type2 = entity.GetType();
            if (!type1.GetTypeInfo().IsAssignableFrom(type2) && !type2.GetTypeInfo().IsAssignableFrom(type1))
                return false;
          
            return this.IdValue.Equals((object)entity.IdValue);
        }

        public override int GetHashCode()
        {
            return this.IdValue.GetHashCode();
        }

        public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            if (object.Equals((object)left, (object)null))
                return object.Equals((object)right, (object)null);
            return left.Equals((object)right);
        }

        public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return string.Format("[{0} {1}]", (object)this.GetType().Name, (object)this.IdValue);
        }

        public virtual bool LogEntity()
        {
            return true;
        }

        public virtual string GetLogTableName()
        {
            return this.GetType().Name;
        }

        [NotMapped]
        public string LogOperation { get; set; }

        [NotMapped]
        public bool BypassLog { get; set; }

        public virtual string GetLogEntityName()
        {
            return IdValue.ToString();
        }
    }

}
