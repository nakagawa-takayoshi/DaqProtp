using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ModernWpf;
using ModernWpf.Controls;
using R3;

namespace Contrtol.Extentensions
{
    public static class NumberBoxExtensions
    {

        public static Observable<NumberBoxValueChangedEventArgs> ValueChangedAsObservable(this NumberBox numberBox)
        {
            return Observable.FromEvent<TypedEventHandler<NumberBox,NumberBoxValueChangedEventArgs>, NumberBoxValueChangedEventArgs>(
                h => (s, e) => h(e),
                h => numberBox.ValueChanged += h,
                h => numberBox.ValueChanged -= h);
        }


        public static Observable<KeyboardFocusChangedEventArgs> LostKeyboardFocusAsObservable(this NumberBox numberBox)
        {
            return Observable.FromEvent<KeyboardFocusChangedEventHandler, KeyboardFocusChangedEventArgs>(
                h => (s, e) => h(e),
                h => numberBox.LostKeyboardFocus += h,
                h => numberBox.LostKeyboardFocus -= h);
        }

        public static Observable<KeyEventArgs> KeyDownAsObservable(this NumberBox numberBox)
        {
            return Observable.FromEvent<KeyEventHandler, KeyEventArgs>(
                h => (s, e) => h(e),
                h => numberBox.KeyDown += h,
                h => numberBox.KeyDown -= h);
        }

        public static IDisposable ReturnKeyAcceptEnable(this NumberBox numberBox)
        {
            return numberBox.KeyDownAsObservable()
                .Where(e => e.Key == Key.Enter)
                .Subscribe((e) =>
                {
                    Keyboard.ClearFocus();
                });
        }

        public static IDisposable EscapeKeyAcceptEnable(this NumberBox numberBox, Action action)
        {
            return numberBox.KeyDownAsObservable()
                .Where(e => e.Key == Key.Escape)
                .Subscribe((e) =>
                {
                    action();
                    Keyboard.ClearFocus();
                });
        }
    }
}
