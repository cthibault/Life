using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using MVVM.Life.Common.Infrastructure;

namespace MVVM.Life.Common.Models
{
    public class HealthDisplay<T> : ObservableObject, IHealthDisplay<T>
    {
        #region Members
        private decimal _value = 0;
        private T _result;
        #endregion Members

        #region Properties
        public decimal Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaisePropertyChanged(() => Value);
                }
            }
        }
        public T Result
        {
            get { return _result; }
            set
            {
                if (!_result.Equals(value))
                {
                    _result = value;
                    RaisePropertyChanged(() => Result);
                }
            }
        }
        #endregion Properties

        #region Constructor
        public HealthDisplay(decimal value, T result)
        {
            _value = value;
            _result = result;
        }
        #endregion Constructor
    }
}
