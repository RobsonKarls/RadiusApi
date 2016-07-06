using System;
using System.Collections.Generic;
using System.Reflection;

namespace Freedom.Domain.Core
{
    public abstract class ValueObject : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((ValueObject)obj);
        }
        protected bool Equals(ValueObject other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }
    }

    public abstract class ValueObject<TValueObject> : ValueObject where TValueObject : IEntity
    {
        public static TValueObject GetFromId(int value)
        {
            Type type = typeof(TValueObject);

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                var fieldValue = (TValueObject)field.GetValue(null);

                if (fieldValue.Id == value)
                {
                    return fieldValue;
                }

            }
            throw new KeyNotFoundException(string.Concat(value, "cannot be found in any static fields."));
        }
    }
}
