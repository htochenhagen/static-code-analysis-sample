using Autofac;
using Caliburn.Micro;
using WpfCalculatorApp.ViewModels;

namespace WpfCalculatorApp.Bootstrapping.Modules
{
    internal class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // register default caliburn instances and startup view
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<CalculatorViewModel>().InstancePerLifetimeScope();
        }
    }
}