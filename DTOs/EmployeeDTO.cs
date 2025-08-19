namespace CrudSimple.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Salary { get; set; }
        public int IdOccupation { get; set; }
        public string OccupationName { get; set; } = string.Empty;
    }
}
