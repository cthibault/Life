using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVM.Life.Common.Infrastructure;
using MVVM.Life.Common.Models;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVM.Life.ViewModels
{
    public class CellViewModel : ObservableObject, ICellViewModel
    {
        #region Members
        private readonly ICell _cell;
        private ICellContext _context;
        private ISettings _settings;
        #endregion Members

        #region Properties
        public ICell Cell
        {
            get { return _cell; }
        }
        public decimal Health
        {
            get { return _cell.Health; }
            set
            {
                if (_cell.Health != value)
                {
                    _cell.Health = value;
                    RaisePropertyChanged(() => Health);
                    RaisePropertyChanged(() => DisplayBrush);
                }
            }
        }

        public ICellContext Context
        {
            get { return _context; }
            set { _context = value; }
        }
        public ISettings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        public SolidColorBrush DisplayBrush
        {
            get
            {
                return new SolidColorBrush(Settings.HealthDisplayManager.GetResult(Health));
            }
        }
        #endregion Properties

        #region Constructors
        public CellViewModel(ICell cell)
        {
            _cell = cell;
        }
        #endregion Constructors

        #region Methods
        public void ToggleLife()
        {
            if (Health > 0)
                Kill();
            else
                Birth();
        }
        public void Kill()
        {
            Health = 0M;
        }
        public void Birth()
        {
            Health = 100M;
        }
        #endregion Methods
    }
}
