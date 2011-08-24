using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Unity;
using MVVM.Life.Common.Services;
using MVVM.Life.ViewModels;
using System.Windows.Markup;
using System.Globalization;
using MVVM.Life.Views;
using MVVM.Life.Business.Services;
using MVVM.Life.Common.Models;
using System.Windows.Media;
using Microsoft.Practices.Prism.Events;

namespace MVVM.Life
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Ensure the current culture passed into bindings is the OS culture. By default, WPF uses en-US 
            // as the culture, regardless of the system settings
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                                                               new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            IUnityContainer container = new UnityContainer();
            

            //Models
            container.RegisterType<ISettings, Settings>(new ContainerControlledLifetimeManager());

            //Services
            container.RegisterType<IDialogService, ModalDialogService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            container.RegisterType<RulesManager, BasicRulesManager>();
            container.RegisterType<HealthDisplayManager<Color>, BasicHealthDisplayManager>();

            //Views
            container.RegisterType<IMainWindowView, MainWindow>();
            container.RegisterType<ISettingsView, SettingsView>();

            //ViewModels
            container.RegisterType<IGameViewModel, GameViewModel>();
            container.RegisterType<ISettingsViewModel, SettingsViewModel>();



            //Entry into the Application
            container.Resolve<IGameViewModel>();
            
            //TestView view = new TestView();
            //view.Show();
        }
    }
}
