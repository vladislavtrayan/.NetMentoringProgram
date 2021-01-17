using System;

namespace Tests.Entities
{
    public class User : IEquatable<User>
    {
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public bool Equals(User other)
        {
            return Name == other.Name;
        }
    }
}