using DotNETStudy.DDD.MediatR.Messages;
using DotNETStudy.DDD.MediatR.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotNETStudy.DDD.MediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public ActionResult<bool> Save(PersonReqModel person)
        {
            Console.WriteLine($"[PersonsController]: {person}");

            _mediator.Publish(new PersonNotification(person.Name, person.Age));

            return true;
        }
    }
}
