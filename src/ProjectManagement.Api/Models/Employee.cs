using System;

namespace ProjectManagement.Api.Models
{
    public class Employee : IEquatable<Employee>
    {
        public int Id;
        public string Name;
        public EmployeeRole EmployeeRole;
        public Department Department;

        public bool Equals(Employee other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Name == other.Name && Equals(EmployeeRole, other.EmployeeRole) && Equals(Department, other.Department);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Employee) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, EmployeeRole, Department);
        }
    }
}