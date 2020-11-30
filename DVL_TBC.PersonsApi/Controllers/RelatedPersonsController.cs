using System.Collections.Generic;
using System.Linq;
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

        [HttpPost("Add/{relatedPersonId}")]
        public IActionResult AddRelatedPerson(int relatedPersonId, int personId, PersonConnectionType connectionType)
        {
            _relatedPersonsRepo.Add(new RelatedPerson()
                { PersonId = personId, RelatedPersonId = relatedPersonId, ConnectionType = connectionType });
            return Ok();
        }

        [HttpPost("Delete/{relatedPersonId}")]
        public IActionResult DeleteRelatedPerson(int relatedPersonId, int personId)
        {
            _relatedPersonsRepo.Delete(personId, relatedPersonId);
            return Ok();
        }

        [HttpGet("Get/RelatedPersonsReport")]
        public ActionResult<List<RelatedPersonsCountResponse>> GetRelatedPersonsCount()
        {
            return Ok(_relatedPersonsRepo.GetRelatedPersonsCount().Select(rp =>
                new RelatedPersonsCountResponse(rp.privateNumber, rp.fullName, rp.connectionType,
                    rp.relatedPersonsCount)));
        }
    }
}
