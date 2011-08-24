using MVVM.Life.Common.Infrastructure;

namespace MVVM.Life.Common.Models
{
    public class Cell : ObservableObject, ICell
    {
        private decimal _health = 0M;
        public decimal Health
        {
            get { return _health; }
            set
            {
                if (_health != value)
                {
                    _health = value;
                    RaisePropertyChanged(() => Health);
                }
            }
        }
        
        public Cell(decimal health)
        {
            _health = health;
        }
    }
}
