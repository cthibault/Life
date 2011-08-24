using MVVM.Life.Common.Infrastructure;
using MVVM.Life.Common.Models;
using System.Collections.Generic;

namespace MVVM.Life.Common.Models
{
    public class CellContext : ObservableObject, ICellContext
    {
        private readonly ICoordinate _coordinate;
        private List<ICell> _neighbors;
        private decimal _futureHealth = 0;

        public ICoordinate Coordinate
        {
            get { return _coordinate; }
        }
        public List<ICell> Neighbors
        {
            get { return _neighbors; }
            set { _neighbors = value; }
        }
        public decimal FutureHealth
        {
            get { return _futureHealth; }
            set { _futureHealth = value; }
        }

        public CellContext(ICoordinate coordinate)
        {
            _coordinate = coordinate;
        }
    }
}