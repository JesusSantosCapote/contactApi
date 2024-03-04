using BusinessLogic.DTO;
using BusinessLogic.Result;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Presentation.Authentication;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    [Route("api/contact")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IUserContext _userContext;

        public ContactController(ILogger<ContactController> logger, IContactService contactService, IUserContext userContext)
        {
            _contactService = contactService;
            _userContext = userContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(ContactDto newContact)
        {
            var result = await _contactService.AddContactAsync(newContact, _userContext.UserName);

            if (result.ResultType == ResultType.Invalid || result.ResultType == ResultType.Unexpected)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(GetContact), new { id = result.Data.Id }, result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var username = _userContext.UserName;
            var result = await _contactService.GetAllContactsAsync(username);
            return this.FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            var result = await _contactService.GetContactAsync(id, _userContext.UserName);
            return this.FromResult(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Constants.CubanAdministratorPolicyName)]
        public async Task<IActionResult> RemoveContact(Guid id)
        {
            var result = await _contactService.DeleteAsync(id, _userContext.UserName);
            return this.FromResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(Guid id, ContactDto newContact)
        {
            var result = await _contactService.UpdateContactAsync(id, newContact, _userContext.UserName);
            return this.FromResult(result);
        }
    }
}
