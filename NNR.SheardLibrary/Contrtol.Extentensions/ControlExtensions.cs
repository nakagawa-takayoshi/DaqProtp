using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using R3;

namespace Contrtol.Extentensions;

public static class ControlExtensions
{
    public static Observable<RoutedEventArgs> LoadedAsObservable(this FrameworkElement frameworkElement)
    {
        return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(
            h => (s, e) => h(e),
            h => frameworkElement.Loaded += h,
            h => frameworkElement.Loaded -= h);
    }

    public static Observable<RoutedEventArgs> UnloadedAsObservable(this FrameworkElement frameworkElement)
    {
        return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(
            h => (s, e) => h(e),
            h => frameworkElement.Unloaded += h,
            h => frameworkElement.Unloaded -= h);
    }

    public static Observable<RoutedEventArgs> ClickedAsObservable(this Button button)
    {
        return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(
            h => (s, e) => h(e),
            h => button.Click += h,
            h => button.Click -= h);
    }

    public static Observable<MouseButtonEventArgs> PreviewMouseLeftButtonDownAsObservable(this UIElement uiElement)
    {
        return Observable.FromEvent<MouseButtonEventHandler, MouseButtonEventArgs>(
            h => (s, e) => h(e),
            h => uiElement.PreviewMouseLeftButtonDown += h,
            h => uiElement.PreviewMouseLeftButtonDown -= h);
    }

    public static Observable<MouseEventArgs> PreviewMouseMoveAsObservable(this UIElement uiElement)
    {
        return Observable.FromEvent<MouseEventHandler, MouseEventArgs>(
            h => (s, e) => h(e),
            h => uiElement.PreviewMouseMove += h,
            h => uiElement.PreviewMouseMove -= h);
    }

    public static Observable<DragEventArgs> PreviewDragOverAsObservable(this UIElement uiElement)
    {
        return Observable.FromEvent<DragEventHandler, DragEventArgs>(
            h => (s, e) => h(e),
            h => uiElement.PreviewDragOver += h,
            h => uiElement.PreviewDragOver -= h);
    }

    public static Observable<DragEventArgs> PreviewDropAsObservable(this UIElement uiElement)
    {
        return Observable.FromEvent<DragEventHandler, DragEventArgs>(
            h => (s, e) => h(e),
            h => uiElement.PreviewDrop += h,
            h => uiElement.PreviewDrop -= h);
    }

    public static Observable<DragEventArgs> PreviewDragEnterAsObservable(this UIElement uiElement)
    {
        return Observable.FromEvent<DragEventHandler, DragEventArgs>(
            h => (s, e) => h(e),
            h => uiElement.PreviewDragEnter += h,
            h => uiElement.PreviewDragEnter -= h);
    }

    public static Observable<DragEventArgs> PreviewDragLeaveAsObservable(this UIElement uiElement)
    {
        return Observable.FromEvent<DragEventHandler, DragEventArgs>(
            h => (s, e) => h(e),
            h => uiElement.PreviewDragLeave += h,
            h => uiElement.PreviewDragLeave -= h);
    }

    public static Observable<QueryContinueDragEventArgs> PreviewQueryContinueDragAsObservable(this UIElement uiElement)
    {
        return Observable.FromEvent<QueryContinueDragEventHandler, QueryContinueDragEventArgs>(
            h => (s, e) => h(e),
            h => uiElement.PreviewQueryContinueDrag += h,
            h => uiElement.PreviewQueryContinueDrag -= h);
    }

    public static Observable<QueryContinueDragEventArgs> QueryContinueDragAsObservable(this UIElement uiElement)
    {
        return Observable.FromEvent<QueryContinueDragEventHandler, QueryContinueDragEventArgs>(
            h => (s, e) => h(e),
            h => uiElement.QueryContinueDrag += h,
            h => uiElement.QueryContinueDrag -= h);
    }

    public static Observable<DragEventArgs> DragOverAsObservable(this UIElement uiElement)
    {
        return Observable.FromEvent<DragEventHandler, DragEventArgs>(
            h => (s, e) => h(e),
            h => uiElement.DragOver += h,
            h => uiElement.DragOver -= h);
    }

    public static Observable<DragEventArgs> DragLeaveAsObservable(this UIElement uiElement)
    {
        return Observable.FromEvent<DragEventHandler, DragEventArgs>(
            h => (s, e) => h(e),
            h => uiElement.DragLeave += h,
            h => uiElement.DragLeave -= h);
    }


    public static Observable<SizeChangedEventArgs> SizeChangedAsObservable(this FrameworkElement frameworkElement)
    {
        return Observable.FromEvent<SizeChangedEventHandler, SizeChangedEventArgs>(
            h => (s, e) => h(e),
            h => frameworkElement.SizeChanged += h,
            h => frameworkElement.SizeChanged -= h);
    }

    public static Observable<DragStartedEventArgs> DragStartedAsObservable(this Thumb thumb)
    {
        return Observable.FromEvent<DragStartedEventHandler, DragStartedEventArgs>(
            h => (s, e) => h(e),
            h => thumb.DragStarted += h,
            h => thumb.DragStarted -= h);
    }

    public static Observable<DragDeltaEventArgs> DragDeltaAsObservable(this Thumb thumb)
    {
        return Observable.FromEvent<DragDeltaEventHandler, DragDeltaEventArgs>(
            h => (s, e) => h(e),
            h => thumb.DragDelta += h,
            h => thumb.DragDelta -= h);
    }

    public static Observable<DragCompletedEventArgs> DragCompletedAsObservable(this Thumb thumb)
    {
        return Observable.FromEvent<DragCompletedEventHandler, DragCompletedEventArgs>(
            h => (s, e) => h(e),
            h => thumb.DragCompleted += h,
            h => thumb.DragCompleted -= h);
    }


}

