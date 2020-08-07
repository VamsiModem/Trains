using System;
using SkiaSharp;
using Trains.NET.Engine;

namespace Trains.NET.Rendering
{
    public class Game
    {
        public const int CellSize = 25;
        private int _width;
        private int _height;
        private readonly GameBoard _gameBoard;
        private readonly IBoardRenderer _gridRenderer = new GridRenderer();
        private readonly IBoardRenderer _trackLayoutRenderer;
        public Tool CurrentTool { get; set; }
        public Game(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
            _trackLayoutRenderer = new TrackLayoutRenderer(gameBoard);
        }
        public void SetSize(int width, int height)
        {
            var columns = Math.Max(width / CellSize, 1);
            //if (columns * CellSize > width)
            //{
            //    columns--;
            //}
            var rows = Math.Max(height / CellSize, 1);
            //if (rows * CellSize > height && rows > 1)
            //{
            //    rows--;
            //}
            _width = columns * CellSize;
            _height = rows * CellSize;
            _gameBoard.Rows = rows;
            _gameBoard.Columns = columns;
        }

        public void Render(SKSurface surface)
        {
            SKCanvas canvas = surface.Canvas;
            canvas.Translate(1,1);
            canvas.Clear(SKColors.White);
            canvas.ClipRect(new SKRect(0, 0, _width + 2, _height + 2), SKClipOperation.Intersect);
            canvas.Save();
            _gridRenderer.Render(surface, _width, _height);
            canvas.Restore();
            canvas.Save();
            _trackLayoutRenderer.Render(surface, _width, _height);
            canvas.Restore();
        }

        public void OnMouseDown(int x, int y)
        {
            var column = x / CellSize;
            var row = y / CellSize;
            if(this.CurrentTool == Tool.Track)
            {
                _gameBoard.AddTrack(column, row);
            }
        }
    }
}
