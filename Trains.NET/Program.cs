using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
            using var from = CreateForm();
            Application.Run(from);
        }

        private static MainForm CreateForm()
        {
            var gameBoard = new Engine.GameBoard();
            return new MainForm(new Game(
                    gameBoard,
                    new List<IBoardRenderer>
                    {
                        new GridRenderer(),
                        new TrackLayoutRenderer(gameBoard, new TrackRenderer())
                    })
                ); ;
        }
    }
}
