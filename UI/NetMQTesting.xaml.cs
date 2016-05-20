using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NetMQ;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for NetMQTesting.xaml
    /// </summary>
    public partial class NetMQTesting : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static IList<string> allowableCommandLineArgs = new[] { "TopicA", "TopicB", "All" };
        private ObservableCollection<string> m_MessageList;

        public NetMQTesting()
        {            
            this.m_MessageList = new ObservableCollection<string>();
            this.m_MessageList.Add("hello");
            InitializeComponent();
            this.DataContext = this;
        }

        public ObservableCollection<string> MessageList
        {
            get { return this.m_MessageList; }
        }

        private void StartPublisher()
        {
            Random rand = new Random(50);

            using (var pubSocket = new NetMQ.Sockets.PublisherSocket())
            {
                this.LogIt("Publisher socket binding...");
                pubSocket.Options.SendHighWatermark = 1000;
                pubSocket.Bind("tcp://10.1.1.96:12345");

                for (var i = 0; i < 200; i++)
                {
                    var msg = "TopicA msg-" + i;
                    this.LogIt("Sending message : " + msg);
                    pubSocket.SendMoreFrame("Hello").SendFrame(msg);
                    System.Threading.Thread.Sleep(500);
                }
            }
        }

        private void LogIt(string message)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.MessageList.Insert(0, message);
            }
            ));
        }

        private void StartSubscriber()
        {
            string topic = allowableCommandLineArgs[0];
            this.LogIt("Subscriber started for Topic : " + topic);

            using (var subSocket = new NetMQ.Sockets.SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = 1000;
                subSocket.Connect("tcp://10.1.1.96:12345");
                subSocket.Subscribe("Hello");
                this.LogIt("Subscriber socket connecting...");
                while (true)
                {
                    string messageTopicReceived = subSocket.ReceiveFrameString();
                    string messageReceived = subSocket.ReceiveFrameString();
                    this.LogIt(messageReceived);
                }
            }        
        }

        private void ButtonStartPublish_Click(object sender, RoutedEventArgs e)
        {
            this.StartPublisher();
        }

        private void ButtonStartSubscribe_Click(object sender, RoutedEventArgs e)
        {
            this.StartSubscriber();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
