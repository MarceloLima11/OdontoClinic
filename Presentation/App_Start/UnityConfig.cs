using Unity;
using Unity.Mvc5;
using System.Web.Mvc;
using Core.Interfaces;
using Infrastructure.Cache;
using Application.Services;
using Application.Interfaces;
using Infrastructure.Data.Repositories;

namespace Presentation
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IClienteService, ClienteService>();
            container.RegisterType<ICacheService, RedisCacheService>();
            container.RegisterType<IClienteRepository, ClienteRepository>();
                        
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}