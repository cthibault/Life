using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;

namespace MVVM.Life.Common.Models
{
    public abstract class RulesManager : IRulesManager
    {
        #region Members
        private IEnumerable<IRule> _rules = new List<IRule>();
        #endregion Members

        #region Properties
        public IEnumerable<IRule> Rules
        {
            get { return _rules; }
            protected set { _rules = value; }
        }
        #endregion Properties

        public virtual bool Evaluate(ICell cell, ICellContext context)
        {
            bool evaluated = false;
            Parallel.ForEach<IRule>(Rules, r => { evaluated |= r.Evaluate(cell, context); });

            return evaluated;
        }
    }
}
