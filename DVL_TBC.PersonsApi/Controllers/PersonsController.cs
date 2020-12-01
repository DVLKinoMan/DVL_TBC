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

        /// <summary>
        /// Getting person with details by personId
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("Get/{personId}")]
        public async Task<ActionResult<GetPersonResponse>> GetPersonAsync(int personId)
        {
            var person = (await _personsRepo.GetAsync(personId)).ToGetPersonResponse();

            if (person.ProfilePictureRelativePath is { } path)
                person.ProfilePicture = await System.IO.File.ReadAllBytesAsync(Path.Combine(_profilePicturesFolderPath, path));

            return Ok(person);
        }

        /// <summary>
        /// Getting list of persons with filters and paging
        /// </summary>
        /// <param name="firstName">Person with firstName substring in FirstName</param>
        /// <param name="lastName">Person with lastName substring in LastName</param>
        /// <param name="privateNumber">Person with privateNumber substring in PrivateNumber</param>
        /// <param name="itemsPerPage">Parameter for paging</param>
        /// <param name="currentPageNumber">Parameter for paging</param>
        /// <returns></returns>
        [HttpGet("List")]
        public async Task<ActionResult<List<GeneralPersonResponse>>> ListPersonsAsync(string? firstName, string? lastName,
            string? privateNumber, int? itemsPerPage, int? currentPageNumber)
            => Ok((await _personsRepo.ListAsync(firstName, lastName, privateNumber,
                itemsPerPage, currentPageNumber)).ToGeneralPersonResponses());

        /// <summary>
        /// Person's ListAsync with many filters
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="privateNumber"></param>
        /// <param name="id"></param>
        /// <param name="gender"></param>
        /// <param name="birthDate"></param>
        /// <param name="cityId"></param>
        /// <param name="cityName"></param>
        /// <param name="profilePictureRelativePath"></param>
        /// <param name="itemsPerPage">Parameter for paging</param>
        /// <param name="currentPageNumber">Parameter for paging</param>
        /// <returns></returns>
        [HttpGet("ListWithManyFilters")]
        public async Task<ActionResult<List<GeneralPersonResponse>>> ListPersonsAsync(string? firstName, string? lastName, string? privateNumber,
            int? id, Gender? gender, DateTime? birthDate, int? cityId, string? cityName,
            string? profilePictureRelativePath,
            int? itemsPerPage, int? currentPageNumber)
            => Ok((await _personsRepo.ListAsync(firstName, lastName, privateNumber,
                id, gender, birthDate, cityId, cityName, profilePictureRelativePath,
                itemsPerPage, currentPageNumber)).ToGeneralPersonResponses());

        /// <summary>
        /// Adding Person in Database
        /// </summary>
        /// <param name="addPersonRequest"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> AddPersonAsync(AddPersonRequest addPersonRequest)
        {
            await _personsRepo.AddAsync(addPersonRequest.ToPerson());
            return Ok();
        }

        /// <summary>
        /// DeleteAsync Person from database with personId
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpPost("Delete/{personId}")]
        public async Task<IActionResult> DeletePersonAsync(int personId)
        {
            await _personsRepo.DeleteAsync(personId);
            return Ok();
        }

        /// <summary>
        /// EditAsync Person with personId
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="editPersonRequest">If Parameter values are null Person's parameter will not be updated</param>
        /// <returns></returns>
        [HttpPost("Edit/{personId}")]
        public async Task<IActionResult> EditPersonAsync(int personId, EditPersonRequest editPersonRequest)
        {
            await _personsRepo.EditAsync(personId, editPersonRequest.FirstName, editPersonRequest.LastName,
                editPersonRequest.Gender, editPersonRequest.PrivateNumber, editPersonRequest.BirthDate,
                editPersonRequest.CityId, editPersonRequest.PhoneNumbers?.ToPhoneNumbers().ToList());
            return Ok();
        }

        /// <summary>
        /// Uploading profilePicture for person (it will be saved in directory which is declared in appSettings)
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost("Upload/ProfilePicture/{personId}")]
        public async Task<ActionResult> EditProfilePictureAsync(int personId, IFormFile image)
        {
            if (!Directory.Exists(_profilePicturesFolderPath))
                throw new DirectoryNotFoundException(string.Format(Translations.ErrorDirectoryNotFound,
                    _profilePicturesFolderPath));
            if (!_profilePictureAllowedContentTypes.Contains(image.ContentType))
                throw new ArgumentException(Translations.ErrorFileContentTypeNotValid, nameof(image));

            string filePath = Path.Combine(_profilePicturesFolderPath, image.FileName);

            try
            {
                await using var stream = new MemoryStream();
                await image.CopyToAsync(stream);

                await System.IO.File.WriteAllBytesAsync(filePath, stream.ToArray());

                await _personsRepo.ChangeProfilePictureAsync(personId, image.FileName);
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
