using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DVL_TBC.Domain.Abstract;
using DVL_TBC.Domain.Models;
using DVL_TBC.PersonsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DVL_TBC.PersonsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RelatedPersonsController : Controller
    {
        private readonly IRelatedPersonsRepository _relatedPersonsRepo;

        public RelatedPersonsController(IRelatedPersonsRepository relatedPersonsRepo)
        {
            _relatedPersonsRepo = relatedPersonsRepo;
        }

        /// <summary>
        /// AddAsync related person in database
        /// </summary>
        /// <param name="relatedPersonId"></param>
        /// <param name="personId"></param>
        /// <param name="connectionType"></param>
        /// <returns></returns>
        [HttpPost("Add/{relatedPersonId}")]
        public async Task<IActionResult> AddRelatedPerson(int relatedPersonId, int personId, PersonConnectionType connectionType)
        {
            await _relatedPersonsRepo.AddAsync(new RelatedPerson()
                { PersonId = personId, RelatedPersonId = relatedPersonId, ConnectionType = connectionType });
            return Ok();
        }

        /// <summary>
        /// DeleteAsync related person from database
        /// </summary>
        /// <param name="relatedPersonId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpPost("Delete/{relatedPersonId}")]
        public async Task<IActionResult> DeleteRelatedPerson(int relatedPersonId, int personId)
        {
            await _relatedPersonsRepo.DeleteAsync(personId, relatedPersonId);
            return Ok();
        }

        /// <summary>
        /// GetAsync report about all persons RelatedPersons Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get/RelatedPersonsReport")]
        public async Task<ActionResult<List<RelatedPersonsCountResponse>>> GetRelatedPersonsCount() =>
            Ok((await _relatedPersonsRepo.GetRelatedPersonsCountAsync()).Select(rp =>
                new RelatedPersonsCountResponse(rp.privateNumber, rp.fullName, rp.connectionType,
                    rp.relatedPersonsCount)));
    }
}
