using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Contrtol.Extentensions;
using DaqProto.MonitorTool.View.NumericalListViewers.ListViewers.Adorners;
using R3;

namespace DaqProto.MonitorTool.View.NumericalListViewers.ListViewers;

internal class ListItemDraggableControlProvider
{
    private ListItemElementGhostAdorner? _ghostAdorner;

    private CompositeDisposable _disposables = new CompositeDisposable();

    public void Register(ListItemViewerElement element)
    {
        element.DragStarted.Subscribe(_ =>
        {
            if (_ghostAdorner is not null) return;

            _ghostAdorner = new ListItemElementGhostAdorner(element);
            _ghostAdorner.BeginRender();
        }).AddTo(_disposables);

        element.DragDelta.Subscribe(_ =>
        {
            if (_ghostAdorner is null) return;

            //_ghostAdorner.InvalidateVisual();
        }).AddTo(_disposables);

        element.DragCompleted.Subscribe(_ =>
        {
            if (_ghostAdorner is null) return;
            _ghostAdorner.EndRender();

            _ghostAdorner = null;
        }).AddTo(_disposables);

        element.UnloadedAsObservable().Subscribe(_ =>
        {
            _disposables.Dispose();
        }).AddTo(_disposables);
    }
}
