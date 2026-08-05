using System.Drawing.Text;

namespace WinFormsTranslator
{
    public partial class FormBase : Form
    {
        #region Fields

        #endregion

        #region Constructors

        public FormBase()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        #endregion

        #region Visual Event Methods

        private void Form1_Load(object sender, EventArgs e)
        {
            Padding = new Padding(0);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            //Executed one time
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void Form1_DragLeave(object sender, EventArgs e)
        {
            //Executed one time
        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            //execute infinite times 
        }

        private void textBoxFileName_DoubleClick(object sender, EventArgs e)
        {
        }

        private async void buttonClose_Click(object sender, EventArgs e)
        {
        }

        private async void buttonProcessCancel_Click(object sender, EventArgs e)
        {
        }

        private void textBoxFileName_TextChanged(object sender, EventArgs e)
        {
        }

        #endregion

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
