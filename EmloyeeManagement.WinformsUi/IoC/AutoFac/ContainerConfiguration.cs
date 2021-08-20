using Autofac;
using EmloyeeManagement.Business;
using EmloyeeManagement.Business.ValidationRules.FluentValidation;
using EmloyeeManagement.Core.Rest.Abstract;
using EmloyeeManagement.Core.Rest.Concrete.RestSharp;
using EmloyeeManagement.Entities.Concrete;
using EmloyeeManagement.Entities.Helper;
using FluentValidation;
using MediatR.Extensions.Autofac.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmloyeeManagement.WinformsUi.IoC.AutoFac
{
    public class ContainerConfiguration
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ApiService<Employee, ApiResponse>>().As<IApiService<Employee, ApiResponse>>();
            builder.RegisterType<EmployeeValidator>().As<IValidator<Employee>>();
            builder.RegisterType<ListPage>();
            builder.RegisterType<EditEmployee>();
            builder.RegisterType<CreateEmployee>();

            builder.RegisterMediatR(typeof(MediatrEntryPoint).Assembly);

            return builder.Build();
        }
    }
}
