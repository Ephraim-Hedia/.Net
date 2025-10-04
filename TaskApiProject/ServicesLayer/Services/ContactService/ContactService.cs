using AutoMapper;
using DataAccessLayer.Entites.Identity;
using Microsoft.Extensions.Logging;
using RepositoriesLayer.Interfaces;
using RepositoriesLayer.Specification.ContactSpecifications;
using ServicesLayer.HandleResponses.CommanResponse;
using ServicesLayer.Helper;
using ServicesLayer.Services.ContactService.Dtos;

namespace ServicesLayer.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ContactService> _logger;
        public ContactService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<ContactService> logger
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;    
            _logger = logger;
        }
        public async Task<CommonResponse<ContactResponseDto>> AddContactAsync(string ownerId , ContactCreateDto dto)
        {
            var response = new CommonResponse<ContactResponseDto>();
            
            if (dto == null)
            {
                response.IsSuccess = false;
                response.Errors = new Error {Code = "400", Message = "Input data is null" };
                return response;
            }

            var contact = _mapper.Map<Contact>(dto);
            contact.Id = Guid.NewGuid();
            contact.OwnerId = ownerId;
            try
            {
                await _unitOfWork.Repository<Contact, Guid>().AddAsync(contact);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
                throw; // rethrow the exception to be handled by a higher-level middleware
            }
            
            var mappedContact = _mapper.Map<ContactResponseDto>(contact);

            response.IsSuccess = true;
            response.Data = mappedContact;
            return response;

        }

        public async Task<CommonResponse<object>> DeleteContactAsync(string ownerId , Guid contactId)
        {
            var response = new CommonResponse<object>();
            if (contactId == null )
            {
                response.IsSuccess = false;
                response.Errors = new Error {Code = "400", Message = "Invalid Contact Id" };
            }

            try
            {
                var specs = new ContactWithSpecification(ownerId , contactId);
                var contact = await _unitOfWork.Repository<Contact, Guid>().GetByIdWithSpecificationAsync(specs);
                if (contact == null)
                {
                    response.IsSuccess = false;
                    response.Errors = new Error { Code = "404", Message = "Contact Not Found" };
                    return response;
                }
                _unitOfWork.Repository<Contact, Guid>().Delete(contact);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
                throw; // rethrow the exception to be handled by a higher-level middleware
            }
            response.IsSuccess = true;
            return response;
        }

        public async Task<CommonResponse <ContactResponseDto>> GetContactByIdAsync(ContactSpecification input)
        {
            var response = new CommonResponse<ContactResponseDto>();
            Contact contact ;
            if (input.ContactId == null )
            {
                response.IsSuccess = false;
                response.Errors = new Error { Code = "400", Message = "Invalid Contact Id" };
                return response;
            }
            var specs = new ContactWithSpecification(input.OwnerId , input.ContactId.Value);
            try
            {
                contact = await _unitOfWork.Repository<Contact, Guid>().GetByIdWithSpecificationAsync(specs);
                if(contact == null)
                {
                    response.IsSuccess = false;
                    response.Errors = new Error { Code = "404", Message = "Contact Not Found" };
                    return response;
                }
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
                throw; // rethrow the exception to be handled by a higher-level middleware
            }
            var mappedContact = _mapper.Map<ContactResponseDto>(contact);
            response.IsSuccess = true;
            response.Data = mappedContact;
            return response;
        }

        public async Task<CommonResponse<PaginatedResultDto<ContactResponseDto>>> GetAllContactsAsync(ContactSpecification input)
        {
            var response = new CommonResponse<PaginatedResultDto<ContactResponseDto>>();
            IReadOnlyList<Contact> contacts = new List<Contact>();
            
            try
            {
                var specs = new ContactWithSpecification(input);
                contacts = await _unitOfWork.Repository<Contact, Guid>().GetAllWithSpecificationAsync(specs);
                if (contacts == null || contacts.Count == 0 )
                {
                    response.IsSuccess = false;
                    response.Errors = new Error { Code = "404", Message = "No Contacts Found" };
                    var paginatedData = new PaginatedResultDto<ContactResponseDto>(
                            input.PageIndex,
                            input.PageSize,
                            0,
                            null);
                    response.Data = paginatedData;
                    return response;
                }
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
                throw; // rethrow the exception to be handled by a higher-level middleware
            }
            
            var mappedContacts =  _mapper.Map<IReadOnlyList<ContactResponseDto>>(contacts);

            

            var specsWithoutPagination = new ContactWithSpecification(new ContactSpecification
            {
                OwnerId = input.OwnerId,
                Search = input.Search,
                Sort = input.Sort,
                PageSize = int.MaxValue,
            });
            var allProducts = await _unitOfWork.Repository<Contact, Guid>().GetAllWithSpecificationAsync(specsWithoutPagination);
            var count = allProducts.Count;

            var paginatedResult = new PaginatedResultDto<ContactResponseDto>(
                input.PageIndex,
                input.PageSize,
                count,
                mappedContacts);

            response.IsSuccess = true;
            response.Data = paginatedResult;
            return response;    
        }
    }
}
