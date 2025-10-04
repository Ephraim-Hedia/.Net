using DataAccessLayer.Entites.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoriesLayer.Specification.ContactSpecifications;
using ServicesLayer.Services.ContactService;
using ServicesLayer.Services.ContactService.Dtos;
using System.Security.Claims;



namespace TaskApiProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly UserManager<ApplicationUser> _userManager;
        public ContactController(
            IContactService contactService,
            UserManager<ApplicationUser> userManager)
        {
            _contactService = contactService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateContact(ContactCreateDto input)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _contactService.AddContactAsync(user.Id, input);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllContact([FromQuery]ContactSpecification input)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            input.OwnerId = user.Id;

            var result = await _contactService.GetAllContactsAsync(input);
            return result.IsSuccess ? Ok(result)
                : NotFound(result);
        }

        [HttpGet]
        [Route("{contactId}")]
        public async Task<ActionResult<ContactResponseDto>> GetContact(Guid contactId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            var input = new ContactSpecification()
            {
                OwnerId = user.Id,
                ContactId = contactId
            };
            var result = await _contactService.GetContactByIdAsync(input);
            return result.IsSuccess ? Ok(result) : result.Errors.Code == "400" ? 
                 BadRequest(result) : NotFound(result);
        }
          
        [HttpDelete]
        [Route("{contactId}")]
        public async Task<ActionResult<bool>> DeleteContact(Guid contactId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);


            var result = await _contactService.DeleteContactAsync(user.Id , contactId);

            return result.IsSuccess ? Ok(result) : result.Errors.Code == "400" ? 
                 BadRequest(result) : NotFound(result);
        }

    }
}
