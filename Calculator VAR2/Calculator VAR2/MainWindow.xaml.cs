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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        double Nr_crt;
        char lastOp ='0';
        bool isMoreThanOnce = false;
        double mpy;
        bool isComaActive = false;
        char anteLastOp;
        double Nr_Here = 0;
        private bool isGridVisible = false;
        public MainWindow()
        {
            
            InitializeComponent();
            this.DataContext = new Number { Nr_crt = 0 };
            Nr_crt = 0;
            EventManager.RegisterClassHandler(typeof(UIElement), UIElement.KeyDownEvent, new KeyEventHandler(GlobalKeyDown));

        }
        private void ShowGridAnimation()
        {
            // Animație pentru grid (de jos în sus)
            DoubleAnimation moveUp = new DoubleAnimation
            {
                From = 200, // Poziția inițială (jos)
                To = 0,     // Poziția finală (sus)
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            GridTransform.BeginAnimation(TranslateTransform.YProperty, moveUp);

            // Fade-in pentru overlay
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
            // Animație pentru grid (de sus în jos)
            DoubleAnimation moveDown = new DoubleAnimation
            {
                From = 0,
                To = 200,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            moveDown.Completed += (s, e) => Memory_Grid.Visibility = Visibility.Hidden;
            GridTransform.BeginAnimation(TranslateTransform.YProperty, moveDown);

            // Fade-out pentru overlay
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
            // Ascundem grid-ul doar dacă este vizibil și dacă apăsăm în afara lui
            if (isGridVisible && !Memory_Grid.IsMouseOver)
            {
                HideGridAnimation();
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        private void TextBlock_Numere_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private bool IsNumeric(string text)
        {
            double result;
            return double.TryParse(text, out result);
        }
        private void Button_0_Click(object sender, RoutedEventArgs e)
        {
            if (lastOp=='0')
            {
                (DataContext as Number).sum=0;
                TextBlock_Suma.Visibility=Visibility.Hidden;
            }
          
                Nr_crt= Nr_crt*10;
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            if (lastOp=='0')
            {
                (DataContext as Number).sum=0;
                TextBlock_Suma.Visibility=Visibility.Hidden;
            }
            if (isComaActive)
            {
                Nr_crt+=1 *mpy;
                mpy/=10;
            }
            else
            {
                Nr_crt= Nr_crt*10+1;
            }
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            if (lastOp == '0')
            {
                (DataContext as Number).sum = 0;
                TextBlock_Suma.Visibility = Visibility.Hidden;
            }
            if (isComaActive)
            {
                Nr_crt+=2 *mpy;
                mpy/=10;
            }
            else
            {
                Nr_crt= Nr_crt*10+2;
            }
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            if (lastOp == '0')
            {
                (DataContext as Number).sum = 0;
                TextBlock_Suma.Visibility = Visibility.Hidden;
            }
            if (isComaActive)
            {
                Nr_crt+=3 *mpy;
                mpy/=10;
            }
            else
            {
                Nr_crt= Nr_crt*10+3;
            }
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //button 4
            if (lastOp=='0')
            {
                (DataContext as Number).sum=0;
                TextBlock_Suma.Visibility=Visibility.Hidden;
            }
            if (isComaActive)
            {
                Nr_crt+=4 *mpy;
                mpy/=10;
            }
            else
            {
            Nr_crt= Nr_crt*10+4;
            }
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //button 5
            if (lastOp == '0')
            {
                (DataContext as Number).sum = 0;
                TextBlock_Suma.Visibility = Visibility.Hidden;
            }
            if (isComaActive)
            {
                Nr_crt+=5 *mpy;
                mpy/=10;
            }
            else
            {
                Nr_crt= Nr_crt*10+5;
            }
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            if (lastOp == '0')
            {
                (DataContext as Number).sum = 0;
                TextBlock_Suma.Visibility = Visibility.Hidden;
            }
            if (isComaActive)
            {
                Nr_crt+=6 *mpy;
                mpy/=10;
            }
            else
            {
                Nr_crt= Nr_crt*10+6;
            }
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_7_Click(object sender, RoutedEventArgs e)
        {
            if (lastOp == '0')
            {
                (DataContext as Number).sum = 0;
                TextBlock_Suma.Visibility = Visibility.Hidden;
            }
            if (isComaActive)
            {
                Nr_crt+=7 *mpy;
                mpy/=10;
            }
            else
            {
                Nr_crt= Nr_crt*10+7;
            }
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_8_Click(object sender, RoutedEventArgs e)
        {
            if (lastOp=='0')
            {
                (DataContext as Number).sum=0;
                TextBlock_Suma.Visibility=Visibility.Hidden;
            }
            if (isComaActive)
            {
                Nr_crt+=8*mpy;
                mpy/=10;
            }
            else
            {
                Nr_crt= Nr_crt*10+8;
            }
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_9_Click(object sender, RoutedEventArgs e)
        {
            if (lastOp=='0')
            {
                (DataContext as Number).sum=0;
                TextBlock_Suma.Visibility=Visibility.Hidden;
            }
            if (isComaActive)
            {
                Nr_crt+=9 *mpy;
                mpy/=10;
            }
            else
            {
                Nr_crt= Nr_crt*10+9;
            }
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_plus_Click(object sender, RoutedEventArgs e)
        {
            switch (lastOp)
            {
                case '0':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '+':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '-':
                    (DataContext as Number).sum=  (DataContext as Number).sum-=Nr_crt;
                    break;
                case '*':
                    (DataContext as Number).sum=  (DataContext as Number).sum*=Nr_crt;
                    break;
                case '/':
                    (DataContext as Number).sum=  (DataContext as Number).sum/=Nr_crt;
                    break;
            }
            lastOp='+';
            (DataContext as Number).ecuation=(DataContext as Number).sum.ToString() +"+";
            Nr_crt=0;
            isMoreThanOnce= false;
            isComaActive= false;
            TextBlock_Suma.Visibility=Visibility.Visible;
        }

        private void Button_Minus_Click(object sender, RoutedEventArgs e)
        {
            switch (lastOp)
            {
                case '0':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '+':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '-':
                    (DataContext as Number).sum=  (DataContext as Number).sum-=Nr_crt;
                    break;
                case '*':
                    (DataContext as Number).sum=  (DataContext as Number).sum*=Nr_crt;
                    break;
                case '/':
                    (DataContext as Number).sum=  (DataContext as Number).sum/=Nr_crt;
                    break;
            }
            lastOp='-';
            (DataContext as Number).ecuation=(DataContext as Number).sum.ToString() +"-";
            Nr_crt=0;
            isMoreThanOnce= false;
            isComaActive= false;
            TextBlock_Suma.Visibility=Visibility.Visible;
        }

        private void Button_Inmultire_Click(object sender, RoutedEventArgs e)
        {
            switch (lastOp)
            {
                case '0':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '+':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '-':
                    (DataContext as Number).sum=  (DataContext as Number).sum-=Nr_crt;
                    break;
                case '*':
                    (DataContext as Number).sum=  (DataContext as Number).sum*=Nr_crt;
                    break;
                case '/':
                    (DataContext as Number).sum=  (DataContext as Number).sum/=Nr_crt;
                    break;
            }
            lastOp='*';
            (DataContext as Number).ecuation=(DataContext as Number).sum.ToString() +"×";
            Nr_crt=0;
            isMoreThanOnce= false;
            isComaActive= false;
            TextBlock_Suma.Visibility=Visibility.Visible;

        }

        private void Button_Impartire_Click(object sender, RoutedEventArgs e)
        {
            switch (lastOp)
            {
                case '0':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '+':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '-':
                    (DataContext as Number).sum=  (DataContext as Number).sum-=Nr_crt;
                    break;
                case '*':
                    (DataContext as Number).sum=  (DataContext as Number).sum*=Nr_crt;
                    break;
                case '/':
                    (DataContext as Number).sum=  (DataContext as Number).sum/=Nr_crt;
                    break;
            }
            lastOp='/';
            (DataContext as Number).ecuation=(DataContext as Number).sum.ToString() +"÷";
            Nr_crt=0;
            isMoreThanOnce= false;
            isComaActive= false;
            TextBlock_Suma.Visibility=Visibility.Visible;
        }
        private void Button_Egal_Click(object sender, RoutedEventArgs e)
        {
            switch (lastOp)
            {
                case '0':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '+':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_crt;
                    break;
                case '-':
                    (DataContext as Number).sum=  (DataContext as Number).sum-=Nr_crt;
                    break;
                case '*':
                    (DataContext as Number).sum=  (DataContext as Number).sum*=Nr_crt;
                    break;
                case '/':
                    (DataContext as Number).sum=  (DataContext as Number).sum/=Nr_crt;
                    break;
            }
            if (!isMoreThanOnce)
            {
            (DataContext as Number).ecuation=(DataContext as Number).ecuation  + Nr_crt.ToString() +"=";
               isMoreThanOnce=true;
                anteLastOp=lastOp;
                Nr_Here = Nr_crt;
            }
            else
            {
                string[] parts = new string[0]; 


                switch (anteLastOp)
            {
                case '0':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_Here;
                    break;
                case '+':
                    (DataContext as Number).sum=  (DataContext as Number).sum+=Nr_Here;
                        parts = (DataContext as Number).ecuation.Split('+');
                        break;
                case '-':
                    (DataContext as Number).sum=  (DataContext as Number).sum-=Nr_Here;
                        parts = (DataContext as Number).ecuation.Split('-');
                        break;
                case '*':
                    (DataContext as Number).sum=  (DataContext as Number).sum*=Nr_Here;
                        parts = (DataContext as Number).ecuation.Split('×');
                        break;
                case '/':
                    (DataContext as Number).sum=  (DataContext as Number).sum/=Nr_Here;
                        parts = (DataContext as Number).ecuation.Split('÷');
                        break;
            }
                if (parts.Length > 1)
                {
                    (DataContext as Number).ecuation = (DataContext as Number).sum.ToString() +anteLastOp+ parts[1];
                    parts = new string[0];
                }
            }

            lastOp='0';
            (DataContext as Number).Nr_crt=(DataContext as Number).sum;
            Nr_crt=0;
            isComaActive=false;
            TextBlock_Suma.Visibility=Visibility.Visible;
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
        }

        private void Button_CE_Click(object sender, RoutedEventArgs e)
        {
            Nr_crt=0;
            isMoreThanOnce= false;
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_procent_Click(object sender, RoutedEventArgs e)
        {
            double aux;
            aux = ((DataContext as Number).sum * Nr_crt)/100;
            Nr_crt = aux;
            isMoreThanOnce= false;
            (DataContext as Number).ecuation= (DataContext as Number).ecuation + Nr_crt.ToString();
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_xPatrat_Click(object sender, RoutedEventArgs e)
        {
            double aux = Nr_crt;
            Nr_crt*=Nr_crt;
            isMoreThanOnce= false;
            TextBlock_Suma.Visibility=Visibility.Visible;
            (DataContext as Number).ecuation= (DataContext as Number).ecuation +"sqrt(" +aux+")";
            (DataContext as Number).Nr_crt=Nr_crt;
        }

        private void Button_Radical_Click(object sender, RoutedEventArgs e)
        {
            double aux = Nr_crt;
            Nr_crt=Math.Sqrt(Nr_crt);
            isMoreThanOnce= false;
            TextBlock_Suma.Visibility=Visibility.Visible;
            (DataContext as Number).ecuation= (DataContext as Number).ecuation +"√" +aux;
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
                Memory_Grid.Visibility=Visibility.Visible;
            }
        }
    }
}