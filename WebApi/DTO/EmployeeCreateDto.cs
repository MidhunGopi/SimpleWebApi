public class EmployeeCreateDto
{
    public string EmpName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public IFormFile? ProfilePhoto { get; set; }
}
