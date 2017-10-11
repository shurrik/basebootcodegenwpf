using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data;
using basebootcodegenwpf.Model;
using System.Collections.ObjectModel;

namespace basebootcodegenwpf
{

    /// <summary> 
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ObservableCollection<ProItem> items = new ObservableCollection<ProItem>()
        //    {
        //        new CalendarItem(3, "Work"),
        //        new CalendarItem(2, "travel"),
        //        new CalendarItem(1, "vacation"),
        //        new CalendarItem(6, "Fishing")
        //    };

        ObservableCollection<ProItem> items = new ObservableCollection<ProItem>();


        public MainWindow()
        {
            InitializeComponent();

            dataGrid1.ItemsSource = items;

            proType.ItemsSource = ProType.types;
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DayChanged(object sender, SelectionChangedEventArgs e)
        {
            var d = (sender as ComboBox).SelectedItem;
        }

        private void GenJson_Click(object sender, RoutedEventArgs e)
        {
            //foreach (ProItem item in items)
            //{
            //    Console.WriteLine(item.Name + "_" + item.ProType.Name + "_" + item.Length + "_" + item.AllowNull + "_" + item.Remark);
            //}

            String str = "";
            str += "[\n";
            str += "    {\n";
            String className = this.className.Text;
            String classRemark = this.classRemark.Text;
            str += "        \"" + className + "#" + classRemark + "\":\n";
            str += "            {\n";
            //foreach (ProItem item in items)
            //{
            //    String name = item.Name;
            //    String protoType = item.ProType.Name;
            //    int lenghth = item.Length;
            //    String allowNull = item.AllowNull?"1":"0";
            //    String remark = item.Remark;
            //    str += "                \"" + name + "\":\"" + protoType + "#" + lenghth + "#0#" + allowNull + "#" + remark + "\",\n";


            //}

            for(int i=0;i<items.Count;i++)
            {
                ProItem item = items[i];
                String name = item.Name;
                String protoType = item.ProType.Name;
                int lenghth = item.Length;
                String allowNull = item.AllowNull ? "1" : "0";
                String remark = item.Remark;
                str += "                \"" + name + "\":\"" + protoType + "#" + lenghth + "#0#" + allowNull + "#" + remark + "\"";
                if(i==items.Count-1)
                {
                    str +="\n";   
                }
                else{
                    str += ",\n";
                }
            }

            str += "            }\n";
            str += "    }\n";
            str += "]\n";
            Console.WriteLine(str);

        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            ProItem newItem = new ProItem("",1,10,false,false,"");

            //items.RemoveAt(1);
            items.Add(newItem);
            //dataGrid1.Items.Add(newItem);


        }

    }
    class ProItem
    {
        public String Name { get; set; }
        public ProType ProType { get; set; }
        public int Length { get; set; }
        public Boolean IsPk { get; set; }
        public Boolean AllowNull { get; set; }
        public String Remark { get; set; }

        public ProItem(String name, int proType, int length, Boolean isPk, Boolean allowNull, String remark)
        {
            Name = name;
            ProType = new ProType(proType);
            Length = length;
            IsPk = isPk;
            AllowNull = allowNull;
            Remark = remark;
        }
    }

    public class ProType
    {
        public static readonly List<string> types = new List<string>() { "String","Integer","Date"};

        public int Id { get; set; }
        public string Name { get; set; }

        public ProType(int i)
        { Id = i; Name = types[i - 1]; }
    }
}
