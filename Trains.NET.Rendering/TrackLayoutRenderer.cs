using SkiaSharp;
using Trains.NET.Engine;

namespace Trains.NET.Rendering
{
    internal class TrackLayoutRenderer : IBoardRenderer
    {
        private const int PlankWidth = 3;
        private const int Padding = 4;
        private const int TrackPadding = 7;
        private const int TrackWidth = 3;
        private readonly GameBoard _gameBoard;

        public TrackLayoutRenderer(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        void IBoardRenderer.Render(SKSurface surface, int width, int height)
        {
            const int numPlanks = 3;
            float plankGap = Game.CellSize / numPlanks;
            SKCanvas canvas = surface.Canvas;
            foreach((int col, int row, Track track) in _gameBoard.GetTracks())
            {
                canvas.Save();
                canvas.Translate(col * Game.CellSize, row * Game.CellSize);
                using var path = new SKPath();
                for(int i = 1; i < numPlanks + 1; i++)
                {
                    var pos = (i * plankGap) - (plankGap / 2) + 1;
                    path.MoveTo(pos, Padding);
                    path.LineTo(pos, Game.CellSize - Padding);
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
                canvas.DrawRect(0, TrackPadding, Game.CellSize, TrackWidth, clear);
                canvas.DrawRect(0, Game.CellSize - TrackPadding - TrackWidth, Game.CellSize, TrackWidth, clear);
                using var trackPath = new SKPath();

                trackPath.MoveTo(0, TrackPadding);
                trackPath.LineTo(Game.CellSize, TrackPadding );
                trackPath.MoveTo(0, TrackPadding + TrackWidth);
                trackPath.LineTo(Game.CellSize, TrackPadding + TrackWidth);

                trackPath.MoveTo(0, Game.CellSize - TrackPadding - TrackWidth);
                trackPath.LineTo(Game.CellSize, Game.CellSize - TrackPadding - TrackWidth);
                trackPath.MoveTo(0, Game.CellSize - TrackPadding);
                trackPath.LineTo(Game.CellSize, Game.CellSize - TrackPadding);
                using var trackPaint = new SKPaint
                {
                    Color = SKColors.Black,
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 1
                };
                canvas.DrawPath(trackPath, trackPaint);
                canvas.Restore();
            }
        }
    }
}
