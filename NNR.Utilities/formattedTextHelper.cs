using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NNR.Utilities;

public class formattedTextHelper
{
    private Control _targetControl;

    private Typeface _typeface;

    public formattedTextHelper(Control targetControl)
    {
        _targetControl = targetControl;

        _typeface = new Typeface(
            _targetControl.FontFamily,
            _targetControl.FontStyle,
            _targetControl.FontWeight,
            _targetControl.FontStretch);
    }

    public Size GetFormatTextSize(string text, double fontSize)
    {
        var targetText = $"{text}■";
        FormattedText formattedText = new FormattedText(
            targetText,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            _typeface,
            fontSize,
            Brushes.Black,
            VisualTreeHelper.GetDpi(_targetControl).PixelsPerDip);


        return new Size(formattedText.Width, formattedText.Height);
    }
}
