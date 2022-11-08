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

namespace Ychebnaya_praktika.Windows
{
    /// <summary>
    /// Логика взаимодействия для UpdateRecInv_window.xaml
    /// </summary>
    public partial class UpdateRecInv_window : Window
    {
        public static WarehouseEntities _context = new WarehouseEntities();
        private Receipt_invoice _recinv;
        private Receipt_invoice_window _window;
        public UpdateRecInv_window(WarehouseEntities context, object o, Receipt_invoice_window recwindow)
        {
            InitializeComponent();
            _recinv = (o as Button).DataContext as Receipt_invoice;
            _context = context;
            _window = recwindow;

            TxtIDrecUp.Text = Convert.ToString(_recinv.ID_rec_invoice);
            TxtDateUp.Text = Convert.ToString(_recinv.Date_rec);
            TxtNameinvUp.Text = _recinv.Name_of_inv_rec;
            TxtNumberUp.Text= Convert.ToString(_recinv.Number_of_product_rec);
            TxtFIOEmplUp.Text = _recinv.Empl_full_name_rec;
            TxtPostEmplUp.Text = _recinv.Empl_post_rec;

        }

        private void BackToRecInvWindowUp_Click(object sender, RoutedEventArgs e)
        {
            Receipt_invoice_window recinv = new Receipt_invoice_window();
            this.Hide();
            recinv.Show();
        }

        

        private void BtnSaveRecUp_Click(object sender, RoutedEventArgs e)
        {
            
            _recinv.ID_rec_invoice = Convert.ToInt32(TxtIDrecUp.Text);
            _recinv.Date_rec = Convert.ToDateTime(TxtDateUp.Text);
            _recinv.Name_of_inv_rec = TxtNameinvUp.Text;
            _recinv.Number_of_product_rec = Convert.ToInt32(TxtNumberUp.Text);
            _recinv.Empl_full_name_rec = TxtFIOEmplUp.Text;
            _recinv.Empl_post_rec = TxtPostEmplUp.Text;
            
            _window.RefreshRec();
            _context.SaveChanges();
            this.Close();
            Receipt_invoice_window recw = new Receipt_invoice_window();
            recw.Show();
        }
    }
}
