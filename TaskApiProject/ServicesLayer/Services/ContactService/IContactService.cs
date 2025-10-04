using RepositoriesLayer.Specification.ContactSpecifications;
using ServicesLayer.HandleResponses.CommanResponse;
using ServicesLayer.Helper;
using ServicesLayer.Services.ContactService.Dtos;

namespace ServicesLayer.Services.ContactService
{
    public interface IContactService
    {
        public Task<CommonResponse<ContactResponseDto>> AddContactAsync(string ownerId ,ContactCreateDto dto);
        Task<CommonResponse<PaginatedResultDto<ContactResponseDto>>> GetAllContactsAsync(ContactSpecification input);
        Task<CommonResponse<ContactResponseDto>> GetContactByIdAsync(ContactSpecification input);
        Task<CommonResponse<object>> DeleteContactAsync(string ownerId, Guid contactId);
    }
}
