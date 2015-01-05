using System;
using System.Windows;
using MassTransit;
using MassTransitTestModel;

namespace MassTransitConsumer
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
                sbc.Subscribe(subs =>
                {
                    subs.Handler(new Action<IConsumeContext<MessageOne>, MessageOne>((context, msg) =>
                    {
                        Dispatcher.Invoke(() => ConsumerLog.Text += msg.ID + ":" + msg.Name + "\n");
                    }));
                });
            });
        }
    }
}
