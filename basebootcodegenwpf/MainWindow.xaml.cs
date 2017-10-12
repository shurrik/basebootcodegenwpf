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
using System.IO;

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

            String str = "";
            str += "[\n";
            str += "    {\n";
            String className = this.className.Text;
            String classRemark = this.classRemark.Text;
            str += "        \"" + className + "#" + classRemark + "\":\n";
            str += "            {\n";

            for(int i=0;i<items.Count;i++)
            {
                ProItem item = items[i];
                String name = item.Name;
                String protoType = item.ProType;
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
            createJsonFile(str);

        }

        private void createJsonFile(String str)
        {

            if (!File.Exists("entity.json"))
            {
                FileStream fs1 = new FileStream("entity.json", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(str);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream("entity.json", FileMode.Truncate, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(str);//开始写入值
                sr.Close();
                fs.Close();

            }
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            ProItem newItem = new ProItem("","String",10,false,false,"");

            items.Add(newItem);
        }

        private void DelData_Click(object sender, RoutedEventArgs e)
        {

            Console.WriteLine("delclick");
            if (dataGrid1.SelectedItem != null)
            {
                ProItem DRV = (ProItem)dataGrid1.SelectedItem;

                MessageBoxResult result = MessageBox.Show("确定要删除属性？", "提示", MessageBoxButton.YesNo);//弹出删除对话框
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        items.Remove(DRV);
                        MessageBox.Show("删除成功！");
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            else
            {
                MessageBox.Show("请选择属性！");
            }
        }

    }
    class ProItem
    {
        public String Name { get; set; }
        public String ProType { get; set; }
        public int Length { get; set; }
        public Boolean IsPk { get; set; }
        public Boolean AllowNull { get; set; }
        public String Remark { get; set; }

        public ProItem(String name, String proType, int length, Boolean isPk, Boolean allowNull, String remark)
        {
            Name = name;
            ProType = proType;
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
