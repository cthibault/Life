using System.Collections.Generic;

namespace MVVM.Life.Common.Models
{
    public interface ICellContext
    {
        ICoordinate Coordinate { get; }
        List<ICell> Neighbors { get; set; }
        decimal FutureHealth { get; set; }
    }
}
