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

namespace Ychebnaya_praktika
{
    /// <summary>
    /// Логика взаимодействия для Select_a_table.xaml
    /// </summary>
    public partial class Select_a_table : Window
    {
        public Select_a_table()
        {
            InitializeComponent();
        }
        private void go_to_receipt_invoice_Click(object sender, RoutedEventArgs e)
        {
            Receipt_invoice_window riw = new Receipt_invoice_window();
            this.Hide();
            riw.Show();
        }

        private void go_to_warehouse_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
