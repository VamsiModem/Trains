using SkiaSharp;
using Trains.NET.Engine;

namespace Trains.NET.Rendering
{
    internal class TrackRenderer : ITrackRenderer
    {
        private const int NumPlanks = 3;
        private const int PlankWidth = 3;
        private const int Padding = 4;
        private const int TrackPadding = 7;
        private const int TrackWidth = 3;
        public void Render(SKCanvas canvas, Track track, int width)
        {
            if(track.Direction == TrackDirection.Vertical)
            {
                canvas.RotateDegrees(90, Game.CellSize / 2, Game.CellSize / 2);
            }
            RenderStraightTrack(canvas, width);
        }

        private static void RenderStraightTrack(SKCanvas canvas, int width)
        {
            float plankGap = width / NumPlanks;
            using var path = new SKPath();
            for (int i = 1; i < NumPlanks + 1; i++)
            {
                var pos = (i * plankGap) - (plankGap / 2) + 1;
                path.MoveTo(pos, Padding);
                path.LineTo(pos, width - Padding);
            }
            using var plank = new SKPaint
            {
                Color = SKColors.Black,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = PlankWidth
            };
            canvas.DrawPath(path, plank);
            using var clear = new SKPaint
            {
                Color = SKColors.White,
                Style = SKPaintStyle.Fill,
                StrokeWidth = 0
            };
            canvas.DrawRect(0, TrackPadding, width, TrackWidth, clear);
            canvas.DrawRect(0, width - TrackPadding - TrackWidth, width, TrackWidth, clear);
            using var trackPath = new SKPath();
            trackPath.MoveTo(0, TrackPadding);
            trackPath.LineTo(width, TrackPadding);
            trackPath.MoveTo(0, TrackPadding + TrackWidth);
            trackPath.LineTo(width, TrackPadding + TrackWidth);

            trackPath.MoveTo(0, width - TrackPadding - TrackWidth);
            trackPath.LineTo(width, width - TrackPadding - TrackWidth);
            trackPath.MoveTo(0, width - TrackPadding);
            trackPath.LineTo(width, width - TrackPadding);
            using var trackPaint = new SKPaint
            {
                Color = SKColors.Black,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1
            };
            canvas.DrawPath(trackPath, trackPaint);
        }
    }
}
