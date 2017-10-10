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
        public ObservableCollection<String> SelectionList
        {
            get { return _selectionList; }
            set { _selectionList = value; }
        }
        private ObservableCollection<String> _selectionList = new ObservableCollection<String>();

        public MainWindow()
        {
            InitializeComponent();

            SelectionList.Add("String");
            SelectionList.Add("Integer");
            SelectionList.Add("Float");
            SelectionList.Add("Long");
            SelectionList.Add("Double");
            SelectionList.Add("BigDecimal");
            SelectionList.Add("Boolean");
            SelectionList.Add("Date");

            Banding();
        }

        
        private void Banding()
        {
            tb = new DataTable();
            DataColumn name = new DataColumn("name");
            DataColumn protoType = new DataColumn("protoType", typeof(ObservableCollection<String>)); ;
            DataColumn lenghth = new DataColumn("lenghth");
            DataColumn isPk = new DataColumn("isPk");
            DataColumn allowNull = new DataColumn("allowNull");
            DataColumn remark = new DataColumn("remark");
            tb.Columns.Add(name);
            tb.Columns.Add(protoType);
            tb.Columns.Add(lenghth);
            //tb.Columns.Add(isPk);
            tb.Columns.Add(allowNull);
            tb.Columns.Add(remark);
            dg_Grid.DataContext = tb;
            
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = tb.NewRow();

            row["protoType"] = SelectionList;
            row["allowNull"] = false;
            tb.Rows.Add(row);
        }

        private void DelData_Click(object sender, RoutedEventArgs e)
        {
            if (dg_Grid.SelectedItem != null)
            {
                DataRowView DRV = (DataRowView)dg_Grid.SelectedItem;
                string Name = DRV.Row[0].ToString();//获取选中行的name列内容

                MessageBoxResult result = MessageBox.Show("确定要删除属性？", "提示", MessageBoxButton.YesNo);//弹出删除对话框
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        DRV.Delete();//删除行
                        MessageBox.Show("删除成功！");
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            else {
                MessageBox.Show("请选择属性！");
            }
        }

        private void GenJson_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
