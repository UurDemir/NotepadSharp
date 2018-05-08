using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditor.Engine;

namespace TextEditor
{
    public partial class frmMain : Form
    {
        private readonly OpenFileDialog _openFileDialog = new OpenFileDialog();
        private readonly ColorDialog _highlightColorDialog = new ColorDialog();
        private readonly ColorDialog _highlightTextColorDialog = new ColorDialog();

        private frmSearch _frmSearch;

        public frmMain()
        {
            InitializeComponent();

            //Dosya seçim penceresinin ayarlamasını yapar.
            InitiliazeFileDialog();

            // Yazı arka plan rengi penceresinin ayarlamasını yapar.
            InitiliazeHighlightColorDialog();

            // Yazı rengi penceresinin ayarlamasını yapar.
            InitiliazeHighlightTextColorDialog();

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.F:
                    OpenSearchForm();
                    return true;
                case Keys.Control | Keys.O:
                    OpenFile();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmAboutUs = new frmAboutBox();


            // Bir pencere ShowDialog olarak açıldığında arka taraftaki pencerelere erişilemez.
            frmAboutUs.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSearchForm();
        }

        private void highlightColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _highlightColorDialog.ShowDialog();
        }


        private void InitiliazeFileDialog()
        {
            // Kullanıcı elle klasör yolu girdiğinde dosyanın var olup olmadığını kontrol etmesi için
            _openFileDialog.CheckPathExists = true;

            // Kullanıcı elle dosya yolu girdiğinde dosyanın var olup olmadığını kontrol etmesi için
            _openFileDialog.CheckFileExists = true;

            // Pencere açıldığında direk .txt uzantılı dosyaları aramasını sağlar
            _openFileDialog.DefaultExt = ".txt";

            // Kullacının sadece .txt uzantılı dosyaları arayabilmesini sağlar.
            _openFileDialog.Filter = "Text Files (*.txt)|*.txt";

            // Kullacının pencereyi kapattığı andaki yolunu kayıt eder ve tekrar açıldığında aynı yolda açılmasığını sağlar.
            _openFileDialog.RestoreDirectory = true;

            // Dosya seçme penceresinin başlığı
            _openFileDialog.Title = "Please select a text file to open !";
        }

        private void InitiliazeHighlightColorDialog()
        {
            // Kullanıcının kendi renklerini oluşturabilmesine izin verir.
            _highlightColorDialog.AllowFullOpen = true;

            // Kullanıcının bütün basic renkler içerisindenseçim yapabilmesini sağlar.
            _highlightColorDialog.AnyColor = true;

            // Renk seçim penceresinin tam olarak açık açılmasını sağlar.
            _highlightColorDialog.FullOpen = true;

            // Renk seçim penceresine default rengi atar.
            _highlightColorDialog.Color = SearchOptions.HighlightColor;
        }

        private void InitiliazeHighlightTextColorDialog()
        {
            // Kullanıcının kendi renklerini oluşturabilmesine izin verir.
            _highlightTextColorDialog.AllowFullOpen = true;

            // Kullanıcının bütün basic renkler içerisindenseçim yapabilmesini sağlar.
            _highlightTextColorDialog.AnyColor = true;

            // Renk seçim penceresinin tam olarak açık açılmasını sağlar.
            _highlightTextColorDialog.FullOpen = true;

            // Renk seçim penceresine default rengi atar.
            _highlightTextColorDialog.Color = SearchOptions.TextColor;
        }
        
        private void OpenSearchForm()
        {
            // Arama formu oluşturulmuş ve açık mı 
            if (_frmSearch != null && !_frmSearch.IsDisposed)
            {
                _frmSearch.Focus();
                return;
            }

            // Arama formunu tekrar oluştur.
            _frmSearch = new frmSearch(txtContent);

            // Arama formunu göster.
            // Bir pencere Show olarak açıldığında arka taraftaki pencerelere erişilebilir.
            _frmSearch.Show();
        }

        private void OpenFile()
        {
            // Bir pencere ShowDialog olarak açıldığında arka taraftaki pencerelere erişilemez.
            var dialogResult = _openFileDialog.ShowDialog();

            //Eğer kullanıcı seçim yaparak pencereyi kapattıysa
            if (dialogResult == DialogResult.OK)
                txtContent.Text = File.ReadAllText(_openFileDialog.FileName);
        }

    }
}
