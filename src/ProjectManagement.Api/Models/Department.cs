using System;

namespace ProjectManagement.Api.Models
{
    public class Department : IEquatable<Department>
    {
        public int Id { get; set; }
        
        public string DepartmentName { get; set; }

        public bool Equals(Department other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && DepartmentName == other.DepartmentName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Department) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, DepartmentName);
        }
    }
}