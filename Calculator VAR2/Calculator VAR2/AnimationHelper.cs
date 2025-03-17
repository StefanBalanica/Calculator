using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace Calculator_VAR2
{
    internal class AnimationHelper
    {
        public static void ShowGridAnimation(UIElement element, TranslateTransform transform)
        {
            DoubleAnimation moveUp = new DoubleAnimation(200, 0, TimeSpan.FromSeconds(0.3))
            {
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            transform.BeginAnimation(TranslateTransform.YProperty, moveUp);
            element.Visibility = Visibility.Visible;
        }

        public static void HideGridAnimation(UIElement element, TranslateTransform transform)
        {
            DoubleAnimation moveDown = new DoubleAnimation(0, 200, TimeSpan.FromSeconds(1.3))
            {
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            transform.BeginAnimation(TranslateTransform.YProperty, moveDown);
            moveDown.Completed += (s, e) => element.Visibility = Visibility.Hidden;
        }
    }
}
