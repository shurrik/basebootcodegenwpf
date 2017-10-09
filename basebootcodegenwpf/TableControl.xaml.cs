using System;
using System.Collections.Generic;
using System.Data;
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

namespace UnityApp.Unity
{
    /// <summary>
    /// TableControl.xaml 的交互逻辑
    /// </summary>
    public partial class TableControl : UserControl
    {
        private DataTable _dt = new DataTable();
        public TableControl()
        {
            InitializeComponent();
            _dt.Columns.Add(new DataColumn("ParamKey", typeof(string)));
            _dt.Columns.Add(new DataColumn("ParamValue", typeof(string)));
            this.dgData.ItemsSource = null;
            this.dgData.ItemsSource = _dt.DefaultView;
        }


        #region 自定义依赖项属性

        /// <summary>
        /// 数据源
        /// </summary>
        public DataTable DataSource
        {
            get { return ((DataView)this.dgData.ItemsSource).Table; }
            set { SetValue(DataSourceProperty, value); }
        }

        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(DataTable), typeof(TableControl), new PropertyMetadata(new DataTable(), DataSourceChanged));


        private static void DataSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TableControl control = d as TableControl;
            if (e.NewValue != e.OldValue)
            {
                DataTable dt = e.NewValue as DataTable;
                control._dt = dt;
                control.dgData.ItemsSource = null;
                control.dgData.ItemsSource = control._dt.DefaultView;
            }
        }

        /// <summary>
        /// 是否编辑状态
        /// </summary>
        public bool IsEdit
        {
            get { return (bool)GetValue(IsEditProperty); }
            set { SetValue(IsEditProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEdit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEditProperty =
            DependencyProperty.Register("IsEdit", typeof(bool), typeof(TableControl), new PropertyMetadata(true, IsEditChanged));

        private static void IsEditChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TableControl control = d as TableControl;
            bool isEdit = Convert.ToBoolean(e.NewValue);
            if (!isEdit)
            {
                int len = control.dgData.Columns.Count;
                control.dgData.Columns[len - 1].Visibility = Visibility.Collapsed;
                control.dgData.IsReadOnly = true;
                control.btnAdd.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            ((DataRowView)this.dgData.SelectedItem).Row.Delete();
            _dt.Rows.Remove(((DataRowView)this.dgData.SelectedItem).Row);
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _dt = ((DataView)this.dgData.ItemsSource).Table;
            DataRow dr = _dt.NewRow();
            _dt.Rows.Add(dr);
            this.dgData.ItemsSource = _dt.DefaultView;
        }

    }
}
