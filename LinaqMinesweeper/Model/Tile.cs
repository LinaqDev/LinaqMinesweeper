using LinaqMinesweeper.Model;
using LinaqMinesweeper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LinaqMinesweeper
{
    public class Tile : BaseModel
    {
        public EventHandler<EventArgs> BombClicked;
        public Tile()
        {
            ToggleLeftClickCmd = new RelayCommand(ToggleLeftClickExe);
            ToggleRightClickCmd = new RelayCommand(ToggleRightClickExe);
        }

        public Tile(Tile item)
        {
            this.CanShowNumberNear = item.CanShowNumberNear;
            this.DisplayBomb = item.DisplayBomb;
            this.IsBomb = item.IsBomb;
            this.IsClicked = item.IsClicked;
            this.IsFlagged = item.IsFlagged;
            this.NumberOfBombsNear = item.NumberOfBombsNear; 
        }

        private void ToggleRightClickExe(object obj)
        {
            IsFlagged = !IsFlagged;
        }

        private void ToggleLeftClickExe(object obj)
        {
            if (IsFlagged)
                IsFlagged = false;

            if (!IsBomb)
            {
                CanShowNumberNear = Visibility.Visible;
                IsClicked = true;
            }
            else
            {
                BombClicked("",EventArgs.Empty);
                DisplayBomb = true;
            }
        }

        private bool _isClicked = false;
        public bool IsClicked
        {
            get
            {
                return _isClicked;
            }
            set
            {
                _isClicked = value;
                RaisePropertyChanged("IsClicked");
            }
        }

        private bool _isFlagged = false;
        public bool IsFlagged
        {
            get
            {
                return _isFlagged;
            }
            set
            {
                _isFlagged = value;
                RaisePropertyChanged("IsFlagged");
            }
        }

        private bool _displayBomb = false;
        public bool DisplayBomb
        {
            get
            {
                return _displayBomb;
            }
            set
            {
                _displayBomb = value;
                RaisePropertyChanged("DisplayBomb");
            }
        }

        private bool _isBomb = false;
        public bool IsBomb
        {
            get
            {
                return _isBomb;
            }
            set
            {
                _isBomb = value;
                RaisePropertyChanged("IsBomb");
            }
        }
        private string _numberOfBombsNear = "1";
        public string NumberOfBombsNear
        {
            get
            {
                return _numberOfBombsNear;
            }
            set
            {
                _numberOfBombsNear = value;
                RaisePropertyChanged("NumberOfBombsNear");
            }
        }

        private Visibility _canShowNumberNear = Visibility.Collapsed;
        public Visibility CanShowNumberNear
        {
            get
            {
                return _canShowNumberNear;
            }
            set
            {
                _canShowNumberNear = value;
                RaisePropertyChanged("CanShowNumberNear");
            }
        }
        public ICommand ToggleLeftClickCmd { get; set; }
        public ICommand ToggleRightClickCmd { get; set; }

    }
}
