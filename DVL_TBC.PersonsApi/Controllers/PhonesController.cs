using System.Threading.Tasks;
using DVL_TBC.Domain.Abstract;
using DVL_TBC.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DVL_TBC.PersonsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhonesController : Controller
    {
        private readonly IPhonesRepository _phonesRepo;

        public PhonesController(IPhonesRepository phonesRepo)
        {
            _phonesRepo = phonesRepo;
        }

        /// <summary>
        /// Adding phoneNumber to Person in database
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> AddPhoneNumberAsync(PhoneNumber phoneNumber)
        {
            await _phonesRepo.AddAsync(phoneNumber);
            return Ok();
        }

        /// <summary>
        /// Removing phoneNumber from database
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<IActionResult> DeletePhoneNumberAsync(int personId, string number)
        {
            await _phonesRepo.DeleteAsync(personId, number);
            return Ok();
        }
    }
}
