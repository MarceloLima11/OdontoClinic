using NHibernate;
using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.Mappings;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Diagnostics;

namespace Infrastructure.NHibernate
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private const string _connectionString = "DefaultConnection";

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory is null)
                {
                    var connectionString = ConfigurationManager.ConnectionStrings[_connectionString].ConnectionString;

                    try
                    {
                        _sessionFactory = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2012
                        .ConnectionString(connectionString))
                        .Mappings(m => m.FluentMappings
                        .AddFromAssemblyOf<ClienteMap>())
                        //.ExposeConfiguration(config => new SchemaExport(config).Create(false, true)) //Caso seja a primeira vez rodando a aplicação
                        .ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true))
                        .BuildSessionFactory();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Erro ao configurar NHibernate: " + ex.Message);
                        Debug.WriteLine("InnerException: " + ex.InnerException?.Message);
                        Debug.WriteLine("StackTrace: " + ex.StackTrace);
                        throw;
                    }
                }

                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
