using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using MVVM.Life.Common.Infrastructure;
using MVVM.Life.Common.Models;

namespace MVVM.Life.Common.Models
{
    public class Settings : ObservableObject, ISettings
    {
        #region Members
        private int _size = 20;
        private int _simulationSpeed = 250;
        
        private bool _wrapBoundaries = false;
        private RulesManager _rulesManager;
        private HealthDisplayManager<Color> _healthDisplayManager;

        private Color _aliveColor = Colors.Gray;
        private Color _deadColor = Colors.LightGray;
        #endregion Members

        #region Properties
        public int Size
        {
            get { return _size; }
            set
            {
                if (_size != value)
                {
                    _size = value;
                    RaisePropertyChanged(() => Size);
                }
            }
        }
        public int SimulationSpeed
        {
            get { return _simulationSpeed; }
            set
            {
                if (_simulationSpeed != value)
                {
                    _simulationSpeed = value;
                    RaisePropertyChanged(() => SimulationSpeed);
                }
            }
        }

        public bool WrapBoundaries
        {
            get { return _wrapBoundaries; }
            set
            {
                if (_wrapBoundaries != value)
                {
                    _wrapBoundaries = value;
                    RaisePropertyChanged(() => WrapBoundaries);
                }
            }
        }
        public RulesManager RulesManager
        {
            get { return _rulesManager; }
            set { _rulesManager = value; }
        }
        public HealthDisplayManager<Color> HealthDisplayManager
        {
            get { return _healthDisplayManager; }
            set { _healthDisplayManager = value; }
        }

        public Color AliveColor
        {
            get { return _aliveColor; }
            set
            {
                if (_aliveColor != value)
                {
                    _aliveColor = value;
                    RaisePropertyChanged(() => AliveColor);
                }
            }
        }
        public Color DeadColor
        {
            get { return _deadColor; }
            set
            {
                if (_deadColor != value)
                {
                    _deadColor = value;
                    RaisePropertyChanged(() => DeadColor);
                }
            }
        }
        #endregion Properties

        #region Constructors
        public Settings(RulesManager rulesManager, HealthDisplayManager<Color> healthDisplayManager)
        {
            _rulesManager = rulesManager;
            _healthDisplayManager = healthDisplayManager;
        }
        #endregion Constructors
    }
}
