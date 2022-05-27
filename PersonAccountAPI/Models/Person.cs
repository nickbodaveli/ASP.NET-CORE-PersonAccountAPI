using Microsoft.EntityFrameworkCore;

namespace PersonAccountAPI.Models
{
    [Index(nameof(BankAccount), IsUnique = true)]
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string BankAccount { get; set; }
        public Bank? Bank { get; set; }
    }
}
