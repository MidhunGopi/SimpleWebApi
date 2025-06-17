namespace DataAccess
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string? ProfilePhoto { get; set; }
    }
}
