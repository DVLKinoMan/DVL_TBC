using DVL_TBC.Domain.Models;
using System;
using System.Collections.Generic;

namespace DVL_TBC.Domain.Abstract
{
    public interface IPersonsRepository
    {
        Person Get(int personId);

        void Add(Person person);

        void Delete(int personId);

        List<Person> List(string? firstName, string? lastName, string? privateNumber, int? itemsPerPage, int? currentPageNumber);

        List<Person> List(string? firstName, string? lastName, string? privateNumber,
            int? id, Gender? gender, DateTime? birthDate, int? cityId, string? cityName,
            string? profilePictureRelativePath,
            int? itemsPerPage, int? currentPageNumber);

        //todo phoneNumbers maybe in another method
        void Edit(int personId, string? firstName, string? lastName, Gender? gender, string? privateNumber,
            DateTime? birthDate, int? cityId, List<PhoneNumber>? phoneNumbers);

        void ChangeProfilePicture(int personId, string relativePath);
    }
}
