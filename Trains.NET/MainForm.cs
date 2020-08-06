using System.Windows.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace Trains.NET
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.Text = "Trains.NET";
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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

            buttonPanel.Controls.Add(
                new Button() { 
                    Text = "Track",
                    Dock = DockStyle.Top,
                    Height = 40,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            });
            buttonPanel.Controls.Add(
                new Button()
                {
                    Text = "Pointer",
                    Dock = DockStyle.Top,
                    Height = 40,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                });
            var skiaView = new SKControl()
            {
                Dock = DockStyle.Fill
            };
            skiaView.PaintSurface += (s, e) => e.Surface.Canvas.Clear(SKColors.White);
            splitContainer.Panel1.Controls.Add(buttonPanel);

            splitContainer.Panel2.Controls.Add(skiaView);
            splitContainer.Panel2.Padding = new Padding(5);
            this.Controls.Add(splitContainer);
        }
    }
}
