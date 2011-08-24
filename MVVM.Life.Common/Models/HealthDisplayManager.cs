using System.Collections.Generic;
using System.Linq;
using MVVM.Life.Common.Models;
using MVVM.Life.Common.Infrastructure;
using System.Collections.ObjectModel;

namespace MVVM.Life.Common.Models
{
    public abstract class HealthDisplayManager<T> : ObservableObject
    {
        #region Members
        private ObservableCollection<IHealthDisplay<T>> _items = new ObservableCollection<IHealthDisplay<T>>();
        private T _default;
        #endregion Members

        #region Properties
        public ObservableCollection<IHealthDisplay<T>> Items
        {
            get { return new ObservableCollection<IHealthDisplay<T>>(_items.OrderByDescending(item => item.Value)); }
            protected set { _items = value; }
        }
        public T Default
        {
            get { return _default; }
            set
            {
                if (!_default.Equals(value))
                {
                    _default = value;
                    RaisePropertyChanged(() => Default);
                }
            }
        }
        #endregion Properties

        #region Methods
        public virtual T GetResult(decimal health)
        {
            if (Items.Any(item => health >= item.Value))
                return Items.FirstOrDefault(item => health >= item.Value).Result;
            return Default;
        }
        #endregion Methods
    }
}
