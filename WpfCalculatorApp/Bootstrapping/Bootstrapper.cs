using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using Autofac;
using Autofac.Core;
using Caliburn.Micro;
using WpfCalculatorApp.Bootstrapping.Modules;
using WpfCalculatorApp.Extensions;
using WpfCalculatorApp.ViewModels;

namespace WpfCalculatorApp.Bootstrapping
{
    public class Bootstrapper : BootstrapperBase
    {
        private IContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();

            ConfigureModules(builder);

            _container = SafeBuild(builder);
        }

        private static void ConfigureModules(ContainerBuilder builder) //, IConfigService configService)
        {
            Trace.TraceInformation("Registering modules...");
            try
            {
                var modules = new List<IModule>
                {
                    new ApplicationModule()
                };

                modules.ForEach(module => builder.RegisterModule(module));
            }
            catch (Exception ex)
            {
                Trace.TraceWarning(ex.GetErrorMessage("Could not register modules"));
            }
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<CalculatorViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            if (string.IsNullOrWhiteSpace(key))
            {
                if (_container.TryResolve(service, out var result))
                    return result;
            }
            else
            {
                if (_container.TryResolveNamed(key, service, out var result))
                    return result;
            }

            throw new DependencyResolutionException(
                $"Could not locate any instances of contract {key ?? service.Name}.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        private static IContainer SafeBuild(ContainerBuilder builder)
        {
            try
            {
                return builder.Build();
            }
            catch (ReflectionTypeLoadException ex)
            {
                throw new AggregateException("Could not build IoC container", ex.LoaderExceptions);
            }
        }
    }
}
