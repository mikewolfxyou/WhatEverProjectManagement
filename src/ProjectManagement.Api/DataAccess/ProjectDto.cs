namespace ProjectManagement.Api.DataAccess
{
    public class ProjectDto
    {
        public int Id { set; get; } = 0;
        
        public string Name { set; get; }
        
        public int State { set; get; }
        
        public int Owner { set; get; }
        
        public string Participant { set; get; }
        
        public float Progress {set; get; }
    }
}