using LinaqMinesweeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LinaqMinesweeper.Model
{
    public class GameField:BaseModel
    {
        private int Size;
        public GameField(int size, int mines)
        {
            Size = size;
            var random = new Random();
            HashSet<int> numbers = new HashSet<int>();
            while (numbers.Count < mines)
            {
                numbers.Add(random.Next(0, Size * Size - 1));
            }

            for (int i = 0; i < size * size; i++)
            {

                FieldTiles.Add(new Tile() { IsBomb = numbers.Contains(i)});
            }

            UpdateBombsNumbers();
            SubscribeToevents();
        }

        public ObservableCollection<Tile> _fieldTiles = new ObservableCollection<Tile>();
        public ObservableCollection<Tile> FieldTiles
        {
            get
            {
                return _fieldTiles;
            }
            set
            {
                _fieldTiles = value;
                RaisePropertyChanged("FieldTiles");
            }
        }

        private void UpdateBombsNumbers()
        {
            ObservableCollection<Tile> updatedfield = new ObservableCollection<Tile>();
            foreach (var item in FieldTiles)
            {
                int cellIndex = FieldTiles.IndexOf(item);

                int topIndex = cellIndex - Size;
                int topRightIndex = cellIndex - Size + 1;
                int rightIndex = cellIndex + 1;
                int bottomRightIndex = cellIndex + Size + 1;
                int bottomIndex = cellIndex + Size;
                int bottomLeftIndex = cellIndex + Size - 1;
                int leftIndex = cellIndex - 1;
                int topLeftIndex = cellIndex - Size - 1;

                bool topEdge = cellIndex < Size ? true : false;
                bool rightEdge = ((cellIndex + 1) % Size) == 0 ? true : false;
                bool leftEdge = (cellIndex % Size) == 0 ? true : false;
                bool bottomEdge = (cellIndex + Size) >= (Size * Size) ? true : false;

                int numberOfNearBobms = 0;
                if (!topEdge && IsMined(topIndex))
                {
                    numberOfNearBobms++;
                }
                if (!rightEdge && IsMined(topRightIndex))
                {
                    numberOfNearBobms++;
                }
                if (!rightEdge && IsMined(rightIndex))
                {
                    numberOfNearBobms++;
                }
                if (!rightEdge && IsMined(bottomRightIndex))
                {
                    numberOfNearBobms++;
                }
                if (!bottomEdge && IsMined(bottomIndex))
                {
                    numberOfNearBobms++;
                }
                if (!leftEdge && IsMined(bottomLeftIndex))
                {
                    numberOfNearBobms++;
                }
                if (!leftEdge && IsMined(leftIndex))
                {
                    numberOfNearBobms++;
                }
                if (!leftEdge && IsMined(topLeftIndex))
                {
                    numberOfNearBobms++;
                }
                Tile updatedCell = new Tile(item);
                if (numberOfNearBobms > 0)
                    updatedCell.NumberOfBombsNear = numberOfNearBobms.ToString();
                else
                    updatedCell.NumberOfBombsNear = "";
                updatedfield.Add(updatedCell);
            }

            if (updatedfield.Count == _fieldTiles.Count)
            {
                foreach (Tile gridCell in _fieldTiles)
                {
                    string numbofbombs = updatedfield.ElementAt(_fieldTiles.IndexOf(gridCell)).NumberOfBombsNear;
                    if (numbofbombs != gridCell.NumberOfBombsNear)
                    {
                        gridCell.NumberOfBombsNear = numbofbombs;
                    }
                }
            }
            RaisePropertyChanged("FieldTiles");

        }
        private void SubscribeToevents()
        {
            foreach (var item in FieldTiles)
            {
                item.BombClicked += GameOver;
            }
        }
        private void GameOver(object sender, EventArgs e)
        {
            ShowAllTiles();
        }

        private void ShowAllTiles()
        {
            foreach (var item in FieldTiles)
            {
                item.IsClicked = true;
                item.CanShowNumberNear = Visibility.Hidden;
                item.NumberOfBombsNear = "";
                if (item.IsBomb)
                {
                    item.DisplayBomb= true;
                }
            }
        }

        private bool IsMined(int cellIndex)
        {
            return IsCellIndexValid(cellIndex) && _fieldTiles.ElementAt(cellIndex).IsBomb == true;
        }
        private bool IsCellIndexValid(int cellIndex)
        {
            return cellIndex >= 0 && cellIndex < _fieldTiles.Count;
        }
    }
}
