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

    public class GridItem
    {
        public String name { get; set; }
        public String protoType { get; set; }
        public String lenghth { get; set; }
        public String isPk { get; set; }
        public String allowNull { get; set; }
        public String remark { get; set; }
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable tb;

        /// <summary>
        /// 下拉框数据源
        /// </summary>        
        public ObservableCollection<int> SelectionList
        {
            get { return _selectionList; }
            set { _selectionList = value; }
        }
        private ObservableCollection<int> _selectionList = new ObservableCollection<int>();

        public MainWindow()
        {
            InitializeComponent();

            SelectionList.Add(1);
            SelectionList.Add(2);
            SelectionList.Add(3);

            Banding();


        }

        
        private void Banding()
        {
            tb = new DataTable();
            DataColumn Checked = new DataColumn("Checked");
            DataColumn Name = new DataColumn("Name");
            DataColumn Sex = new DataColumn("Sex");
            DataColumn Age = new DataColumn("Age");
            DataColumn Selection = new DataColumn("Selection", typeof(ObservableCollection<int>));
            tb.Columns.Add(Checked);
            tb.Columns.Add(Name);
            tb.Columns.Add(Sex);
            tb.Columns.Add(Age);
            tb.Columns.Add(Selection); //如果绑定数据不采用依赖属性绑定 采用数据模板添加 Combox 则该下拉框的数据源为Selection 
            for (int i = 0; i < 5; i++)
            {
                DataRow row = tb.NewRow();
                row["Name"] = "AA" + i.ToString();
                if (i % 2 == 0)
                {
                    row["Checked"] = true;
                    row["Sex"] = "男";
                }
                else
                {
                    row["Checked"] = false;
                    row["Sex"] = "女";
                }
                row["Age"] = i.ToString();
                row["Selection"] = SelectionList;
                tb.Rows.Add(row);
            }
            //如果数据源绑定要用DataContent上下文数据绑定，则xmal界面必须指定ItemSource="{Binding}"
            dg_Grid.DataContext = tb;
            
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            DataRow dr = tb.NewRow();
            for (int columIndex = 0; columIndex < tb.Columns.Count; columIndex++)
                dr[columIndex] = "New Row - " + columIndex.ToString();
            tb.Rows.Add(dr);
        }
    }
}
