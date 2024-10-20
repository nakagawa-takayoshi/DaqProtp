using System.Windows;
using System.Windows.Documents;

namespace DaqProto.MonitorTool.View.NumericalListViewers.ListViewers.Adorners;

/// <summary>
/// 装飾レイヤークラス
/// </summary>
internal class ListItemElementAdornerLayer
{
    /// <summary>
    /// UIの装飾
    /// </summary>
    private readonly Adorner _adorner;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ListItemElementAdornerLayer(Adorner adorner)
    {
        _adorner = adorner;
    }

    /// <summary>
    /// レイヤーに追加
    /// </summary>
    public void Add(UIElement uIElement)
    {
        var layer = AdornerLayer.GetAdornerLayer(uIElement);
        if (layer is null) return;

        var adorners = layer.GetAdorners(uIElement);
        if (adorners is not null)
        {
            if (adorners.Contains(_adorner)) return;
        }

        layer.Add(_adorner);
    }

    /// <summary>
    /// レイヤーから削除
    /// </summary>
    public void Remove(UIElement uIElement)
    {
        var layer = AdornerLayer.GetAdornerLayer(uIElement);
        if (layer is null) return;

        var adorners = layer.GetAdorners(uIElement);
        if (adorners is not null)
        {
            if (!adorners.Contains(_adorner)) return;
        }

        layer.Remove(_adorner);
    }
}
