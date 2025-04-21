using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using Infrastructure.Data.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Presentation
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IClienteService, ClienteService>();
            container.RegisterType<IClienteRepository, ClienteRepository>();
                        
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}