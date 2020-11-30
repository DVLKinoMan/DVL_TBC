using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DVL_TBC.Domain.Abstract;
using DVL_TBC.Domain.Models;
using DVL_TBC.PersonsApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DVL_TBC.PersonsApi.Models;
using DVL_TBC.PersonsApi.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DVL_TBC.PersonsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly IPersonsRepository _personsRepo;
        private readonly string _profilePicturesFolderPath;
        private readonly string[] _profilePictureAllowedContentTypes;

        public PersonsController(ILogger<PersonsController> logger, IPersonsRepository personsRepo, IConfiguration configuration)
        {
            _logger = logger;
            _personsRepo = personsRepo;
            _profilePicturesFolderPath = configuration["ProfilePicturesFolderPath"];
            _profilePictureAllowedContentTypes = configuration["ProfilePictureAllowedContentTypes"]
                .Split(',', StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()).ToArray();
        }

        [HttpGet("Get/{personId}")]
        public async Task<ActionResult<GetPersonResponse>> GetPersonAsync(int personId)
        {
            var person = _personsRepo.Get(personId).ToGetPersonResponse();

            if (person.ProfilePictureRelativePath is { } path)
                person.ProfilePicture = await System.IO.File.ReadAllBytesAsync(Path.Combine(_profilePicturesFolderPath, path));

            return Ok(person);
        }

        [HttpGet("List")]
        public ActionResult<List<GeneralPersonResponse>> ListPersons(string? firstName, string? lastName,
            string? privateNumber, int? itemsPerPage, int? currentPageNumber)
            => Ok(_personsRepo.List(firstName, lastName, privateNumber,
                itemsPerPage, currentPageNumber).ToGeneralPersonResponses());

        [HttpGet("ListWithManyFilters")]
        public ActionResult<List<GeneralPersonResponse>> ListPersons(string? firstName, string? lastName, string? privateNumber,
            int? id, Gender? gender, DateTime? birthDate, int? cityId, string? cityName,
            string? profilePictureRelativePath,
            int? itemsPerPage, int? currentPageNumber)
            => Ok(_personsRepo.List(firstName, lastName, privateNumber,
                id, gender, birthDate, cityId, cityName, profilePictureRelativePath,
                itemsPerPage, currentPageNumber).ToGeneralPersonResponses());

        [HttpPost("Add")]
        public IActionResult AddPerson(AddPersonRequest addPersonRequest)
        {
            _personsRepo.Add(addPersonRequest.ToPerson());
            return Ok();
        }

        [HttpPost("Delete/{personId}")]
        public IActionResult DeletePerson(int personId)
        {
            _personsRepo.Delete(personId);
            return Ok();
        }

        [HttpPost("Edit/{personId}")]
        public IActionResult EditPerson(int personId, EditPersonRequest editPersonRequest)
        {
            //todo check if parameters are valid
            _personsRepo.Edit(personId, editPersonRequest.FirstName, editPersonRequest.LastName,
                editPersonRequest.Gender, editPersonRequest.PrivateNumber, editPersonRequest.BirthDate,
                editPersonRequest.CityId, editPersonRequest.PhoneNumbers);
            return Ok();
        }

        [HttpPost("Upload/ProfilePicture/{personId}")]
        public async Task<ActionResult> EditProfilePicture(int personId, IFormFile image)
        {
            if (!Directory.Exists(_profilePicturesFolderPath))
                throw new DirectoryNotFoundException(string.Format(Translations.ErrorDirectoryNotFound, _profilePicturesFolderPath));
            if (!_profilePictureAllowedContentTypes.Contains(image.ContentType))
                throw new ArgumentException(Translations.ErrorFileContentTypeNotValid, nameof(image));

            string filePath = Path.Combine(_profilePicturesFolderPath, image.FileName);

            try
            {
                await using var stream = new MemoryStream();
                await image.CopyToAsync(stream);

                await System.IO.File.WriteAllBytesAsync(filePath, stream.ToArray());

                _personsRepo.ChangeProfilePicture(personId, image.FileName);
            }
            catch
            {
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                throw;
            }

            return Ok();
        }
    }
}
