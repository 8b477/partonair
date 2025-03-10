using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using InfrastructureLayer.partonair.Persistence;

using Microsoft.EntityFrameworkCore;


namespace InfrastructureLayer.partonair.Repositories
{
    public class ContactRepository(AppDbContext ctx) : GenericRepository<Contact>(ctx), IContactRepository
    {
        public async Task<Contact> FindContactAsync(Guid senderId, Guid contactId)
        {
            var result = await _ctx.Contacts
                                     .Where(c => c.Id_Receiver == senderId && c.Id_Sender ==  contactId)
                                     .FirstOrDefaultAsync();

            return result ?? throw new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException,$"Identifier sender : {senderId} or identifier receiver : {contactId} - No match");
        }

        public async Task<Contact> GetByEmailAsync(string email)
        {
            var result = await _ctx.Contacts
                                   .Where(c => c.ContactEmail == email)
                                   .FirstOrDefaultAsync();

            return result ?? throw new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException,$"Identifier {email} - No match");
        }

        public async Task<ICollection<Contact>> GetByNameAsync(string name)
        {
            var result = await _ctx.Contacts
                                   .Where(c => c.ContactName == name)
                                   .ToListAsync();

            return 
                result.Count == 0
                ? throw new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException,$"identifier {name} - No match")
                : result;
        }

        public async Task<ICollection<Contact>> GetByPendingStatusAsync(StatusContact status)
        {
            var result = await _ctx.Contacts
                                   .Where(c => c.ContactStatus == status)
                                   .ToListAsync();

            return result ?? [];
        }

        public async Task<ICollection<Contact>> GetByAcceptedStatusAsync(StatusContact status)
        {
            var result = await _ctx.Contacts
                                   .Where(c => c.ContactStatus == status)
                                   .ToListAsync();

            return result ?? [];
        }

        public async Task<ICollection<Contact>> GetByBlockedStatusAsync(StatusContact status)
        {
            var result = await _ctx.Contacts
                                   .Where(c => c.ContactStatus == status)
                                   .ToListAsync();

            return result ?? [];
        }

        public async Task<bool> CheckIsContactExist(Guid idToCheck1, Guid idToCheck2)
        {
            var result1 = await _dbSet.Where(c => c.Id_Sender == idToCheck1 && c.Id_Receiver == idToCheck2).FirstOrDefaultAsync();
            var result2 = await _dbSet.Where(c => c.Id_Receiver == idToCheck1 && c.Id_Sender == idToCheck2).FirstOrDefaultAsync();

            if (result1 != null || result2 != null)
                return true;

            return false;
        }
    }
}
