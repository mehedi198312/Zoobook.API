using System.ComponentModel.DataAnnotations;

namespace Zoobook.Data
{
    public class Employee
    {
        [Key] public string Id { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}