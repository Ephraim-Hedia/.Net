using DataAccessLayer.Entites.Identity;
using Store.Repositories.Specification;
using System.Linq.Expressions;

namespace RepositoriesLayer.Specification.ContactSpecifications
{
    public class ContactWithSpecification : BaseSpecification<Contact>
    {
        public ContactWithSpecification(ContactSpecification specs) 
            : base(c=>
             c.OwnerId == specs.OwnerId &&
            (!specs.ContactId.HasValue || c.Id == specs.ContactId.Value) &&
            (string.IsNullOrEmpty(specs.Search) ||
                (!string.IsNullOrEmpty(c.FirstName) && c.FirstName.ToLower().Contains(specs.Search.ToLower())) ||
                (!string.IsNullOrEmpty(c.LastName) && c.LastName.ToLower().Contains(specs.Search.ToLower())) ||
                (!string.IsNullOrEmpty(c.Email) && c.Email.ToLower().Contains(specs.Search.ToLower())) ||
                (!string.IsNullOrEmpty(c.PhoneNumber) && c.PhoneNumber.ToLower().Contains(specs.Search.ToLower()))))

        {

            ApplyPagination((specs.PageIndex - 1) * specs.PageSize, specs.PageSize);

            if (!string.IsNullOrEmpty(specs.Sort))
            {
                switch (specs.Sort)
                {
                    case "NameAsc":
                        AddOrderBy(contact => contact.FirstName);
                        break;
                    case "NameDsc":
                        AddOrderByDescending(contact => contact.FirstName);
                        break;
                    default:
                        AddOrderBy(contact => contact.CreatedAt);
                        break;
                }
            }
            else
            {
                // Default sorting if none provided
                AddOrderBy(contact => contact.CreatedAt);
            }
        }
        public ContactWithSpecification( string ownerId , Guid contactId)
            : base(c => c.OwnerId == ownerId && c.Id == contactId)
        { 
        }
        public ContactWithSpecification(string ownerId)
            : base(c => c.OwnerId == ownerId)
        {

        }
    }
}
