using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Contrtol.Extentensions;
using R3;

namespace DaqProto.MonitorTool.View.NumericalListViewers.ListViewers.Adorners;

public class ListItemElementHighLightAdorner : Adorner
{
    CompositeDisposable _disposables = new CompositeDisposable();

    private Border _border;

    private readonly ListItemElementAdornerLayer _adornerLayer;

    protected override int VisualChildrenCount => 1;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ListItemElementHighLightAdorner(FrameworkElement adornedElement) : base(adornedElement)
    {
        _border = new Border()
        {
            BorderBrush = Brushes.Red,
            BorderThickness = new Thickness(2),
        };

        _adornerLayer = new ListItemElementAdornerLayer(this);
        Visible(false);

        adornedElement.LoadedAsObservable().Subscribe(_ =>
        {
            _adornerLayer.Add(adornedElement);
        }).AddTo(_disposables);

        adornedElement.UnloadedAsObservable().Subscribe(_ =>
        {
            _adornerLayer.Remove(adornedElement);
        }).AddTo(_disposables);
    }

    public void Visible(bool visible = true)
    {
        this.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        base.ArrangeOverride(finalSize);
        var borderRect = new Rect(finalSize);
        borderRect.Inflate(-1, -1);
        _border.Arrange(new Rect(finalSize));

        return finalSize;
    }

    protected override Visual GetVisualChild(int _)
    {
        return _border;
    }

}
