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

        private void BtnPDF_Click(object sender, RoutedEventArgs e)
        {
            List<Receipt_invoice> recs;

            using (WarehouseEntities warehouseEntities = new WarehouseEntities())
            {
                recs = warehouseEntities.Receipt_invoice.ToList().OrderBy(s => s.ID_rec_invoice).ToList();
                var app = new Word.Application();
                Word.Document document = app.Documents.Add();

                Word.Paragraph paragraph =
                document.Paragraphs.Add();
                Word.Range range = paragraph.Range;
                range.Text = "";
                paragraph.set_Style("Заголовок 1");
                range.InsertParagraphAfter();
                Word.Paragraph tableParagraph = document.Paragraphs.Add();
                Word.Range tableRange = tableParagraph.Range;
                Word.Table studentsTable =
                document.Tables.Add(tableRange, recs.Count() + 1, 6);
                studentsTable.Borders.InsideLineStyle =
                studentsTable.Borders.OutsideLineStyle =
                Word.WdLineStyle.wdLineStyleSingle;
                studentsTable.Range.Cells.VerticalAlignment =
                Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                Word.Range cellRange;
                cellRange = studentsTable.Cell(1, 1).Range;
                cellRange.Text = "Номер накладного";                
                cellRange = studentsTable.Cell(1, 2).Range;
                cellRange.Text = "Дата";
                cellRange = studentsTable.Cell(1, 3).Range;
                cellRange.Text = "Инвентарь";
                cellRange = studentsTable.Cell(1, 4).Range;
                cellRange.Text = "Количество инвентаря";
                cellRange = studentsTable.Cell(1, 5).Range;
                cellRange.Text = "Сотрудник принявший инвентарь";
                cellRange = studentsTable.Cell(1, 6).Range;
                cellRange.Text = "Должность сотрудника";
                cellRange = studentsTable.Cell(1, 6).Range;
                studentsTable.Rows[1].Range.Bold = 1;
                studentsTable.Rows[1].Range.ParagraphFormat.Alignment =
                Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int i = 1;
                foreach (var currentrep in recs)
                {
                    cellRange = studentsTable.Cell(i + 1, 1).Range;
                    cellRange.Text = currentrep.ID_rec_invoice.ToString();
                    cellRange.ParagraphFormat.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = studentsTable.Cell(i + 1, 2).Range;
                    cellRange.Text = currentrep.Date_rec.ToString();
                    cellRange.ParagraphFormat.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = studentsTable.Cell(i + 1, 3).Range;
                    cellRange.Text = currentrep.Name_of_inv_rec.ToString();
                    cellRange.ParagraphFormat.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = studentsTable.Cell(i + 1, 4).Range;
                    cellRange.Text = currentrep.Number_of_product_rec.ToString();
                    cellRange.ParagraphFormat.Alignment =
                     Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = studentsTable.Cell(i + 1, 5).Range;
                    cellRange.Text = currentrep.Empl_full_name_rec.ToString();
                    cellRange.ParagraphFormat.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = studentsTable.Cell(i + 1, 6).Range;
                    cellRange.Text = currentrep.Empl_post_rec.ToString();
                    cellRange.ParagraphFormat.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = studentsTable.Cell(i + 1, 6).Range;
                    i++;
                }
                Word.Paragraph countStudentsParagraph = document.Paragraphs.Add();
                Word.Range countStudentsRange =
                countStudentsParagraph.Range;
                countStudentsRange.Text = $"Количество накладных -{recs.Count()}";
                countStudentsRange.Font.Color = Word.WdColor.wdColorDarkRed;
                countStudentsRange.InsertParagraphAfter();
                document.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);

                app.Visible = true;
                document.SaveAs2(@"C:\Users\Гульфия\Desktop\ПрактикаВорд.docx");
                document.SaveAs2(@"C:\Users\Гульфия\Desktop\ПрактикаПдф.pdf",
                Word.WdExportFormat.wdExportFormatPDF);
            }
        }
        private void BtnUpdateRecInv_Click(object sender, RoutedEventArgs e)
        {
            Windows.UpdateRecInv_window update = new Windows.UpdateRecInv_window(_context, sender, this);
            this.Hide();
            update.Show();
        }
    }
}
