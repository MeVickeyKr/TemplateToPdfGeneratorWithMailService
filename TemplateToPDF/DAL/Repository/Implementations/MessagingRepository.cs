using Microsoft.EntityFrameworkCore;
using TemplateToPDF.DAL.DatabaseContext;
using TemplateToPDF.DAL.Entities;
using TemplateToPDF.DAL.Repository.Interface;

namespace TemplateToPDF.DAL.Repository.Implementations
{
    public class MessagingRepository :IMessagingRepository
    {
        private readonly PolicyDocumentDbContext _policyDocumentDbContext;
        public MessagingRepository(PolicyDocumentDbContext policyDocumentDbContext)
        {
            _policyDocumentDbContext = policyDocumentDbContext;
        }
        public async Task AddAsync(MessagingEntity entity)
        {
            await _policyDocumentDbContext.AddAsync(entity);
            await _policyDocumentDbContext.SaveChangesAsync();
        }
        public async Task<List<MessagingEntity>> GetAllAsync()
        {
            var entity = await _policyDocumentDbContext.MessagingTable.Where(p => p.isSent == false && p.isDeleted == false && p.Attempt < p.MaxAttempt).ToListAsync();
            return entity;
        }
        public async Task UpdateAllAsync(List<MessagingEntity> messagingEntities)
        {
            //messagingEntities.Select( p => _policyDocumentDbContext.MessagingTable.Update(p));
            //await _policyDocumentDbContext.SaveChangesAsync();
            //

            foreach (var messagingEntity in messagingEntities)
            {
                _policyDocumentDbContext.Entry(messagingEntity).State = EntityState.Modified;
            }
            await _policyDocumentDbContext.SaveChangesAsync();  
            
        }
    }
}
