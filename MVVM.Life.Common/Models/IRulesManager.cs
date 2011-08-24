using System;
using System.Collections.Generic;
namespace MVVM.Life.Common.Models
{
    public interface IRulesManager
    {
        IEnumerable<IRule> Rules { get; }
        bool Evaluate(ICell cell, ICellContext context);
    }
}
