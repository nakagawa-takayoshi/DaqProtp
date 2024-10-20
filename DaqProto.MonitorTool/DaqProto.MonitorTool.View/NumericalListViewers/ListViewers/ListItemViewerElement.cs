using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Contrtol.Extentensions;
using DaqProto.MonitorTool.View.NumericalListViewers.ListViewers.Adorners;
using R3;

namespace DaqProto.MonitorTool.View.NumericalListViewers.ListViewers;

internal class ListItemViewerElement : ContentPresenter
{
    private CompositeDisposable _disposables = new CompositeDisposable();

    private readonly ListItemElementHighLightAdorner _highLightAdorner;

    private readonly ListItemDraggableAdorner _draggableAdorner;

    public Observable<DragStartedEventArgs> DragStarted => _draggableAdorner.DragStarted;

    public Observable<DragEventArgs> DragCompleted => _draggableAdorner.Dropped;

    public Observable<QueryContinueDragEventArgs> DragDelta => _draggableAdorner.DragDelta;

    public int Count { get; }

    public bool Dragging => _draggableAdorner.Dragging;


    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="element"></param>
    /// <param name="count"></param>
    public ListItemViewerElement(FrameworkElement element,int count)
    {
        Content = element;
        Count = count;
        AllowDrop = true;
        IsHitTestVisible = true;
        _highLightAdorner = new ListItemElementHighLightAdorner(element);
        _draggableAdorner = new ListItemDraggableAdorner(element);

        _draggableAdorner.DragStarted.Subscribe(_=>
        {
            Debug.WriteLine("DragStarted");
        }).AddTo(_disposables);

        _draggableAdorner.DragEntered.Subscribe(e =>
        {
            Debug.WriteLine(@"DragEntered");
            if (_draggableAdorner.Dragging) return;
            Debug.WriteLine(@"DragEntered_Visible");
            _highLightAdorner.Visible();
        }).AddTo(_disposables);

        _draggableAdorner.DragLeaved.Subscribe(e =>
        {
            Debug.WriteLine(@"DragLeaved");
            if (_draggableAdorner.Dragging) return;
            _highLightAdorner.Visible(false);
        }).AddTo(_disposables);

        _draggableAdorner.Dropped.Subscribe(e =>
        {
            Debug.WriteLine(@"Dropped");
            if (_draggableAdorner.Dragging) return;
            _highLightAdorner.Visible(false);
        }).AddTo(_disposables);
    }

    protected override void OnDragEnter(DragEventArgs e)
    {
        base.OnDragEnter(e);

        Debug.WriteLine(@"DragEntered");
        if (_draggableAdorner.Dragging) return;
        Debug.WriteLine(@"DragEntered_Visible");
        _highLightAdorner.Visible();
    }

    protected override void OnDragLeave(DragEventArgs e)
    {
        base.OnDragLeave(e);
        Debug.WriteLine(@"DragLeaved");
        if (_draggableAdorner.Dragging) return;
        _highLightAdorner.Visible(false);
    }

    protected override void OnDrop(DragEventArgs e)
    {
        base.OnDrop(e);
        Debug.WriteLine(@"Dropped");
        if (_draggableAdorner.Dragging) return;
        _highLightAdorner.Visible(false);
    }
}
