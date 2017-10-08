using System;

namespace Algorithms.Grid
{
    public interface ICell
    {
        int Row { get; }
        int Col { get; }
        char ToChar();
        Tuple<int, int> GetPositionTuple();
        double GetDistance(ICell otherCell);
    }
}
