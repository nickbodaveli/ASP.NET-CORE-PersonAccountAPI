using Microsoft.EntityFrameworkCore;
using PersonAccountAPI.Data;
using PersonAccountAPI.Helpers.Interfaces;
using PersonAccountAPI.IRepository;
using PersonAccountAPI.Models;
using PersonAccountAPI.Models.DTOs;

namespace PersonAccountAPI.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public PersonRepository(DataContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<Person> GetPersonAsync(int Id)
        {
            var _person = _unitOfWork.Query<Person>().AsQueryable();
            var _bank = _unitOfWork.Query<Bank>().AsQueryable();

            var getById =
                from person in _person
                join bank in _bank on person.Id
                equals bank.PersonsId
                into nikasbank
                from m in nikasbank.DefaultIfEmpty()
                where person.Id == Id
                select new Person
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Age = person.Age,
                    BankAccount = person.BankAccount,
                    Bank = new Bank()
                    {
                        Id = m.Id,
                        BankName = m.BankName,
                        BankCode = m.BankCode,
                        PersonsId = m.PersonsId
                    }
                };

            return await getById.FirstOrDefaultAsync();
        }

        public async Task<Person> GetPersonByKeywordAsync(string keyword)
        {
            var _person = _unitOfWork.Query<Person>().AsQueryable();
            var _bank = _unitOfWork.Query<Bank>().AsQueryable();

            var getByName =
                from person in _person
                join bank in _bank on person.Id
                equals bank.PersonsId
                into nikasbank
                from m in nikasbank.DefaultIfEmpty()
                where person.FirstName.Contains(keyword) || person.LastName.Contains(keyword)
                select new Person
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Age = person.Age,
                    BankAccount = person.BankAccount,
                    Bank = new Bank()
                    {
                        Id = m.Id,
                        BankName = m.BankName,
                        BankCode = m.BankCode,
                        PersonsId = m.PersonsId
                    }
                };

            return await getByName.FirstOrDefaultAsync();
        }

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            var _person = _unitOfWork.Query<Person>().AsQueryable();
            var _bank = _unitOfWork.Query<Bank>().AsQueryable();

            var getAll = (from person in _person
                          join bank in _bank on person.Id
                          equals bank.PersonsId
                          select new Person
                          {
                              Id = person.Id,
                              FirstName = person.FirstName,
                              LastName = person.LastName,
                              Age = person.Age,
                              BankAccount = person.BankAccount,
                              Bank = new Bank()
                              {
                                  Id = bank.Id,
                                  BankName = bank.BankName,
                                  BankCode = bank.BankCode,
                                  PersonsId = bank.PersonsId
                              }
                          });

            return await getAll.ToListAsync();
        }


        public async Task<List<Person>> GetAllPersonsByKeywordAsync(string keyword)
        {
            var _person = _unitOfWork.Query<Person>().AsQueryable();
            var _bank = _unitOfWork.Query<Bank>().AsQueryable();

            var getAll = (from person in _person
                          join bank in _bank on person.Id
                          equals bank.PersonsId
                          where person.FirstName.Contains(keyword) || person.LastName.Contains(keyword)
                          select new Person
                          {
                              Id = person.Id,
                              FirstName = person.FirstName,
                              LastName = person.LastName,
                              Age = person.Age,
                              BankAccount = person.BankAccount,
                              Bank = new Bank()
                              {
                                  Id = bank.Id,
                                  BankName = bank.BankName,
                                  BankCode = bank.BankCode,
                                  PersonsId = bank.PersonsId
                              }
                          });

            return await getAll.ToListAsync();
        }


        public async Task<Person> AddSPersonAsync(PersonDto addPerson)
        {
            var _person = new Person();
            _person.FirstName = addPerson.FirstName;
            _person.LastName = addPerson.LastName;
            _person.Age = addPerson.Age;
            _person.BankAccount = addPerson.BankAccount;

            _person.Bank = new Bank();
            _person.Bank.BankName = addPerson.bankDto.BankName;
            _person.Bank.BankCode = addPerson.bankDto.BankCode;

            _unitOfWork.Add(_person);
            await _unitOfWork.CommitAsync();
            return _person;
        }

        public async Task<Person> UpdatePersonAsync(int Id, PersonUpdateDto updatedPerson)
        {
            var getById = GetPersonAsync(Id).Result;

            if (getById != null)
            {
                getById.BankAccount = updatedPerson.BankAccount;
                _unitOfWork.ChangeTracker();
                _unitOfWork.Update(getById);
                _unitOfWork.CommitAsync();
            }

            return getById;
        }

        public async Task<bool> DeletePersonAsync(int Id)
        {
            var getById = GetPersonAsync(Id).Result;
            if (getById == null)
            {
                return false;
            }
            _unitOfWork.Remove(getById);
            _unitOfWork.CommitAsync();

            return true;
        }
    }
}
