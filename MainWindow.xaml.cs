using System.ComponentModel;
namespace ProgressBar_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        BackgroundWorker worker = new BackgroundWorker();
        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerSupportsCancellation = true;
            
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    (sender as BackgroundWorker).ReportProgress(i);
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)        {            pbStatus.Value = e.ProgressPercentage;        }

        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)        {            worker.RunWorkerAsync();        }

        private void button2_Click(object sender, System.Windows.RoutedEventArgs e)        {            worker.CancelAsync();        }

    }
}
