using System;
using MVVM.Life.Common.Models;
namespace MVVM.Life.ViewModels
{
    interface ICellViewModel
    {
        void Birth();
        //ICell Cell { get; }
        ICellContext Context { get; set; }
        void Kill();
        void ToggleLife();
    }
}
