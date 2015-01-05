using System.Windows;
using MassTransit;
using MassTransitTestModel;

namespace MassTransitPublisher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Bus.Initialize(sbc =>
                           {
                               sbc.UseRabbitMq();
                               sbc.ReceiveFrom("rabbitmq://localhost/test_queue");
                           });
        }

        private int _lastId = 0;
        private void OnPublish(object sender, RoutedEventArgs e)
        {
            Bus.Instance.Publish(new MessageOne {ID = ++_lastId, Name = NameVal.Text});
            NameVal.Text = "";
        }
    }
}
