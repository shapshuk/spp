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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Browser.ViewModel;
using Assembly_BrowserLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Browser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataViewModel data = new DataViewModel();
            //string result = JsonConvert.SerializeObject(data.Types);
            //var token = JToken.Parse(result);

            //var children = new List<JToken>();
            //if (token != null)
            //{
            //    children.Add(token);
            //}

            //treeView1.ItemsSource = null;
            //treeView1.Items.Clear();
            //treeView1.ItemsSource = children[0];
            treeView1.ItemsSource = data.Types;
        }
    }
}
