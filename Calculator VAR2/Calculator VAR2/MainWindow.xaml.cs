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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator_VAR2
{
  
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private CalculatorLogic calculator = new CalculatorLogic();
        private MemoryManager memoryManager = new MemoryManager();

        public event PropertyChangedEventHandler PropertyChanged;
        double Nr_crt;
        char lastOp ='0';
        bool isMoreThanOnce = false;
        double mpy;
        bool isComaActive = false;
        private bool isGridVisible = false;
        string[] myArray = new string[0];

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new Number();
        }

        private void Button_Number_Click(object sender, RoutedEventArgs e)
        {
            DataContext= calculator.DataContext;
            if (calculator.lastOp=='0')
            {
                (DataContext as Number).sum=0;
                TextBlock_Suma.Visibility=Visibility.Hidden;
            }
            string numberText = (sender as Button).Content.ToString();
            double value = double.Parse(numberText);
            calculator.Scrie_Nr(value, isComaActive, ref mpy);
        }
        private void Button_Operation_Click(object sender, RoutedEventArgs e)
        {
            DataContext= calculator.DataContext;
            char operation = (sender as Button).Content.ToString()[0];
            calculator.Calcul(operation, (DataContext as Number).Nr_crt);
            (DataContext as Number).ecuation=(DataContext as Number).sum.ToString() +operation;
            isMoreThanOnce= false;
            isComaActive= false;
            TextBlock_Suma.Visibility=Visibility.Visible;
        }

        private void Button_Predef_Click(object sender, RoutedEventArgs e)
        {
            DataContext= calculator.DataContext;
            string symbol = (sender as Button).Content.ToString();
            calculator.Predefinit(symbol, (DataContext as Number).Nr_crt);
            isMoreThanOnce= false;
            isComaActive= false;
            TextBlock_Suma.Visibility=Visibility.Visible;
        }
        private void Button_Egal_Click(object sender, RoutedEventArgs e)
        {
            calculator.Egal(ref isMoreThanOnce, ref isComaActive);
            calculator.Reset();
            DataContext= calculator.DataContext;
        }
        /*

        public MainWindow()
        {
            
            InitializeComponent();
            this.DataContext = new Number { Nr_crt = 0 };
            Nr_crt = 0;
            EventManager.RegisterClassHandler(typeof(UIElement), UIElement.KeyDownEvent, new KeyEventHandler(GlobalKeyDown));
        }*/
        private void ShowGridAnimation()
        {
            
            DoubleAnimation moveUp = new DoubleAnimation
            {
                From = 200, 
                To = 0,  
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            GridTransform.BeginAnimation(TranslateTransform.YProperty, moveUp);


            Overlay.Visibility = Visibility.Visible;
            DoubleAnimation fadeIn = new DoubleAnimation
            {
                From = 0,
                To = 0.5,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            Overlay.BeginAnimation(UIElement.OpacityProperty, fadeIn);
        }
        private void HideGridAnimation()
        {
            DoubleAnimation moveDown = new DoubleAnimation
            {
                From = 0,
                To = 200,
                Duration = TimeSpan.FromSeconds(1.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            GridTransform.BeginAnimation(TranslateTransform.YProperty, moveDown);
            moveDown.Completed += (s, e) => Memory_Grid.Visibility = Visibility.Hidden;
            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 0.5,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            fadeOut.Completed += (s, e) => Overlay.Visibility = Visibility.Collapsed;
            Overlay.BeginAnimation(UIElement.OpacityProperty, fadeOut);

            isGridVisible = false;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isGridVisible && !Memory_Grid.IsMouseOver)
            {
                HideGridAnimation();
                Scoll.Visibility = Visibility.Hidden;
            }
        }
        private void PopulateGrid(string[] values)
        {
            Memory_Grid.Children.Clear();
            Memory_Grid.RowDefinitions.Clear();

            for (int i = 0; i < values.Length; i++)
            {
                Memory_Grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                TextBlock textBox = new TextBlock
                {
                    Text = values[i],
                    FontSize = 40,
                    Margin = new Thickness(0),
                    Foreground = Brushes.White,
                    Background = Brushes.Transparent,
                    TextAlignment = TextAlignment.Right,
                };

                Grid.SetRow(textBox, i);
                Memory_Grid.Children.Add(textBox);
            }
        }
        private void GlobalKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled) return;
            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    Button_0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D1:
                case Key.NumPad1:
                    Button_1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D2:
                case Key.NumPad2:
                    Button_2.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D3:
                case Key.NumPad3:
                    Button_3.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D4:
                case Key.NumPad4:
                    Button_4.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D5:
                case Key.NumPad5:
                    Button_5.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D6:
                case Key.NumPad6:
                    Button_6.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D7:
                case Key.NumPad7:
                    Button_7.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D8:
                case Key.NumPad8:
                    Button_8.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D9:
                case Key.NumPad9:
                    Button_9.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.OemPlus:
                case Key.Add:
                    Button_plus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.OemMinus:
                case Key.Subtract:
                    Button_Minus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.Oem2:
                case Key.Divide:
                    Button_Impartire.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.Oem8:
                case Key.Multiply:
                    Button_Inmultire.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.Enter:
                    Button_Egal.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.Back:
                    Button_Stergere.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
            }
            e.Handled = true;
        }
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
     
        private void Button_Stergere_Click(object sender, RoutedEventArgs e)
        {
            Nr_crt=Math.Floor(Nr_crt / 10);
            (DataContext as Number).Nr_crt=Nr_crt;
            isMoreThanOnce= false;
        }

        private void Button_C_Click(object sender, RoutedEventArgs e)
        {
            lastOp='0';
            (DataContext as Number).sum = 0;
            TextBlock_Suma.Visibility = Visibility.Hidden;
            Nr_crt=0;
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
            calculator.Reset();
            DataContext= calculator.DataContext;
        }

        private void Button_CE_Click(object sender, RoutedEventArgs e)
        {
            Nr_crt=0;
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        
        private void Button_Virgula_Click(object sender, RoutedEventArgs e)
        {
            isComaActive=true;
            mpy=0.1;
        }

        private void Button_PlusMinus_Click(object sender, RoutedEventArgs e)
        {
            Nr_crt*=-1;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!isGridVisible)
            {
                ShowGridAnimation();
                isGridVisible = true;
               //Memory_Grid.Visibility=Visibility.Visible;
                Scoll.Visibility=Visibility.Visible;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ///button M+
            try
            {
                if (Nr_crt==0)
                {
                    myArray[0] = (int.Parse(myArray[0]) +  (DataContext as Number).sum).ToString();
                }
                else
                {
                    myArray[0] = (int.Parse(myArray[0]) + Nr_crt).ToString();
                }
            (DataContext as Number).myArray = myArray;
            PopulateGrid(myArray);
            }
            catch { }
            Button_mr.IsEnabled= (DataContext as Number).IsButtonEnabled;
            Button_mc.IsEnabled= (DataContext as Number).IsButtonEnabled;
            Button_mv.IsEnabled= (DataContext as Number).IsButtonEnabled;
        }

        private void Ms_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Nr_crt==0)
            {
                myArray = new[] { (DataContext as Number).sum.ToString() }.Concat(myArray).ToArray();
            }
            else
            {
            myArray = new[] { Nr_crt.ToString() }.Concat(myArray).ToArray();
            }
             (DataContext as Number).myArray = myArray;
            PopulateGrid(myArray);
            Button_mr.IsEnabled= (DataContext as Number).IsButtonEnabled;
            Button_mc.IsEnabled= (DataContext as Number).IsButtonEnabled;
            Button_mv.IsEnabled= (DataContext as Number).IsButtonEnabled;
        }

        private void Button_M__Minus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Nr_crt==0)
                {
                    myArray[0] = (int.Parse(myArray[0]) -  (DataContext as Number).sum).ToString();
                }
                else
                {
                    myArray[0] = (int.Parse(myArray[0]) - Nr_crt).ToString();
                }
                 (DataContext as Number).myArray = myArray;
                PopulateGrid(myArray);
            }
            catch { }
            Button_mr.IsEnabled= (DataContext as Number).IsButtonEnabled;
            Button_mc.IsEnabled= (DataContext as Number).IsButtonEnabled;
            Button_mv.IsEnabled= (DataContext as Number).IsButtonEnabled;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ///button MC
            myArray = new string[0];
            (DataContext as Number).myArray = myArray;
            PopulateGrid(myArray);
            Button_mr.IsEnabled= (DataContext as Number).IsButtonEnabled;
            Button_mc.IsEnabled= (DataContext as Number).IsButtonEnabled;
            Button_mv.IsEnabled= (DataContext as Number).IsButtonEnabled;
        }

        private void Button_mr_Click(object sender, RoutedEventArgs e)
        {
            Nr_crt=int.Parse(myArray[0]);
            (DataContext as Number).Nr_crt=Nr_crt;
        }
    }
}