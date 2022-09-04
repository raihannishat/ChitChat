namespace Infrastructure
{
    public interface IMongoDbSetting
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
