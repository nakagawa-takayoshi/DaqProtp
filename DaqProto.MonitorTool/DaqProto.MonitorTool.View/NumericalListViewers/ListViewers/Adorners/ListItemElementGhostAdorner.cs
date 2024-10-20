using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Contrtol.Extentensions;
using R3;

namespace DaqProto.MonitorTool.View.NumericalListViewers.ListViewers.Adorners;

internal class ListItemElementGhostAdorner : Adorner
{
    [DllImport("user32.dll")]
    internal static extern bool GetCursorPos(out PointStructure pt);

    [StructLayout(LayoutKind.Sequential)]
    internal struct PointStructure
    {
        public Int32 X;
        public Int32 Y;
    };

    private bool _render = false;

    private Point _offsetPoint;

    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private readonly ListItemElementAdornerLayer _adornerLayer;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ListItemElementGhostAdorner(FrameworkElement adornedElement) : base(adornedElement)
    {
        _adornerLayer = new ListItemElementAdornerLayer(this);
        _offsetPoint = new Point(0, 0);

    }

    public void BeginRender()
    {
        Visibility = Visibility.Visible;
        var point =  AdornedElement.PointFromScreen(GetMousePoint());
        _offsetPoint = new Point(-point.X, -point.Y);
        _render = true;
        Debug.WriteLine($"BeginRender: {_offsetPoint.X}, {_offsetPoint.Y}");
        _adornerLayer.Add(AdornedElement);
    }

    public void EndRender()
    {
        Visibility = Visibility.Hidden;
        _render = false;
        _adornerLayer.Remove(AdornedElement);
    }

    /// <summary>
    /// 描画処理
    /// </summary>
    /// <param name="drawingContext"></param>
    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);
        if (!_render) return;

        var currentPoint = AdornedElement.PointFromScreen(GetMousePoint());
        Debug.WriteLine($"OnRender: {currentPoint.X}, {currentPoint.Y}");
        var renderPoint = new Point(currentPoint.X + _offsetPoint.X, currentPoint.Y + _offsetPoint.Y);
        var rect =  new Rect(renderPoint, this.AdornedElement.RenderSize);
        var brush = new VisualBrush(AdornedElement)
        {
            Opacity = 0.5d,
        };

        drawingContext.DrawRectangle(brush, null, rect);
    }

    private Point GetMousePoint()
    {
        GetCursorPos(out PointStructure point);

        return new Point(point.X, point.Y);
    }
}
