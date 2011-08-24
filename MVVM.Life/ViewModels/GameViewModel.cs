using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using MVVM.Life.Common.Infrastructure;
using MVVM.Life.Common.Models;
using MVVM.Life.Common.Services;

namespace MVVM.Life.ViewModels
{
    public class GameViewModel : ObservableObject, IGameViewModel
    {
        #region Constants
        private const int SIZE = 30;
        #endregion Constants

        #region Members
        private readonly IUnityContainer _container;
        private readonly IDialogService _dialogService;

        private ObservableCollection<CellViewModel> _cells = new ObservableCollection<CellViewModel>();
        private ICell _activeCell;
        private ISettings _settings;
        private BackgroundWorker _worker;

        private int _generationCounter = 0;
        private bool _isRunning = false;
        #endregion Members

        #region Properties
        public ObservableCollection<CellViewModel> Cells
        {
            get { return _cells; }
            set
            {
                if (_cells != value)
                {
                    //TODO: Implement Order based on the Coordinate to guarantee the order is consistent throughout the simulation
                    _cells = value;
                    RaisePropertyChanged(() => Cells);
                }
            }
        }
        public ICell ActiveCell
        {
            get { return _activeCell; }
            set
            {
                if (_activeCell != value)
                {
                    _activeCell = value;
                    RaisePropertyChanged(() => ActiveCell);
                }
            }
        }
        public ISettings Settings
        {
            get { return _settings; }
        }

        public int GenerationCounter
        {
            get { return _generationCounter; }
            set
            {
                if (_generationCounter != value)
                {
                    _generationCounter = value;
                    RaisePropertyChanged(() => GenerationCounter);
                }
            }
        }
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    RaisePropertyChanged(() => IsRunning);
                }
            }
        }

        public int PopulationAlive
        {
            get { return _cells.Count(vm => vm.Health > 0); }
        }
        public int PopulationDead
        {
            get { return _cells.Count() - PopulationAlive; }
        }
        public decimal AlivePercentage
        {
            get
            {
                int count = _cells.Count();
                return (count > 0) ? (decimal)PopulationAlive / _cells.Count() : 0M;
            }
        }
        
        private BackgroundWorker Worker
        {
            get
            {
                if (_worker == null)
                    _worker = _createWorker();
                return _worker;
            }
        }
        #endregion Properties

        #region Actions
        public ICommand ClearCommand
        {
            get { return new RelayCommand(Clear, _canClear); }
        }
        public ICommand RandomizeCommand
        {
            get { return new RelayCommand(RandomizeBoard, _canRandomize); }
        }
        public ICommand SettingsCommand
        {
            get { return new RelayCommand(OpenSettings, _canSettings); }
        }
        public ICommand StartCommand
        {
            get { return new RelayCommand(Start, _canStart); }
        }
        public ICommand StepCommand
        {
            get { return new RelayCommand(Step, _canStep); }
        }
        public ICommand StopCommand
        {
            get { return new RelayCommand(Stop, _canStop); }
        }
        
        //AVAILABILITY
        private bool _canClear()
        {
            return !IsRunning;
        }
        private bool _canRandomize()
        {
            return !IsRunning;
        }
        private bool _canSettings()
        {
            return !IsRunning;
        }
        private bool _canStart()
        {
            return !IsRunning;
        }
        private bool _canStep()
        {
            return !IsRunning;
        }
        private bool _canStop()
        {
            return IsRunning;
        }

        //IMPLEMENTATION
        private void Clear()
        {
            foreach (var cell in Cells)
                cell.Kill();
            GenerationCounter = 0;

            _updateStatistics();
        }
        private void OpenSettings()
        {
            var viewModel = _container.Resolve<ISettingsViewModel>();
        }
        private void RandomizeBoard()
        {
            Random randomGenerator = new Random(DateTime.Now.Millisecond);

            Cells.Clear();
            GenerationCounter = 0;

            foreach (int x in Enumerable.Range(0, Settings.Size))
                foreach (int y in Enumerable.Range(0, Settings.Size))
                    Cells.Add(new CellViewModel(new Cell((randomGenerator.Next(0, 2) > 0) ? 100M : 0M)) 
                    { 
                        Context = new CellContext(new Coordinate(x, y)),
                        Settings = Settings                    
                    });

            Parallel.ForEach<CellViewModel>(Cells, vm => _assignNeighbors(vm));

            _updateStatistics();
        }
        private void Start()
        {
            if (!Worker.IsBusy)
                Worker.RunWorkerAsync(Settings);

            IsRunning = true;
        }
        private void Step()
        {
            try
            {
                Parallel.ForEach<CellViewModel>(Cells, vm => Settings.RulesManager.Evaluate(vm.Cell, vm.Context));
                Parallel.ForEach<CellViewModel>(Cells, vm =>
                {
                    vm.Health = vm.Context.FutureHealth;
                    vm.Context.FutureHealth = 0M;
                });

                GenerationCounter++;
                _updateStatistics();
            }
            catch (Exception ex)
            {
                _dialogService.ShowException(ex.Message);
            }
        }
        private void Stop()
        {
            if (Worker.IsBusy)
                Worker.CancelAsync();

            IsRunning = false;
        }
        #endregion Actions

        #region Constructors
        public GameViewModel(IUnityContainer container, IDialogService dialogService)
        {
            _container = container;
            _dialogService = dialogService;

            _setupGame();
            _activateView();
        }
        #endregion Constructors

        #region Methods
        private void _setupGame()
        {
            _settings = _container.Resolve<ISettings>();
            _settings.Size = SIZE;
            _settings.SimulationSpeed = 250;

            RandomizeBoard();
        }
        private void _activateView()
        {
            var view = _container.Resolve<IMainWindowView>();
            view.DataContext = this;
            view.Show();
        }
        private BackgroundWorker _createWorker()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = false;

            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Settings settings = e.Argument as Settings;
                if (settings != null)
                {
                    while (!worker.CancellationPending)
                    {
                        Step();
                        System.Threading.Thread.Sleep(settings.SimulationSpeed);
                    }
                }
            };
            worker.DoWork += delegate
            {
                IsRunning = false;
            };

            return worker;
        }
        private void _assignNeighbors(CellViewModel cellVM)
        {
            int xPlusOne = cellVM.Context.Coordinate.X + 1;
            int yPlusOne = cellVM.Context.Coordinate.Y + 1;
            int xMinusOne = cellVM.Context.Coordinate.X - 1;
            int yMinusOne = cellVM.Context.Coordinate.Y - 1;

            if (Settings.WrapBoundaries)
            {
                xPlusOne = (xPlusOne >= Settings.Size) ? 0 : xPlusOne;
                yPlusOne = (yPlusOne >= Settings.Size) ? 0 : yPlusOne;
                xMinusOne = (xMinusOne < 0) ? Settings.Size - 1 : xMinusOne;
                yMinusOne = (yMinusOne < 0) ? Settings.Size - 1 : yMinusOne;
            }

            List<Coordinate> neighboringCoordinates = new List<Coordinate>()
            {
                new Coordinate(xMinusOne, yMinusOne),
                new Coordinate(xMinusOne, cellVM.Context.Coordinate.Y),
                new Coordinate(xMinusOne, yPlusOne),
                new Coordinate(cellVM.Context.Coordinate.X, yMinusOne),
                new Coordinate(cellVM.Context.Coordinate.X, yPlusOne),
                new Coordinate(xPlusOne, yMinusOne),
                new Coordinate(xPlusOne, cellVM.Context.Coordinate.Y),
                new Coordinate(xPlusOne, yPlusOne)
            };

            cellVM.Context.Neighbors = Cells.Where(vm => neighboringCoordinates.Contains(vm.Context.Coordinate)).Select(vm => vm.Cell).ToList();
        }
        private void _updateStatistics()
        {
            RaisePropertyChanged(() => PopulationAlive);
            RaisePropertyChanged(() => PopulationDead);
            RaisePropertyChanged(() => AlivePercentage);
            RaisePropertyChanged(() => GenerationCounter);
        }
        #endregion Methods
    }
}
