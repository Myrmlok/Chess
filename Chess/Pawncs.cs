using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace MVMM
{
    public class Figures : INotifyPropertyChanged
    {
        int x;
        int y;
        bool isWhite;
        bool isKing = false;
        public bool IsRook = false;
        string sourceImage;
        bool isPawn = false;
        public bool FirstMove = true;
        public bool IsPawn
        {
            get => isPawn;
            set
            {
                isPawn = value;
                OnPropertyChangedvalue();
            }
        }
        public string SourceImage
        {
            get => sourceImage;
            set
            {
                sourceImage = value;
                OnPropertyChangedvalue();
            }
        }
        public bool IsKing
        {
            get => isKing;
            set
            {
                isKing = value;
                OnPropertyChangedvalue();
            }
        }
        public bool IsWhite
        {
            get => isWhite;
            set
            {
                isWhite = value;
                OnPropertyChangedvalue();
            }
        }

        public int X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChangedvalue();
            }
        }
        public int Y {
            get => y;
            set
            {
                y = value;
                OnPropertyChangedvalue();
            }
        }
        public virtual List<Point> CanGo(SideChess white, SideChess black)
        {

            throw new Exception();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChangedvalue([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
    public class Pawn : Figures
    {

        public Pawn()
        {
            IsPawn = true;
        }
       
        public override List<Point> CanGo(SideChess white, SideChess black)
        {
            if (white == null || black == null)
            {
                return null;
            }
            List<Point> friend;
            List<Point> unfriend;
            List<Point> points = new List<Point>();

            if (IsWhite)
            {
                friend = white.GetPoints();
                unfriend = black.GetPoints();
                int min_Y = Y - 1;
                if (FirstMove)
                {
                    min_Y = Y - 2;
                }
                if (unfriend.Contains(new Point(X + 1, Y - 1)))
                {
                    points.Add(new Point(X + 1, Y - 1));
                }
                if (unfriend.Contains(new Point(X - 1, Y - 1)))
                {
                    points.Add(new Point(X - 1, Y - 1));
                }
                for (int y = Y - 1; y >= min_Y; y--)
                {
                    if (friend.Contains(new Point(X, y)) || unfriend.Contains(new Point(X, y)))
                    {
                        return points;
                    }
                    else
                    {
                        points.Add(new Point(X, y));
                    }
                }



            }
            else
            {
                friend = black.GetPoints();
                unfriend = white.GetPoints();
                int min_Y = Y + 1;
                if (FirstMove)
                {
                    min_Y = Y + 2;
                    FirstMove = false;
                }
                if (unfriend.Contains(new Point(X + 1, Y + 1)))
                {
                    points.Add(new Point(X + 1, Y + 1));
                }
                if (unfriend.Contains(new Point(X - 1, Y + 1)))
                {
                    points.Add(new Point(X - 1, Y + 1));
                }
                for (int y = Y + 1; y <= min_Y; y++)
                {
                    if (friend.Contains(new Point(X, y)) || unfriend.Contains(new Point(X, y)))
                    {
                        return points;
                    }
                    else
                    {
                        points.Add(new Point(X, y));
                    }
                }
            }
            
            return points;
        }

    }
    public class Horse:Figures
    {
        public override List<Point> CanGo(SideChess white, SideChess black)
        {
            if (white == null || black == null)
            {
                return null;
            }
            List<Point> friend;
            List<Point> points = new List<Point>();

            if (IsWhite)
            {
                friend = white.GetPoints();
            }
            else
            {
                friend = black.GetPoints();
            }
            List<Point> listmove = new List<Point>() {
            new Point(X + 1,Y + 2),
            new Point(X - 1,Y + 2),
            new Point(X + 1,Y - 2),
            new Point(X - 1,Y - 2),

            new Point(X + 2, Y + 1),
            new Point(X + 2, Y - 1),
            new Point(X - 2, Y + 1),
            new Point(X - 2, Y - 1),
            };
            for(int i = 0; i < listmove.Count; i++)
            {
                if (friend.Contains(listmove[i]))
                {
                    listmove.RemoveAt(i);
                }
            }
            return listmove;
        }
    }
    public class Rook:Figures
    {
        public Rook()
        {
            IsRook = true;
        }
        
        public override List<Point> CanGo(SideChess white, SideChess black)
        {
            if (white == null || black == null)
            {
                return null;
            }
            List<Point> friend;
            List<Point> unfriend;
            List<Point> points = new List<Point>();

            if (IsWhite)
            {
                friend = white.GetPoints();
                unfriend = black.GetPoints();
            }
            else
            {
                friend = black.GetPoints();
                unfriend = white.GetPoints();
            }
            for(int i = X+1; i < 8; i++)
            {
                Point point = new Point(i,Y);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }
            for (int i = Y+1; i < 8; i++)
            {
                Point point = new Point(X, i);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }

            for (int i = X - 1; i >= 0; i--)
            {
                Point point = new Point(i, Y);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }
            for (int i = Y - 1; i >= 0; i--)
            {
                Point point = new Point(X, i);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }
            return points;
        }
    }
    public class Elephant:Figures
    {
        public override List<Point> CanGo(SideChess white, SideChess black)
        {
            if (white == null || black == null)
            {
                return null;
            }
            List<Point> friend;
            List<Point> unfriend;
            List<Point> points = new List<Point>();

            if (IsWhite)
            {
                friend = white.GetPoints();
                unfriend = black.GetPoints();
            }
            else
            {
                friend = black.GetPoints();
                unfriend = white.GetPoints();
            }
            for (int x = X + 1, y = Y+1; x < 8 && y < 8; x++, y++)
            {
                Point point = new Point(x, y);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }
            for (int x = X-1, y = Y-1; x >= 0 && y >=0 ; x--, y--)
            { 
                Point point = new Point(x, y);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }

            for (int x = X + 1, y = Y - 1; x < 8 && y >= 0; x++, y--)
            {
                Point point = new Point(x, y);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }
            for (int x = X - 1, y = Y + 1; x >= 0 && y <8; x--, y++)
            {
                Point point = new Point(x, y);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }
            return points;
        }
    }
    public class Queen : Figures
    {
        public override List<Point> CanGo(SideChess white, SideChess black)
        {
            if (white == null || black == null)
            {
                return null;
            }
            List<Point> friend;
            List<Point> unfriend;
            List<Point> points = new List<Point>();

            if (IsWhite)
            {
                friend = white.GetPoints();
                unfriend = black.GetPoints();
            }
            else
            {
                friend = black.GetPoints();
                unfriend = white.GetPoints();
            }
            for (int i = X + 1; i < 8; i++)
            {
                Point point = new Point(i, Y);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }
            for (int i = Y + 1; i < 8; i++)
            {
                Point point = new Point(X, i);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }

            for (int i = X - 1; i >= 0; i--)
            {
                Point point = new Point(i, Y);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }
            for (int i = Y - 1; i >= 0; i--)
            {
                Point point = new Point(X, i);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }
            for (int x = X + 1, y = Y + 1; x < 8 && y < 8; x++, y++)
            {
                Point point = new Point(x, y);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }
            for (int x = X - 1, y = Y - 1; x >= 0 && y >= 0; x--, y--)
            {
                Point point = new Point(x, y);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }

            for (int x = X + 1, y = Y - 1; x < 8 && y >= 0; x++, y--)
            {
                Point point = new Point(x, y);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }
            for (int x = X - 1, y = Y + 1; x >= 0 && y < 8; x--, y++)
            {
                Point point = new Point(x, y);
                if (unfriend.Contains(point))
                {
                    points.Add(point);
                    break;
                }
                if (friend.Contains(point))
                {
                    break;
                }
                else
                {
                    points.Add(point);
                }
            }
            return points;
        }
    }
    public class King:Figures
    {
      
        public Point RookLeftMove;
        public Point RookRightMove;

        public Button RookLeft;
        public Button RookRight;
        public King()
        {
            IsKing = true;
        }
        public override List<Point> CanGo(SideChess white, SideChess black)
        {
            if (white == null || black == null)
            {
                return null;
            }
            Rook rookLeft=RookLeft.DataContext as Rook;
            Rook rookRight = RookRight.DataContext as Rook;
            List<Point> friend;
            List<Point> unfriend;
            List<Point> points=new List<Point>();
            if (IsWhite)
            {
                friend = white.GetPoints();
                unfriend = black.GetPoints();
            }
            else
            {
                friend = black.GetPoints();
                unfriend = white.GetPoints();
            }
            for(int i = X - 1; i <= X + 1; i++)
            {
                for(int j = Y - 1; j < Y + 1; j++)
                {
                    if (i == X && j == Y)
                    {
                        continue;
                    }
                    else
                    {
                        Point point = new Point(i, j);
                        if (friend.Contains(point))
                        {
                            continue;
                        }
                        else
                        {
                            points.Add(point);
                        }
                    }
                }
            }
            for(int i = X - 1; i <= X + 1; i++)
            {
                for(int j = Y - 1; j <= Y + 1; j++)
                {
                    if (i == X && j == Y)
                    {
                        continue;
                    }
                    else
                    {
                        Point point = new Point(i, j);
                        if (friend.Contains(point))
                        {
                            continue;
                        }
                        else
                        {
                            points.Add(point);
                        }
                    }
                }
            }
            if(FirstMove && rookLeft.FirstMove)
            {
                Point point = new Point(X - 2, Y);
                if (!friend.Contains(point)&&
                    !unfriend.Contains(point)){
                    points.Add(point);
                    RookLeftMove = point;
                }
               
            }
            if (FirstMove && rookRight.FirstMove)
            {
                Point point = new Point(X + 2, Y);
                if (!friend.Contains(point)&&
                    !unfriend.Contains(point))
                {
                    points.Add(point);
                    RookRightMove = point;
                }
            }
            return points;
        }
    }
}
