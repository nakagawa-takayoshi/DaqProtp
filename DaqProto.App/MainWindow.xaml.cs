using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Contrtol.Extentensions;
using DaqProto.MonitorTool.View.NumericalListViewers;
using R3;

namespace DaqProto.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public MainWindow()
        {
            InitializeComponent();

            this.LoadedAsObservable().Subscribe(_ =>
            {
                MonitorFrame.Navigate(new NumericalListViewerPanel());
            }).AddTo(_disposables);
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Disposable.Dispose();
        }
    }
}