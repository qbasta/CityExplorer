using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CityExplorer.Models.Base;

public abstract class ModelBase : IModelBase
{
    [Key]
    public int Id { get; set; }

    public T Copy<T>()
        where T : class
    {
        var type = GetType();
        var obj = Activator.CreateInstance(type) as T
            ?? throw new NullReferenceException();

        foreach (var prop in type
            .GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var value = prop.GetValue(this);
            prop.SetValue(obj, value);
        }

        return obj;
    }
}
