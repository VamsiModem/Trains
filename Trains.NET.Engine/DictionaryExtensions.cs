using System.Collections.Generic;

namespace Trains.NET.Engine
{
    public static class DictionaryExtensions
    {
        public static void Deconstruct(this KeyValuePair<(int, int), Track> kvp, out int col, out int row, out Track track)
        {
            col = kvp.Key.Item1;
            row = kvp.Key.Item2;
            track = kvp.Value;
        }
    }
}
