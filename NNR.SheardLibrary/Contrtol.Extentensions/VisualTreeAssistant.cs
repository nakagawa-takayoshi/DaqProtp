using System.Windows;
using System.Windows.Media;

namespace Contrtol.Extentensions;

public static class VisualTreeAssistant
{
    public static T? FindChild<T>(DependencyObject parent, string childName) where T : FrameworkElement
    {
        if (parent is null) return null;

        var count =  VisualTreeHelper.GetChildrenCount(parent);
        for (var i = 0; i < count; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
            if (child is not T targetPanel) continue;
            if (targetPanel.Name.Equals(childName)) return targetPanel;
        }

        return null;
    }

    public static T? FindParent<T>(object source, string name = "") where T : FrameworkElement
    {
        var childObject = source as DependencyObject;
        if (childObject is null) return null;

        var findObject = childObject as FrameworkElement;
        if (findObject is T targetPanel) return targetPanel;

        while (findObject is not null)
        {
            var parent = VisualTreeHelper.GetParent(findObject) as FrameworkElement;
            findObject = parent;
            if (parent is not T findPanel) continue;

            if ((parent.Name.Equals(name))) return findPanel;
        }

        return null;
    }
}
