using Autofac;
using Caliburn.Micro;
using WpfCalculatorApp.Core;
using WpfCalculatorApp.Helpers;
using WpfCalculatorApp.Interfaces;
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
            builder.RegisterType<CalculatorHelper>().As<ICalculatorHelper>().SingleInstance();
            builder.RegisterType<CalculatorContext>().As<ICalculationContext>();
            builder.RegisterType<Calculator>().As<ICalculator>();
            builder.RegisterType<CalculatorViewModel>().InstancePerLifetimeScope();
        }
    }
}