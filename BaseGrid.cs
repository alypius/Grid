using System;
using System.Linq;
using System.Text;

namespace Algorithms.Grid
{
    public abstract class BaseGrid<TCell> where TCell : class, ICell
    {
        private TCell[,] _grid;

        protected int RowCount { get { return this._grid.GetLength(0); } }
        protected int ColCount { get { return this._grid.GetLength(1); } }

        public BaseGrid(int rowCount, int colCount, Func<int, int, TCell> createInitialCell)
        {
            this._grid = new TCell[rowCount, colCount];
            InitializeGrid(createInitialCell);
        }

        public TCell GetCell(int row, int col)
        {
            return this._grid[row, col];
        }

        public TCell GetCell(Tuple<int, int> positionTuple)
        {
            return GetCell(positionTuple.Item1, positionTuple.Item2);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            var separatorLine = String.Join("", Enumerable.Repeat('-', this.ColCount * 2 + 3));

            stringBuilder.AppendLine(separatorLine);

            for (int i = 0; i < this.RowCount; i++)
            {
                stringBuilder.Append("[ ");
                for (int j = 0; j < this.ColCount; j++)
                {
                    stringBuilder.Append(this._grid[i, j].ToChar() + " ");
                }
                stringBuilder.AppendLine("]");
            }

            stringBuilder.AppendLine(separatorLine);

            return stringBuilder.ToString();
        }

        protected TCell[][] Get2DCellArray()
        {
            return Enumerable.Range(0, this.RowCount)
                    .Select(row =>
                        Enumerable.Range(0, this.ColCount)
                            .Select(col => this.GetCell(row, col))
                            .ToArray())
                    .ToArray();
        }

        protected TCell[] GetFlatCellArray()
        {
            return this.Get2DCellArray()
                .SelectMany(it => it)
                .ToArray();
        }

        protected bool IsValidPosition(int row, int col)
        {
            return row >= 0 && col >= 0 && row < this.RowCount && col < this.ColCount;
        }

        protected bool IsValidPosition(Tuple<int, int> tuple)
        {
            return IsValidPosition(tuple.Item1, tuple.Item2);
        }

        protected TCell[] GetNeighbors(TCell cell)
        {
            return Enumerable.Range(-1, 3)
                .SelectMany(rowDelta => Enumerable.Range(-1, 3)
                    .Select(colDelta => Tuple.Create(cell.Row + rowDelta, cell.Col + colDelta))
                )
                .Where(this.IsValidPosition)
                .Select(it => this.GetCell(it.Item1, it.Item2))
                .ToArray();
        }

        private void InitializeGrid(Func<int, int, TCell> createInitialCell)
        {
            for (int i = 0; i < this.RowCount; i++)
                for (int j = 0; j < this.ColCount; j++)
                    this._grid[i, j] = createInitialCell(i, j);
        }
    }
}
