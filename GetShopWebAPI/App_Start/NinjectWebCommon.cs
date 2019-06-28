[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(GetShopWebAPI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(GetShopWebAPI.App_Start.NinjectWebCommon), "Stop")]

namespace GetShopWebAPI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using GetShopWebAPI.Repositories;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common.WebHost;
    using Ninject.Syntax;
    using System.Web.Http.Dependencies;
    using System.Collections.Generic;
    using Ninject.Parameters;
    using Ninject.Activation;
    using System.Web.Http;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                //kernel.Bind(x =>
                //{
                //    x.FromThisAssembly()
                //     .SelectAllClasses()
                //     .BindDefaultInterface();
                //});
                 return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IShopRepository>().To<ShopRepository>();
        }        
    }

public class NinjectDependencyScope : IDependencyScope
{
    IResolutionRoot resolver;

    public NinjectDependencyScope(IResolutionRoot resolver)
    {
        this.resolver = resolver;
    }

    public object GetService(Type serviceType)
    {
        if (resolver == null)
            throw new ObjectDisposedException("this", "This scope has been disposed");

        return resolver.TryGet(serviceType);
    }

    public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
    {
        if (resolver == null)
            throw new ObjectDisposedException("this", "This scope has been disposed");

        return resolver.GetAll(serviceType);
    }

    public void Dispose()
    {
        IDisposable disposable = resolver as IDisposable;
        if (disposable != null)
            disposable.Dispose();

        resolver = null;
    }
}

public class NinjectDependencyResolver : NinjectDependencyScope, System.Web.Http.Dependencies.IDependencyResolver
{
    IKernel kernel;
    public NinjectDependencyResolver(IKernel kernel)
        : base(kernel)
    {
        this.kernel = kernel;
    }
    public IDependencyScope BeginScope()
    {
        return new NinjectDependencyScope(kernel.BeginBlock());
    }
} 
}
