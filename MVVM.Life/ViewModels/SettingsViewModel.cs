using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVM.Life.Common.Infrastructure;
using MVVM.Life.Common.Models;
using Microsoft.Practices.Unity;
using System.Windows.Media;
using System.Windows.Input;
using MVVM.Life.Views;

namespace MVVM.Life.ViewModels
{
    public class SettingsViewModel : ObservableObject, ISettingsViewModel
    {
        #region Members
        private IUnityContainer _container;
        private ISettings _settings;

        private List<Color> _colors = new List<Color>() { Colors.Black, Colors.Blue, Colors.Red, Colors.White, Colors.Yellow };
        private Color _aliveColor = ((SolidColorBrush)App.Current.Resources["AliveBrush"]).Color;
        private Color _deadColor = ((SolidColorBrush)App.Current.Resources["DeadBrush"]).Color;
        #endregion Members

        #region Properties
        public ISettings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        public IEnumerable<Color> ColorOptions
        {
            get { return _colors; }
        }
        public Color AliveColor
        {
            get { return _aliveColor; }
            set
            {
                if (_aliveColor != value)
                {
                    _aliveColor = value;
                    App.Current.Resources["AliveBrush"] = new SolidColorBrush(value);
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
                    App.Current.Resources["DeadBrush"] = new SolidColorBrush(value);
                    RaisePropertyChanged(() => DeadColor);
                }
            }
        }
        #endregion Properties

        #region Constructors
        public SettingsViewModel(IUnityContainer container, ISettings settings)
        {
            _container = container;
            Settings = settings;

            _activateView();
        }
        #endregion Constructors

        #region Methods
        private void _activateView()
        {
            var view = _container.Resolve<ISettingsView>();
            view.DataContext = this;
            view.Show();
        }
        #endregion Methods
    }
}
