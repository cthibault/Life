using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVM.Life.Common.Models
{
    public class HealthPoint : IHealthPoint
    {
        #region Members
        private readonly decimal _value = 0;
        private readonly bool _inclusive = true;
        #endregion Members

        #region Properties
        public decimal Value
        {
            get { return _value; }
        }
        public bool Inclusive
        {
            get { return _inclusive; }
        }
        #endregion Properties

        #region Constructors
        public HealthPoint(decimal value, bool inclusive = false)
        {
            _value = value;
            _inclusive = inclusive;
        }
        #endregion Constructors
    }
}
