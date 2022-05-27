using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonAccountAPI.IRepository;
using PersonAccountAPI.Models;
using PersonAccountAPI.Models.DTOs;

namespace PersonAccountAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repository;

        public PersonController(IPersonRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("{id}", Name = "GetPersonById")]
        [ActionName("GetPersonById")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var _person = await _repository.GetPersonAsync(id);

            if (_person != null)
                return Ok(_person);
            else
                return NotFound();
        }

        [HttpGet("keyword", Name = "GetPersonByKeyword")]
        [ActionName("GetPersonByKeyword")]
        public async Task<IActionResult> GetPersonByNameId(string keyword)
        {
            var _person = await _repository.GetPersonByKeywordAsync(keyword);

            if (_person != null)
                return Ok(_person);
            else
                return NotFound();
        }

        [HttpGet(Name = "GetAllPerson")]
        [ActionName("GetAllPerson")]
        public async Task<IActionResult> GetAll()
        {
            var _person = await _repository.GetAllPersonsAsync();

            if (_person != null)
                return Ok(_person);
            else
                return NotFound();
        }

        [HttpGet("keyword", Name = "GetAllPersonsByKeyword")]
        [ActionName("GetAllPersonsByKeyword")]
        public async Task<IActionResult> GetAllPersonsByKeyword(string keyword)
        {
            var _person = await _repository.GetAllPersonsByKeywordAsync(keyword);

            if (_person != null)
                return Ok(_person);
            else
                return NotFound();
        }

        [HttpPost(Name = "Post")]
        [ActionName("Post")]
        public async Task<IActionResult> Post([FromBody] PersonDto addPerson)
        {
            var _person = await _repository.AddSPersonAsync(addPerson);
            return Ok(_person);
        }

        [HttpPut("{id}", Name = "Update")]
        [ActionName("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonUpdateDto updatedPerson)
        {
            var _person = await _repository.UpdatePersonAsync(id, updatedPerson);

            if (_person != null)
                return Ok(_person);
            else
                return NotFound();
        }

        [HttpDelete("{id}", Name = "Delete")]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var _person = await _repository.DeletePersonAsync(id);

            if (_person != false)
                return Ok(_person);
            else
                return NotFound();
        }
    }
}
