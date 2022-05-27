using PersonAccountAPI.Models;
using PersonAccountAPI.Models.DTOs;

namespace PersonAccountAPI.IRepository
{
    public interface IPersonRepository
    {
        Task<Person> GetPersonAsync(int Id);
        Task<Person> GetPersonByKeywordAsync(string keyword);
        Task<List<Person>> GetAllPersonsAsync();
        Task<List<Person>> GetAllPersonsByKeywordAsync(string keyword);
        Task<Person> AddSPersonAsync(PersonDto addModel);
        Task<Person> UpdatePersonAsync(int Id, PersonUpdateDto updatedPerson);
        Task<bool> DeletePersonAsync(int Id);
    }
}
