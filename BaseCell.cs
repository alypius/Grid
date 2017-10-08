using System;

namespace Algorithms.Grid
{
    public abstract class BaseCell : ICell
    {
        private int _row;
        private int _col;

        public int Row { get { return this._row; } }
        public int Col { get { return this._col; } }

        public BaseCell(int row, int col)
        {
            this._row = row;
            this._col = col;
        }

        public abstract char ToChar();

        public Tuple<int, int> GetPositionTuple()
        {
            return Tuple.Create(this.Row, this.Col);
        }

        public double GetDistance(ICell otherCell)
        {
            return Math.Sqrt(Math.Pow(otherCell.Row - this.Row, 2) + Math.Pow(otherCell.Col - this.Col, 2));
        }
    }
}
