using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using MVMM;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
namespace Chess
{
    public class ViewModal:INotifyPropertyChanged
    {
        public Button button;
  
        SideChess white;
        SideChess black;
        Figures selectedFigure;
        List<Point> canGo;
        bool whiteMove=true;
        public bool WhiteMove
        {
            get => whiteMove;
            set
            {
                whiteMove = value;
            }
        }
        public List<Point> CanGo
        {
            get => canGo;
            set
            {
                canGo = value;
            }
        }
        public Figures SelectedFigure
        {
            get => selectedFigure;
            set
            {
                selectedFigure = value;
                OnPropertyChangedvalue();
            }
        }
        public SideChess ChessWhite
        {
            get { return white; }
            set
            {
                white = value;
                OnPropertyChangedvalue();
            }
        }
        public SideChess ChessBlack
        {
            get { return black; }
            set
            {
                black = value;
                OnPropertyChangedvalue();
            }
        }
        
       

        public ViewModal()
        {
            ChessWhite = new SideChess(true);
            ChessBlack = new SideChess(false);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChangedvalue([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
