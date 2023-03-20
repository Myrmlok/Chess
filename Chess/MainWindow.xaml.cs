using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MVMM;
namespace Chess
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ViewModal viewModal;
        public MainWindow()
        {
            
            InitializeComponent();
            viewModal = new ViewModal();
            DataContext = viewModal;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button button = new Button();
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    button.Click += Button_Click_2;
                    if (j % 2 == i % 2)
                    {
                        button.Background = Brushes.White;
                    }
                    else
                    {
                        button.Background = Brushes.Firebrick;
                    }
                    grid.Children.Add(button);
                }
            }
            for (int i = 0; i < viewModal.ChessWhite.FiguresMany.Count; i++)
            {
                Button button = new Button();
                button.DataContext = viewModal.ChessWhite.FiguresMany[i];
                Grid.SetColumn(button, viewModal.ChessWhite.FiguresMany[i].X);
                Grid.SetRow(button, viewModal.ChessWhite.FiguresMany[i].Y);
                button.Click += Button_Click_1;
                button.Background = new ImageBrush(new BitmapImage(new Uri(viewModal.ChessWhite.FiguresMany[i].SourceImage)));
                if (viewModal.ChessWhite.FiguresMany[i].IsRook)
                {
                    if (viewModal.ChessWhite.FiguresMany[i].X == 0)
                    {
                        viewModal.ChessWhite.king.RookLeft = button;
                    }
                    if (viewModal.ChessWhite.FiguresMany[i].X == 7)
                    {
                        viewModal.ChessWhite.king.RookRight = button;
                    }
                }
                grid.Children.Add(button);
            }
            for (int i = 0; i < viewModal.ChessBlack.FiguresMany.Count; i++)
            {
                Button button = new Button();
                button.DataContext = viewModal.ChessBlack.FiguresMany[i];
                Grid.SetColumn(button, viewModal.ChessBlack.FiguresMany[i].X);
                Grid.SetRow(button, viewModal.ChessBlack.FiguresMany[i].Y);
                button.Click += Button_Click_1;
                button.Background = new ImageBrush(new BitmapImage(new Uri(viewModal.ChessBlack.FiguresMany[i].SourceImage)));
                if (viewModal.ChessBlack.FiguresMany[i].IsRook)
                {
                    if (viewModal.ChessBlack.FiguresMany[i].X == 0)
                    {
                        viewModal.ChessBlack.king.RookLeft = button;
                    }
                    if (viewModal.ChessBlack.FiguresMany[i].X == 7)
                    {
                        viewModal.ChessBlack.king.RookRight = button;
                    }
                }
                grid.Children.Add(button);
            }
            
        }
        private void button_figure_click(object sender, EventArgs e)
        {

           
        }
        private void button_click(object sender, EventArgs e)
        {
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            Button button = sender as Button;
            Figures figures = button.DataContext as Figures;
            if (viewModal.WhiteMove == figures.IsWhite)
            {
                viewModal.button = button;
                viewModal.SelectedFigure = figures;
                viewModal.CanGo = figures.CanGo(viewModal.ChessWhite, viewModal.ChessBlack);
            }
            else
            {
                Button_Click_2(sender, e);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int x = Grid.GetColumn(button);
            int y = Grid.GetRow(button);
            Point point = new Point(x, y);
            
            if (viewModal.CanGo == null ? false : viewModal.CanGo.Contains(point))
            {
               
                SideChess sideChess;
                SideChess friend;
                List<Point> unfreind = new List<Point>();
                if (viewModal.SelectedFigure.IsWhite)
                {
                    unfreind = viewModal.ChessBlack.GetPoints();
                    sideChess = viewModal.ChessBlack;
                    friend = viewModal.ChessWhite;
                }
                else
                {
                    unfreind = viewModal.ChessWhite.GetPoints();
                    sideChess = viewModal.ChessWhite;
                    friend = viewModal.ChessBlack;
                }
                if (unfreind != null)
                {
                    
                    if (unfreind.Contains(point))
                    {
                        int index = unfreind.IndexOf(point);
                        sideChess.FiguresMany[index].X = -1;
                        sideChess.FiguresMany[index].Y = -1;
                        grid.Children.Remove(button);
                        if (sideChess.FiguresMany[index].IsKing)
                        {
                            viewModal.SelectedFigure.X = x;
                            viewModal.SelectedFigure.Y = y;
                            Grid.SetColumn(viewModal.button, x);
                            Grid.SetRow(viewModal.button, y);
                            if (sideChess.FiguresMany[index].IsWhite)
                            {
                                MessageBox.Show("Black wins");
                            }
                            else
                            {
                                MessageBox.Show("White wins");
                            }
                            Close();
                        }
                    }
                }
                
                viewModal.SelectedFigure.X = x;
                viewModal.SelectedFigure.Y = y;
                Grid.SetColumn(viewModal.button, x);
                Grid.SetRow(viewModal.button, y);
               
                if (viewModal.SelectedFigure.IsKing&& viewModal.SelectedFigure.FirstMove)
                {
                    King king = viewModal.SelectedFigure as King;
                    if (point == king.RookLeftMove&&king.RookLeft!=null)
                    {
                        Rook rook= king.RookLeft.DataContext as Rook;
                        int index= friend.FiguresMany.IndexOf(rook);
                        friend.FiguresMany[index].X = x + 1;
                        Grid.SetColumn(king.RookLeft, friend.FiguresMany[index].X);
                    }
                    if (point == king.RookRightMove && king.RookRight  != null)
                    {
                        Rook rook = king.RookRight.DataContext as Rook;
                        int index = friend.FiguresMany.IndexOf(rook);
                        friend.FiguresMany[index].X = x - 1;
                        Grid.SetColumn(king.RookRight, friend.FiguresMany[index].X);
                    }
                }
                viewModal.WhiteMove = !viewModal.WhiteMove;
                if (viewModal.SelectedFigure.IsPawn)
                {
                    if (viewModal.SelectedFigure.Y == 7 || viewModal.SelectedFigure.Y == 0)
                    {
                        string src = "";
                        if (viewModal.SelectedFigure.IsWhite)
                        {
                            src = "White";
                        }
                        else
                        {
                            src = "Black";
                        }
                        int _x = viewModal.SelectedFigure.X;
                        int _y = viewModal.SelectedFigure.Y;
                        bool isWhite = viewModal.SelectedFigure.IsWhite;
                        int index = friend.FiguresMany.IndexOf(viewModal.SelectedFigure);
                        friend.FiguresMany[index]= new Queen() { X = _x, Y = _y,IsWhite= isWhite, SourceImage = $@"C:\Users\User\Desktop\Шаг\Дмитрий\C#\homework\Chess\Chess\img\{"Queen" + src + ".png"}" };
                        viewModal.button.Background = new ImageBrush(new BitmapImage(new Uri(friend.FiguresMany[index].SourceImage)));
                        viewModal.button.DataContext = friend.FiguresMany[index];
                        viewModal.SelectedFigure.FirstMove = false;

                    }
                }
                
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
    
}
