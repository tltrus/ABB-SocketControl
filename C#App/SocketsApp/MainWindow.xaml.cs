using System;
using System.Windows;
using System.Windows.Controls;

namespace ABBSockets
{
    public partial class MainWindow : Window
    {
        ABBRobot Robot;

        public MainWindow()
        {
            InitializeComponent();
            
            Robot = new ABBRobot();

            btnMove1.IsEnabled = false;
            btnMove2.IsEnabled = false;
            btnMove3.IsEnabled = false;
            btnMoveHome.IsEnabled = false;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            Robot.OpenSocket(tbIp.Text, int.Parse(tbPort.Text));
            rtbLog.AppendText(String.Format("\rC# : {0}", "Press any MOVE button"));

            btnMove1.IsEnabled = true;
            btnMove2.IsEnabled = true;
            btnMove3.IsEnabled = true;
            btnMoveHome.IsEnabled = true;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Robot.CloseSocket();

            btnMove1.IsEnabled = false;
            btnMove2.IsEnabled = false;
            btnMove3.IsEnabled = false;
            btnMoveHome.IsEnabled = false;
        }

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            var btnContent = ((Button)sender).Content;

            switch(btnContent)
            {
                case "MOVE 1":
                    Robot.SendDataToRobot("POS1");
                    break;
                case "MOVE 2":
                    Robot.SendDataToRobot("POS2");
                    break;
                case "MOVE 3":
                    Robot.SendDataToRobot("POS3");
                    break;
                case "MOVE HOME":
                    Robot.SendDataToRobot("HOME");
                    break;
            }

            string message = Robot.ReceiveDataFromRobot();
            rtbLog.AppendText(String.Format("\rC# : {0}", message));
        }
    }
}
