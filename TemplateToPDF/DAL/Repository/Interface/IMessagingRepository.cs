using TemplateToPDF.DAL.Entities;

namespace TemplateToPDF.DAL.Repository.Interface
{
    public interface IMessagingRepository
    {
        public Task AddAsync(MessagingEntity entity);
        Task<List<MessagingEntity>> GetAllAsync();
        Task UpdateAllAsync(List<MessagingEntity> messagingEntities);
    }
}
