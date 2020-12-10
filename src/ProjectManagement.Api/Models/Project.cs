using System.Collections.Generic;

namespace ProjectManagement.Api.Models
{
    public class Project
    {
        public int? Id { set; get; }
        
        public string Name { set; get; }
        
        public ProjectState State { set; get; }
        
        public Employee Owner { set; get; }
        
        public IEnumerable<Employee> Participants { set; get; }
        
        public float Progress {set; get; }
    }
}