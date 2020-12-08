using System.Collections.Generic;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ProjectManagement.Api.Models
{
    public class Project
    {
        public int? Id { set; get; }
        
        public string Name { set; get; }
        
        public ProjectState State { set; get; }
        
        public int OwnerEmployeeId { set; get; }
        
        public List<int> ParticipantEmployeeIds { set; get; }
        
        public double Progress {set; get; }
        
        
        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(State)}: {State}, {nameof(OwnerEmployeeId)}: {OwnerEmployeeId}, {nameof(ParticipantEmployeeIds)}: {JsonSerializer.Serialize(ParticipantEmployeeIds)}, {nameof(Progress)}: {Progress}";
        }
    }
}