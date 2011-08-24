using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Life.Common.Models
{
    public class Rule : IRule
    {
        #region Members
        private readonly string _key = string.Empty;
        private readonly Func<ICell, ICellContext, bool> _condition;
        private readonly Func<decimal, decimal> _result;
        #endregion Members

        #region Properties
        public string Key
        {
            get { return _key; }
        }
        public Func<ICell, ICellContext, bool> Condition
        {
            get { return _condition; }
        }
        public Func<decimal, decimal> Result
        {
            get { return _result; }
        }
        #endregion Properties

        #region Constructor
        public Rule(string key, Func<ICell, ICellContext, bool> condition, Func<decimal, decimal> result)
        {
            _key = key;
            _condition = condition;
            _result = result;
        }
        #endregion Constructor

        #region Methods
        public bool Evaluate(ICell cell, ICellContext context)
        {
            bool condition = Condition(cell, context);
            if (condition)
                context.FutureHealth = Result(cell.Health);

            return condition;
        }
        #endregion Methods
    }
}
