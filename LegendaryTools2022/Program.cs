using LegendaryTools2022.Data;
using LegendaryTools2022.Models.Entities;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryTools2022
{
    static class Program
    {
        private static Container container;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap();
            Application.Run(container.GetInstance<Form1>());
        }

        private static void Bootstrap()
        {
            // Create the container as usual.
            container = new Container();

            // Register your types, for instance:
            container.Register<IRepositoryCard, RepositoryCard>(Lifestyle.Singleton);
            container.Register<IRepositoryCardType, RepositoryCardType>(Lifestyle.Singleton);
            container.Register<IRepositoryCustomSet, RepositoryCustomSet>(Lifestyle.Singleton);
            container.Register<IRepositoryDeck, RepositoryDeck>(Lifestyle.Singleton);
            container.Register<IRepositoryDeckType, RepositoryDeckType>(Lifestyle.Singleton);
            container.Register<IRepositoryCardTemplate, RepositoryCardTemplate>(Lifestyle.Singleton);
            //container.Register<IUserContext, WinFormsUserContext>();
            container.Register<Form1>();
            var registration = container.GetRegistration(typeof(Form1)).Registration;

            registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "whatever");

            // Optionally verify the container.
            container.Verify();
        }
    }
}
