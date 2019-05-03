﻿using Caliburn.Micro;
using RMDesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMDesktopUI
{
    // Sets up caliburn micro

    public class Bootstrapper : BootstrapperBase
    {

        private SimpleContainer _container = new SimpleContainer();


        public Bootstrapper()
        {
            Initialize();
        }


        // container holds an instance of itself to pass if someone asks for simple container
        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>() // Handles bringing windows in and out
                .Singleton<IEventAggregator, EventAggregator>(); // Here we pass event messaging throughout application, handles events.

            // Use reflection get type for our current instance, get class types, where name of the class ends with ViewModel and add to list, run through list.
            // Registers the classes so that a new instance is created on request
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList().ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        // Opens ShellViewModel on start of application
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

    }
}
