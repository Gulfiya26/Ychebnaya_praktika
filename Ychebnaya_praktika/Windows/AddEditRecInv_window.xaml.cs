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
using Word = Microsoft.Office.Interop.Word;

namespace Ychebnaya_praktika
{
    /// <summary>
    /// Логика взаимодействия для AddEditRecInv.xaml
    /// </summary>
    public partial class AddEditRecInv_window : Window
    {
        private WarehouseEntities _context;
        private Receipt_invoice_window _window;
        private Receipt_invoice _currentrec = new Receipt_invoice();
        public AddEditRecInv_window(WarehouseEntities context, Receipt_invoice_window recwindow, Receipt_invoice selectedRec)
        {
            InitializeComponent();
            this._context= context;
            this._window = recwindow;
            if ( selectedRec!= null)
                _currentrec = selectedRec;
            DataContext = _currentrec;
        }

        private void BtnSaveRec_Click(object sender, RoutedEventArgs e)
        {
           /* StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentrec.Name_of_inv_rec))
                errors.AppendLine("Укажите название Инвентаря");
            if (string.IsNullOrWhiteSpace(_currentrec.Empl_full_name_rec)) 
                errors.AppendLine("Укажите ФИО сотрудника");
            if (string.IsNullOrWhiteSpace(_currentrec.Empl_post_rec))
                errors.AppendLine("Укажите должность сотрудника");
            if (_currentrec. ID_rec_invoice< 0 || TxtIDrec.Text=="")
                errors.AppendLine("Укажите правильный номер накладного");
            if (_currentrec.Number_of_product_rec < 0 || TxtIDrec.Text == "")
                errors.AppendLine("Укажите правильное количество инвентаря");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentrec.ID_rec_invoice == 0)
                WarehouseEntities.GetContext().Receipt_invoice.Add(_currentrec);
            try
            {
                WarehouseEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }*/
              _context.Receipt_invoice.Add(new Receipt_invoice()
              {
                  ID_rec_invoice = Convert.ToInt32(TxtIDrec.Text),
                  Date_rec = DateTime.Parse(TxtDate.Text),
                  Name_of_inv_rec = TxtNameinv.Text,
                  Number_of_product_rec = Convert.ToInt32(TxtNumber.Text),
                  Empl_full_name_rec = TxtFIOEmpl.Text,
                  Empl_post_rec = TxtPostEmpl.Text
              });


              _context.SaveChanges();
              _window.RefreshRec();

              this.Close();
              Receipt_invoice_window receipt_Invoice_Window = new Receipt_invoice_window();
              receipt_Invoice_Window.Show();

        }

        private void BackToRecInvWindow_Click(object sender, RoutedEventArgs e)
        {
            Receipt_invoice_window recwin = new Receipt_invoice_window();
            this.Hide();
            recwin.Show();

        }

        private void BtnPDF_Click(object sender, RoutedEventArgs e)
        {
           
        
        }
    }
}
