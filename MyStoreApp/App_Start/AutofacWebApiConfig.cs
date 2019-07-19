using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using MongoDB.Driver;
using MyStore.Database;
using MyStore.Mongo.Repository;
using MyStore.MongoDB;
using MyStore.Repository.Interface;
using MyStoreApp.Repository;
using System.Configuration;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;

namespace MyStoreApp.App_Start
{
    public class AutofacWebApiConfig
    {
        public static IContainer Container;
        public static IMongoDBSettings Settings;
        public static void Initialize(HttpConfiguration config)
        {
            var mongoDbHost = ConfigurationManager.AppSettings["MongoDBHost"];
            var mongoDbName = ConfigurationManager.AppSettings["MongoDBName"];
            if (!string.IsNullOrWhiteSpace(mongoDbHost)
                && !string.IsNullOrWhiteSpace(mongoDbName))
            {
                Settings = new MongoDBSettings
                {
                    MongoDBConnectionString = mongoDbHost,
                    MongoDBName = mongoDbName
                };
            }

            Initialize(config, RegisterServices(new ContainerBuilder()));

        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<MyStoreEntities>()
                   .As<DbContext>()
                   .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                   .As<IDbFactory>()
                   .InstancePerRequest();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappingProfile());
            });

            builder.RegisterInstance(config.CreateMapper())
                .As<IMapper>()
                .SingleInstance();

            builder.RegisterGeneric(typeof(GenericRepository<>))
                   .As(typeof(IGenericRepository<>))
                   .InstancePerRequest();

            builder.Register(ctx =>
            {
                return Settings;
            }).As<IMongoDBSettings>().InstancePerRequest();

            builder.RegisterType<MongoDBContext>()
                   .As<IMongoDBContext>()
                   .InstancePerRequest();

            builder.RegisterType<ProductMongoRepository>()
                   .As<IProductMongoRepository>()
                   .InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}
