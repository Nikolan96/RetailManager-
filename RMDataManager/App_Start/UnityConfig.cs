using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Interfaces;
using RMDataManager.Library.Internal.DataAccess;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace RMDataManager
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterSingleton<ISqlDataAccess, SqlDataAccess>();
            container.RegisterType<IBillData, BillData>();
            container.RegisterType<IBillItemData, BillItemData>();
            container.RegisterType<IProductData, ProductData>();
            container.RegisterType<IUserData, UserData>();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}