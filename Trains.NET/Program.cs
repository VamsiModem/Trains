using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Trains.NET.Engine;
using Trains.NET.Rendering;

namespace Trains.NET
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var col = new ServiceCollection();
            foreach (Assembly a in GetAssemblies())
            {
                foreach (Type t in a.GetTypes())
                {
                    foreach (Type i in t.GetInterfaces())
                    {
                        if (i.Namespace?.StartsWith("Trains.NET", StringComparison.OrdinalIgnoreCase) == true)
                        {
                            col.AddSingleton(i, t);
                        }
                    }
                }
            }
            var serviceProvider = col.BuildServiceProvider();
            using Form form = new MainForm(serviceProvider.GetService<IGame>());
            Application.Run(form);
            static IEnumerable<Assembly> GetAssemblies()
            {
                yield return typeof(IGameBoard).Assembly;
                yield return typeof(IGame).Assembly;
            }
        }
    }
}
