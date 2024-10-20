using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.TextFormatting;
using Contrtol.Extentensions;
using NNR.Utilities;
using R3;

namespace DaqProto.MonitorTool.View.NumericalListViewers.ListViewers;


/// <summary>
/// ListItemViewPanel.xaml の相互作用ロジック
/// </summary>
public partial class ListItemViewPanel : UserControl
{
    private CompositeDisposable _disposables = new CompositeDisposable();

    private double _maxFontSize = 18.0d;

    public ListItemViewPanel()
    {
        InitializeComponent();
        MesureValueText.Content = "1234567890";

        MesureValueText.SizeChangedAsObservable().Subscribe(_ =>
        {
            var formattedTextHelper = new formattedTextHelper(MesureValueText);
            var actualWidth = MesureValueText.ActualWidth;
            var fontSize = MesureValueText.FontSize;
            var textSize = formattedTextHelper.GetFormatTextSize((string)MesureValueText.Content, fontSize);
            if (textSize.Width < actualWidth)
            {
                AdjustLargeFontSize(formattedTextHelper, textSize);
                return;
            }

            if (actualWidth < textSize.Width) 
            {
                if (fontSize <= 8.0d) return;

                var newFontSize = fontSize;
                while (actualWidth  < textSize.Width)
                {
                    if (newFontSize <= 8.0d) break;

                    newFontSize -= 1.0d;
                    textSize = formattedTextHelper.GetFormatTextSize((string)MesureValueText.Content, newFontSize);
                }

                MesureValueText.FontSize = newFontSize;
            }
        }).AddTo(_disposables);
    }

    private void AdjustLargeFontSize(formattedTextHelper formattedTextHelper, Size originalTextSize)
    {
        var fontSize = MesureValueText.FontSize;
        var actualWidth = MesureValueText.ActualWidth;
        if (_maxFontSize <= fontSize) return;

        var newFontSize = fontSize;
        var adjustTextSize = originalTextSize;
        while (adjustTextSize.Width < actualWidth)
        {
            if (_maxFontSize <= newFontSize) break;

            newFontSize += 1.0d;
            adjustTextSize = formattedTextHelper.GetFormatTextSize((string)MesureValueText.Content, newFontSize);
        }

        MesureValueText.FontSize = newFontSize - 1.0d;
    }
}
