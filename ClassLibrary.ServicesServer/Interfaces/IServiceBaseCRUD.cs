namespace ClassLibrary.ServicesServer.Interfaces
{
    public interface IServiceBaseCRUD<DTO, Entity>
    {
        public Task<IEnumerable<DTO>> GetAllAsync(CancellationToken cancellationToken);
        public Task<DTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<DTO> CreateAsync(Entity entity, CancellationToken cancellationToken);
        public Task<DTO?> UpdateAsync(int id, Entity entity, CancellationToken cancellationToken);
        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
