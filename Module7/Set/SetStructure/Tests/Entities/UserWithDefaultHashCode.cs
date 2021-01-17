using System;

namespace Tests.Entities
{
    public class UserWithDefaultHashCode : IEquatable<UserWithDefaultHashCode>
    {
        public string Name { get; set; }
        
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is User user)
                return Name == user.Name;
            return false;
        }
        
        public bool Equals(UserWithDefaultHashCode obj)
        {
            if (obj == null) return false;
            return Name == obj.Name;
        }
    }
}