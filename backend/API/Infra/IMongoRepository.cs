namespace API.Infra
{
    public interface IMongoRepository<T>
    {
        List<T> Get();
        T Get(string id);
        T GetBySlug(string slug);
        T Create(T news);
        void Update(string id, T news);
        void Remove(string id);
    }
}
