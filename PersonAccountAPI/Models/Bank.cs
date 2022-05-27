using System.Text.Json.Serialization;

namespace PersonAccountAPI.Models
{
    
    public class Bank
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        //[JsonIgnore]
        public int PersonsId { get; set; }
        [JsonIgnore]
        public Person? Persons { get; set; }
    }
}
