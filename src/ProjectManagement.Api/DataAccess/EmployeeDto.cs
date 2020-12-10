namespace ProjectManagement.Api.DataAccess
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int DepartmentId { get; set; }
        
        public int EmployeeRoleId { get; set; }
        
        public string IdentifyId { get; set; }
    }
}