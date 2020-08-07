using SkiaSharp;

namespace Trains.NET.Rendering
{
    public class GridRenderer : IBoardRenderer
    {
        private const int CellSize = 25;
        void IBoardRenderer.Render(SKSurface surface, int width, int height)
        {
            SKCanvas canvas = surface.Canvas;
            canvas.Translate(1, 1);
            canvas.Clear(SKColors.White);
            canvas.ClipRect(new SKRect(0, 0, width + 2, height + 2), SKClipOperation.Intersect);
            using var grid = new SKPaint
            {
                Color = SKColors.LightGray,
                StrokeWidth = 1,
                Style = SKPaintStyle.Stroke

            };
            for (int x = 0; x < width + 1; x += CellSize)
            {
                canvas.DrawLine(x, 0, x, height, grid);
            }
            for (int y = 0; y < height + 1; y += CellSize)
            {
                canvas.DrawLine(0, y, width, y, grid);
            }

        }
    }
}
