using System;
using System.Collections.Generic;

namespace ProjectManagement.Api.Models
{
    public class Project : IEquatable<Project>
    {
        public int? Id { set; get; }
        
        public string Name { set; get; }
        
        public ProjectState State { set; get; }
        
        public Employee Owner { set; get; }
        
        public IEnumerable<Employee> Participants { set; get; }
        
        public float Progress {set; get; }

        public bool Equals(Project other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Name == other.Name && State == other.State && Equals(Owner, other.Owner) && Equals(Participants, other.Participants) && Progress.Equals(other.Progress);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Project) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, (int) State, Owner, Participants, Progress);
        }
    }
}