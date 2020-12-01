using DVL_TBC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVL_TBC.Domain.Abstract
{
    public interface IPersonsRepository
    {
        Task<Person> GetAsync(int personId);

        Task AddAsync(Person person);

        Task DeleteAsync(int personId);

        Task<List<Person>> ListAsync(string? firstName, string? lastName, string? privateNumber, int? itemsPerPage, int? currentPageNumber);

        Task<List<Person>> ListAsync(string? firstName, string? lastName, string? privateNumber,
            int? id, Gender? gender, DateTime? birthDate, int? cityId, string? cityName,
            string? profilePictureRelativePath,
            int? itemsPerPage, int? currentPageNumber);

        Task EditAsync(int personId, string? firstName, string? lastName, Gender? gender, string? privateNumber,
            DateTime? birthDate, int? cityId, List<PhoneNumber>? phoneNumbers);

        Task ChangeProfilePictureAsync(int personId, string relativePath);
    }
}
