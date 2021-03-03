using LinaqMinesweeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LinaqMinesweeper.ViewModel
{
    public class MainViewModel:BaseModel
    {
        
        public MainViewModel()
        {
            Size = 20;
            Gf = new GameField(Size, 4);
            NewGameCmd = new RelayCommand(NewGameExe);
        }

        private void NewGameExe(object obj)
        {
            Gf = new GameField(Size,20);
        }

        private int _size = 0;
        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                RaisePropertyChanged("Size");
            }
        }

        private GameField _gf;
        public GameField Gf
        {
            get
            {
                return _gf;
            }
            set
            {
                _gf = value;
                RaisePropertyChanged("Gf");
            }
        }

        public ICommand NewGameCmd { get; set; }
    }
}
