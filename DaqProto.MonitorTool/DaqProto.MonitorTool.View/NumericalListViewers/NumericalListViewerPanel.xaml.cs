using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Contrtol.Extentensions;
using DaqProto.MonitorTool.View.NumericalListViewers.ListViewers;
using R3;

namespace DaqProto.MonitorTool.View.NumericalListViewers
{
    /// <summary>
    /// NumericalListViewerPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class NumericalListViewerPanel : UserControl
    {

        private CompositeDisposable _disposables = new CompositeDisposable();


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NumericalListViewerPanel()
        {
            InitializeComponent();
        }

        private void NumericalListViewerPanel_Loaded(object sender, RoutedEventArgs e)
        {

            for(int ii=0;ii<1; ii++)
            {
                var childPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                childPanel.Children.Add(new ListViewRowHeader());
                var panel = new ListItemViewerElement(new ListItemViewPanel(), ii);
                childPanel.Children.Add(panel);

                ParentPanel.Children.Add(childPanel);
            }

            if (ParentPanel.Children.Count < 1) return;

            var childStackPanel = ParentPanel.Children[0] as StackPanel;
            if (childStackPanel is null) return;
            if (childStackPanel.Children.Count < 2) return;

            var rowHeaderPanel = childStackPanel.Children[0] as ListViewRowHeader;
            if (rowHeaderPanel is null) return;

            var itemWidth = ParentPanel.RenderSize.Width - rowHeaderPanel.RenderSize.Width;
            var itemPanel = childStackPanel.Children[1] as ListItemViewerElement;
            if (itemPanel is null) return;

            if (itemWidth < 0.0d) return;

            itemPanel.Width = itemWidth;
        }

        private void NumericalListViewerPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Debug.WriteLine($"SizeChanged - RenderHeight{this.RenderSize.Height}");
            if (ParentPanel.Children.Count < 1) return;

            var childStackPanel = ParentPanel.Children[0] as StackPanel;
            if (childStackPanel is null) return;
            if (childStackPanel.Children.Count < 2) return;

            var rowHeaderPanel = childStackPanel.Children[0] as ListViewRowHeader;
            if (rowHeaderPanel is null) return;
            Debug.WriteLine($"SizeChanged - ChildRenderHeight{rowHeaderPanel.RenderSize.Height}");

            var itemWidth = ParentPanel.RenderSize.Width - rowHeaderPanel.RenderSize.Width;
            var itemPanel = childStackPanel.Children[1] as ListItemViewerElement;
            if (itemPanel is null) return;

            if (itemWidth < 0.0d) return;

            itemPanel.Width = itemWidth;
        }


    }
}
