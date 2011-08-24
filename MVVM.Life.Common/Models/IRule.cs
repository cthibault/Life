using System;

namespace MVVM.Life.Common.Models
{
    public interface IRule
    {
        string Key { get; }
        Func<ICell, ICellContext, bool> Condition { get; }
        Func<decimal, decimal> Result { get; }
        bool Evaluate(ICell cell, ICellContext context);
    }
}
