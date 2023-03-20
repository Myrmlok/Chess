using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.IO;
namespace MVMM
{
    public class SideChess
    {
        public ObservableCollection<Figures> FiguresMany { get; set; }
        public King king;
        public SideChess(bool isWhite)
        {
            int Y;
            int Y_pawns;
            string src;
            if (isWhite)
            {
                src="White";
                Y = 7;
                Y_pawns = Y - 1;
            }
            else
            {
                src = "Black";
                Y = 0;
                Y_pawns = Y + 1;
            }
            FiguresMany = new ObservableCollection<Figures>();
            for(int i = 0; i < 8; i++)
            {
                FiguresMany.Add(new Pawn { X=i, Y=Y_pawns,IsWhite=isWhite,SourceImage = $@"{Directory.GetCurrentDirectory()}\img\{"pawn"+src+".png"}"});
            }
            FiguresMany.Add(new Rook { X = 7, Y = Y, IsWhite = isWhite, SourceImage = $@"{Directory.GetCurrentDirectory()}\img\{"Rook" + src + ".png"}" });
            FiguresMany.Add(new Rook { X = 0, Y = Y, IsWhite = isWhite, SourceImage = $@"{Directory.GetCurrentDirectory()}\img\{"Rook" + src + ".png"}" });
            FiguresMany.Add(new Horse { X = 1, Y = Y,IsWhite=isWhite, SourceImage = $@"{Directory.GetCurrentDirectory()}\img\{"Horse" + src + ".png"}" });
            FiguresMany.Add(new Horse { X = 6, Y = Y,IsWhite=isWhite, SourceImage = $@"{Directory.GetCurrentDirectory()}\img\{"Horse" + src + ".png"}" });
           
            FiguresMany.Add(new Elephant { X = 2, Y = Y, IsWhite = isWhite, SourceImage = $@"{Directory.GetCurrentDirectory()}\img\{"Elephant" + src + ".png"}" });
            FiguresMany.Add(new Elephant { X = 5, Y = Y, IsWhite = isWhite, SourceImage = $@"{Directory.GetCurrentDirectory()}\img\{"Elephant" + src + ".png"}" });
            FiguresMany.Add(new Queen { X=3,Y=Y,IsWhite = isWhite, SourceImage = $@"{Directory.GetCurrentDirectory()}\img\{"Queen" + src + ".png"}" });
            king = new King { X = 4, Y = Y, IsWhite = isWhite, SourceImage = $@"{Directory.GetCurrentDirectory()}\img\{"King" + src + ".png"}" };
            FiguresMany.Add(king);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChangedvalue([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public List<Point> GetPoints()
        {
            List<Point> points = new List<Point>();
            for(int i = 0; i < FiguresMany.Count; i++)
            {
                points.Add(new Point(FiguresMany[i].X, FiguresMany[i].Y));
            }
            return points;
        }
    }
}
