using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Contrtol.Extentensions;
using R3;

namespace DaqProto.MonitorTool.View.NumericalListViewers.ListViewers.Adorners;

/// <summary>
/// ドラッグアンドドロップの装飾クラス
/// </summary>
internal class ListItemDraggableAdorner : Adorner
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private readonly Thumb _thumb;

    private ListItemElementAdornerLayer _adornerLayer;

    private ListItemElementGhostAdorner? _ghostAdorner;

    public bool CanDrag { get; private set; } = true;

    public bool Dragging { get; set; } = false;

    public Observable<DragStartedEventArgs> DragStarted { get; }

    public Observable<DragEventArgs> DragOvered { get; }

    public Observable<QueryContinueDragEventArgs> DragDelta { get; }

    public Observable<DragEventArgs> DragEntered { get; }

    public Observable<DragEventArgs> DragLeaved { get; }

    public Observable<DragEventArgs> Dropped { get; }

    protected override int VisualChildrenCount => 1;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ListItemDraggableAdorner(FrameworkElement adornedElement) : base(adornedElement)
    {
        _thumb = new Thumb{ Opacity = 0, Visibility = Visibility.Visible, AllowDrop = true };
        AddVisualChild(_thumb);

        IsHitTestVisible = true;
        Visibility = Visibility.Visible;
        _adornerLayer = new ListItemElementAdornerLayer(this);

        DragStarted = _thumb.DragStartedAsObservable().Where(_ => !CanDrag && Dragging).AsObservable();
        DragStarted.Subscribe(_ => DoDragDrop(adornedElement));

        DragOvered = adornedElement.DragOverAsObservable().AsObservable();

        DragDelta = adornedElement.QueryContinueDragAsObservable();
        DragDelta.Subscribe(_ =>
        {
            if (_ghostAdorner is null) return;
            _ghostAdorner.InvalidateVisual();
        }).AddTo(_disposables);

        DragEntered = adornedElement.PreviewDragEnterAsObservable();

        DragLeaved = adornedElement.PreviewDragLeaveAsObservable();

        Dropped = adornedElement.PreviewDropAsObservable().Where(_=> CanDrag && !Dragging).AsObservable();
        Dropped.Subscribe(_ =>
        {
            Debug.WriteLine(@"Dropped");
        }).AddTo(_disposables);


        adornedElement.LoadedAsObservable().Subscribe(_ =>
        {
            _adornerLayer.Add(adornedElement);
        }).AddTo(_disposables);

        adornedElement.UnloadedAsObservable().Subscribe(_ =>
        {
            _adornerLayer.Remove(adornedElement);
        }).AddTo(_disposables);
    }

    private void ClearDragging()
    {
        CanDrag = true;
        Dragging = false;
    }

    private void DoDragDrop(FrameworkElement adornedElement)
    {
        _ghostAdorner = new ListItemElementGhostAdorner(adornedElement);
        _ghostAdorner.BeginRender();

        Dispatcher.BeginInvoke(() =>
        {
            adornedElement.CaptureMouse();
            DragDrop.DoDragDrop(adornedElement, new DataObject("ListItemElement", adornedElement), DragDropEffects.Move);
            adornedElement.ReleaseMouseCapture();
            Dragging = false;
            _ghostAdorner.EndRender();
            _ghostAdorner = null;
            ClearDragging();
        });

    }


    protected override Size ArrangeOverride(Size finalSize)
    {
        _thumb.Arrange(new Rect(finalSize));
        return base.ArrangeOverride(finalSize);
    }


    protected override Visual GetVisualChild(int _)
    {
        return _thumb;
    }

    protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        if (!CanDrag) return;

        CanDrag = false;
        Dragging = false;
    }


    protected override void OnPreviewMouseMove(MouseEventArgs e)
    {
        base.OnPreviewMouseMove(e);
        if (CanDrag) return;
        if (Dragging) return;

        e.Handled = true;
        Dragging = true;
    }

}
