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
using Ychebnaya_praktika.Models;


namespace Ychebnaya_praktika
{
    /// <summary>
    /// Логика взаимодействия для Receipt_invoice_window.xaml
    /// </summary>
    public partial class Receipt_invoice_window : Window
    {
        //public WarehouseEntities _context = new WarehouseEntities();
        public static WarehouseEntities _context = new WarehouseEntities();
        
        public Receipt_invoice_window()
        {
            InitializeComponent();
            Dgreceiptinvoice.ItemsSource = WarehouseEntities.GetContext().Receipt_invoice.ToList();
            // _recinv = (o as Button).DataContext as Receipt_invoice;
            //_window = recwindow;
            //_context = context;


        }


        public void RefreshRec()
        {
            Dgreceiptinvoice.ItemsSource = _context.Receipt_invoice.ToList();
        }
        private void BtnEditRec_Click(object sender, RoutedEventArgs e)
        {
            AddEditRecInv_window addedit = new AddEditRecInv_window(_context, this, (sender as Button).DataContext as Receipt_invoice);
            this.Hide();
            addedit.Show();

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Select_a_table select_A_Table = new Select_a_table();
            this.Hide();
            select_A_Table.Show();
        }

        private void BtnAddRec_Click(object sender, RoutedEventArgs e)
        {
            AddEditRecInv_window addedit = new AddEditRecInv_window(_context, this, null);
            this.Hide();
            addedit.Show();
        }

        private void BtnDeleteRec_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = Dgreceiptinvoice.SelectedItems.Cast<Receipt_invoice>().ToList();
            if (MessageBox.Show("Вы точно хотите удалить следующий элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    WarehouseEntities.GetContext().Receipt_invoice.RemoveRange(hotelsForRemoving);
                    WarehouseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    Dgreceiptinvoice.ItemsSource = null;
                    Dgreceiptinvoice.ItemsSource = WarehouseEntities.GetContext().Receipt_invoice.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            /*var result = MessageBox.Show(_recinv.Name_of_inv_rec, "Действительно хотите удалить?", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                _context.Receipt_invoice.Remove(_recinv);
                _context.SaveChanges();

                _window.RefreshRec();
                this.Close();
            }*/
        }
    }
}
