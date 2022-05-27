namespace PersonAccountAPI.Models.DTOs
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string BankAccount { get; set; }
        public BankDto bankDto { get; set; }
    }
}
