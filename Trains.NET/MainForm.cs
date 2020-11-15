﻿using System.Windows.Forms;
using SkiaSharp.Views.Desktop;
using System.Drawing;
using Trains.NET.Rendering;
using System;
using System.Linq;

namespace Trains.NET
{
    public partial class MainForm : Form
    {
        private readonly IGame _game;
        public MainForm(IGame game)
        {
            _game = game;
            this.Text = "Trains.NET";
            this.AutoScaleMode = AutoScaleMode.Font;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1086, 559);
            this.ClientSize = new Size(1547, 897);
            var splitContainer = new SplitContainer()
            {
                FixedPanel = FixedPanel.Panel1,
                SplitterDistance = 400,
                Dock = DockStyle.Fill,
                IsSplitterFixed = true
            };

            var buttonPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(5)
            };
            foreach(Tool tool in ((Tool[])Enum.GetValues(typeof(Tool))).Reverse()){
                buttonPanel.Controls.Add(CreateButton(tool));
            }
            
            var skiaView = new SKControl()
            {
                Dock = DockStyle.Fill
            };
            skiaView.MouseDown += DoMouseClick;
            skiaView.MouseMove += DoMouseClick;
            skiaView.Resize += (s, e) => _game.SetSize(skiaView.Width, skiaView.Height);
            skiaView.PaintSurface += (s, e) => _game.Render(e.Surface);
            splitContainer.Panel1.Controls.Add(buttonPanel);

            splitContainer.Panel2.Controls.Add(skiaView);
            splitContainer.Panel2.Padding = new Padding(5);
            this.Controls.Add(splitContainer);

            void DoMouseClick(object sender, MouseEventArgs e)
            {
                if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
                _game.OnMouseDown(e.X, e.Y);
                skiaView.Refresh();
            }
            RadioButton CreateButton(Tool tool)
            {
                var button =  new RadioButton()
                {
                    Text = tool.ToString(),
                    Dock = DockStyle.Top,
                    Height = 40,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Appearance = Appearance.Button,
                    Checked = tool == 0
                };
                button.Click += (s, e) => _game.CurrentTool = tool;
                return button;
            }
        }

        
    }
}
