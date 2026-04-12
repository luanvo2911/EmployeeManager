using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.WebAPI.Model
{
    public class EmployeeModel
    {
        public long id { get; set; }
        public string fullName { get; set; }
        public string department { get; set; }
        public string position { get; set; }
        public DateOnly hireDate {  get; set; }
        public string salary { get; set; }
        public string status { get; set; }
    }
}
