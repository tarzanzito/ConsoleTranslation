using System.Drawing.Text;

namespace WinFormsTranslator
{
    internal enum ScreenState
    {
        DisableAllInput,
        InputDataIncompleted,       // (button process DISABLE)
        InputDataAndReadyToProcess, // (button process ENABLED)
        ProcessStarted,
        ProcessCancelled
    }

    public partial class Form1 : Form
    {
        private ScreenState _currScreenState;
        private ScreenState _lastScreenState;
        private string _targetFolder = string.Empty;
        private const string ButtonProcessTitle = "Process";
        private const string ButtonCancelTitle = "Cancel";

        public Form1()
        {
            InitializeComponent();
        }

        #region Private Methods

        private async Task ExecuteAtionAsync()
        {
            await Task.Delay(15000);
        }

        private async Task CancelAtionAsync()
        {
            await Task.Delay(15000);
        }

        private void ChangeScreenState(string sender)
        {
            listBox1.Items.Add($"{sender}, Curr ScreenState: {_currScreenState}, Last Screen State: {_lastScreenState}");

            if (_currScreenState == _lastScreenState)
                return;

            switch (_currScreenState)
            {
                case ScreenState.DisableAllInput:
                    textBoxFileName.Enabled = false;
                    comboBoxSourceLang.Enabled = false;
                    comboBoxTargeLang.Enabled = false;

                    buttonClose.Enabled = true;
                    buttonExploreFile.Enabled = false;
                    buttonProcessCancel.Enabled = false;
                    buttonOpenFolder.Enabled = false;

                    listBox1.Enabled = false;

                    buttonProcessCancel.Text = ButtonProcessTitle;

                    Cursor = Cursors.Default;
                    buttonProcessCancel.Cursor = Cursors.Default;
                    buttonClose.Cursor = Cursors.Default;

                    break;

                //input data - NO condition to process (button process DISABLE)
                case ScreenState.InputDataIncompleted:

                    textBoxFileName.Enabled = true;
                    comboBoxSourceLang.Enabled = true;
                    comboBoxTargeLang.Enabled = true;

                    buttonClose.Enabled = true;
                    buttonExploreFile.Enabled = true;
                    buttonProcessCancel.Enabled = false;
                    buttonOpenFolder.Enabled = (_targetFolder.Length > 0);

                    listBox1.Enabled = false;

                    buttonProcessCancel.Text = ButtonProcessTitle;

                    Cursor = Cursors.Default;
                    buttonProcessCancel.Cursor = Cursors.Default;
                    buttonClose.Cursor = Cursors.Default;

                    break;

                //input data - WITH condition to process (button process ENABLE)
                case ScreenState.InputDataAndReadyToProcess:
                    textBoxFileName.Enabled = true;
                    comboBoxSourceLang.Enabled = true;
                    comboBoxTargeLang.Enabled = true;

                    buttonClose.Enabled = true;
                    buttonExploreFile.Enabled = true;
                    buttonProcessCancel.Enabled = true;
                    buttonOpenFolder.Enabled = false;

                    listBox1.Enabled = false;

                    buttonProcessCancel.Text = ButtonProcessTitle;

                    Cursor = Cursors.Default;
                    buttonProcessCancel.Cursor = Cursors.Default;
                    buttonClose.Cursor = Cursors.Default;

                    break;

                //Process Started (DISABLE input data)
                case ScreenState.ProcessStarted:
                    textBoxFileName.Enabled = false;
                    comboBoxSourceLang.Enabled = false;
                    comboBoxTargeLang.Enabled = false;

                    buttonClose.Enabled = true;
                    buttonExploreFile.Enabled = false;
                    buttonProcessCancel.Enabled = true;
                    buttonOpenFolder.Enabled = false;

                    listBox1.Enabled = false;

                    buttonProcessCancel.Text = ButtonCancelTitle;

                    Cursor = Cursors.WaitCursor;
                    buttonProcessCancel.Cursor = Cursors.AppStarting;
                    buttonClose.Cursor = Cursors.AppStarting;
                    break;

                //Process Cancelled (DISABLE input data)
                case ScreenState.ProcessCancelled:
                    textBoxFileName.Enabled = false;
                    comboBoxSourceLang.Enabled = false;
                    comboBoxTargeLang.Enabled = false;

                    buttonClose.Enabled = true;
                    buttonExploreFile.Enabled = false;
                    buttonProcessCancel.Enabled = false;
                    buttonOpenFolder.Enabled = false;

                    listBox1.Enabled = false;

                    buttonProcessCancel.Text = "Undo...";

                    Cursor = Cursors.WaitCursor;
                    buttonProcessCancel.Cursor = Cursors.WaitCursor;
                    buttonClose.Cursor = Cursors.AppStarting;

                    break;
            }

            _lastScreenState = _currScreenState;
        }

        private bool IsValidDraggedFile(DragEventArgs e)
        {
            string fileName = GetDroppedFile(e);
            return (fileName.Length > 0);
        }

        private string GetDroppedFile(DragEventArgs e)
        {
            if (e.Data == null)
                return string.Empty;

            object? data = e.Data.GetData(DataFormats.FileDrop);

            if (data == null)
                return string.Empty;

            string[] files = (string[])data;

            if (files.Length != 1)
                return string.Empty;

            return files[0];
        }

        bool IsValidInputData()
        {
            if (textBoxFileName.Text.Length == 0)
                return false;

            if (comboBoxSourceLang.Text.Length == 0)
                return false;

            if (comboBoxTargeLang.Text.Length == 0)
                return false;

            return true;
        }

        private async Task PrepareAtionAsync()
        {
            //before
            switch (_currScreenState)
            {
                case ScreenState.InputDataAndReadyToProcess:
                    _currScreenState = ScreenState.ProcessStarted;
                    ChangeScreenState("process");
                    break;

                case ScreenState.ProcessStarted:
                    _currScreenState = ScreenState.ProcessCancelled;
                    ChangeScreenState("cancel");
                    break;

                default:
                    return;
            }

            //action
            if (_currScreenState == ScreenState.ProcessStarted)
            {
                await ExecuteAtionAsync();
                this.listBox1.Items.Add("After Process !!!!");
            }

            if (_currScreenState == ScreenState.ProcessCancelled)
            {
                //Cancel
                await CancelAtionAsync();
                this.listBox1.Items.Add("After CANCEL !!!!");
            }

            //after
            _currScreenState = ScreenState.InputDataAndReadyToProcess;
            ChangeScreenState("buttonProcessCancel");
        }

        private void TextChangedAll()
        {
            if (IsInputDataEnabled())
                return;

            if (IsValidInputData())
                _currScreenState = ScreenState.InputDataAndReadyToProcess;
            else
                _currScreenState = ScreenState.InputDataIncompleted;

            ChangeScreenState("TextChangedAll");
        }

        private bool IsInputDataEnabled()
        {
            return ((_currScreenState == ScreenState.InputDataAndReadyToProcess)
                || (_currScreenState != ScreenState.InputDataIncompleted));
        }

        #endregion

        #region Visual Event Methods

        private void Form1_Load(object sender, EventArgs e)
        {
            _currScreenState = ScreenState.InputDataIncompleted;
            _lastScreenState = ScreenState.DisableAllInput;

            _targetFolder = string.Empty;
            listBox1.Items.Clear();

            ChangeScreenState("Load");
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            //Executed one time
            if (IsValidDraggedFile(e))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            textBoxFileName.Text = GetDroppedFile(e);
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
            textBoxFileName.SelectAll();
        }

        private async void buttonClose_Click(object sender, EventArgs e)
        {
            if (_currScreenState == ScreenState.ProcessStarted)
                await PrepareAtionAsync();

            //espera que cancel termine !!!
            Close();
        }
  
        private async void buttonProcessCancel_Click(object sender, EventArgs e)
        {
            await PrepareAtionAsync();
        }

        private void textBoxFileName_TextChanged(object sender, EventArgs e)
        {
            TextChangedAll();
        }

        private void comboBoxSourceLang_TextChanged(object sender, EventArgs e)
        {
            TextChangedAll();
        }

        private void comboBoxTargeLang_TextChanged(object sender, EventArgs e)
        {
            TextChangedAll();
        }

        #endregion
    }
}
