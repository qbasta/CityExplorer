namespace CityExplorer.Models.Base
{
    public interface IModelBase
    {
        int Id { get; set; }

        T Copy<T>() where T : class;
    }
}