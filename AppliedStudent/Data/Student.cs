using System.ComponentModel.DataAnnotations;

namespace AppliedStudent.Data
{
    public class Student
    
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public decimal PhoneNumber { get; set; }

        public string Address { get; set; }
        
    }
}
