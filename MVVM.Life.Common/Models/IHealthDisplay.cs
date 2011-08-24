
namespace MVVM.Life.Common.Models
{
    public interface IHealthDisplay<T>
    {
        decimal Value { get; set; }
        T Result { get; set; }
    }
}
