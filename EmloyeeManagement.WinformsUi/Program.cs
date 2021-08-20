using Autofac;
using EmloyeeManagement.Business;
using EmloyeeManagement.Business.ValidationRules.FluentValidation;
using EmloyeeManagement.Core.Rest.Abstract;
using EmloyeeManagement.Core.Rest.Concrete.RestSharp;
using EmloyeeManagement.Entities.Concrete;
using EmloyeeManagement.Entities.Helper;
using EmloyeeManagement.WinformsUi.IoC.AutoFac;
using FluentValidation;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmloyeeManagement.WinformsUi
{
    static class Program
    {
        public static IContainer container;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            container = ContainerConfiguration.Configure();

            Application.Run(new ListPage(container.Resolve<IMediator>()));
        }
    }
}
