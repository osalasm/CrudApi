namespace CrudSimple.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Salary { get; set; }
        public int IdOccupation { get; set; }
        public virtual Occupation Occupations { get; set; }
    }
}
