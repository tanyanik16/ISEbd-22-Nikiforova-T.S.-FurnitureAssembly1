using System;
using FurnitureAssemblyBusinessLogic.BusinessLogics;
using FurnitureAssemblyContracts.BusinessLogicsContracts;
using FurnitureAssemblyContracts.StoragesContracts;
using FurnitureAssemblyFileImplement.Implements;
using FurnitureAssemblyFileImplement;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace FurnitureAssemblyView
{
    static class Program
    {
        private static IUnityContainer container = null;
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += ApplicationExit;
            AppDomain.CurrentDomain.UnhandledException += (o, e) => { if (e.IsTerminating) ApplicationExit(null, null); };
            Application.ThreadException += (o, e) => { Application.Exit(); };
            Application.Run(Container.Resolve<FormMain>());
        }
        private static void ApplicationExit(object sender, EventArgs e)
        {
            FileDataListSingleton.SaveAll();
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IComponentStorage,
            ComponentStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IFurnitureStorage, FurnitureStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IComponentLogic, ComponentLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IFurnitureLogic, FurnitureLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorehouseLogic, StorehouseLogic>(new
         HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorehouseStorage, StorehouseStorage>(new HierarchicalLifetimeManager());
            return currentContainer;
        }

    }
}
