using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using R3;

namespace Contrtol.Extentensions
{
    public static class RadioButtonExtensions
    {
        public static Observable<RoutedEventArgs> ChackedAsObservable(this RadioButton radioButton)
        {
            return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(
                h => (s, e) => h(e),
                h => radioButton.Checked += h,
                h => radioButton.Checked -= h);
        }

        public static Observable<RoutedEventArgs> UncheckedAsObservable(this RadioButton radioButton)
        {
            return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(
                h => (s, e) => h(e),
                h => radioButton.Unchecked += h,
                h => radioButton.Unchecked -= h);
        }
    }
}
