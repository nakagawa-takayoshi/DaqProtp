using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaqProto.MonitorTool.View.NumericalListViewers.ListViewers.Adorners;

namespace DaqProto.MonitorTool.View.NumericalListViewers.ListViewers;
internal class ListItemElementGhostViewer
{
    private ListItemElementGhostAdorner? _ghostAdorner;

    public void Add(ListItemViewerElement element)
    {
        if (_ghostAdorner is not null) return;
        
        _ghostAdorner = new ListItemElementGhostAdorner(element);
        _ghostAdorner.BeginRender();
        _ghostAdorner.InvalidateVisual();
    }

    public void Remove()
    {
        if (_ghostAdorner is null) return;

        _ghostAdorner.EndRender();
        _ghostAdorner = null;
    }
}
