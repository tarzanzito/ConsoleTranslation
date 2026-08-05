using System.Drawing.Text;

namespace WinFormsTranslator
{
    public partial class Form1 : Form
    {
        #region Fields

        private const string ButtonProcessTile = "Process";
        private const string ButtonCancelTitle = "Cancel";
        private const string ButtonUndoTitle = "Undo...";

        private ScreenState _currScreenState = ScreenState.InputDataIncompleted;
        private ScreenState _lastScreenState = ScreenState.AllDisabled;

        private string _initialDirectory = string.Empty;

        #endregion

        #region Constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private async Task ExecuteAtionAsync()
        {
            await Task.Delay(15000);
        }

        private async Task CancelAtionAsync()
        {
            await Task.Delay(15000);
        }

        private void ChangeScreenStateAllDisabled()
        {
            textBoxFileName.Enabled = false;
            comboBoxSourceLang.Enabled = false;
            comboBoxTargeLang.Enabled = false;

            buttonClose.Enabled = true;
            buttonExploreFile.Enabled = false;
            buttonProcessCancel.Enabled = false;
            buttonOpenFolder.Enabled = false;

            listBox1.Enabled = false;

            buttonProcessCancel.Text = ButtonProcessTile;

            Cursor = Cursors.Default;
            buttonProcessCancel.Cursor = Cursors.Default;
            buttonClose.Cursor = Cursors.Default;
        }

        private void ChangeScreenStateInputDataIncompleted()
        {
            textBoxFileName.Enabled = true;
            comboBoxSourceLang.Enabled = true;
            comboBoxTargeLang.Enabled = true;

            buttonClose.Enabled = true;
            buttonExploreFile.Enabled = true;
            buttonProcessCancel.Enabled = false;
            buttonOpenFolder.Enabled = (textBoxFileName.Text.Length > 0);

            listBox1.Enabled = true;

            buttonProcessCancel.Text = ButtonProcessTile;

            Cursor = Cursors.Default;
            buttonProcessCancel.Cursor = Cursors.Default;
            buttonClose.Cursor = Cursors.Default;
        }

        private void ChangeScreenStateInputDataReadyToProcessing()
        {
            textBoxFileName.Enabled = true;
            comboBoxSourceLang.Enabled = true;
            comboBoxTargeLang.Enabled = true;

            buttonClose.Enabled = true;
            buttonExploreFile.Enabled = true;
            buttonProcessCancel.Enabled = true;
            buttonOpenFolder.Enabled = false;

            listBox1.Enabled = true;

            buttonProcessCancel.Text = ButtonProcessTile;

            Cursor = Cursors.Default;
            buttonProcessCancel.Cursor = Cursors.Default;
            buttonClose.Cursor = Cursors.Default;
        }

        private void ChangeScreenStateProcessStarted()
        {
            textBoxFileName.Enabled = false;
            comboBoxSourceLang.Enabled = false;
            comboBoxTargeLang.Enabled = false;

            buttonClose.Enabled = true;
            buttonExploreFile.Enabled = false;
            buttonProcessCancel.Enabled = true;
            buttonOpenFolder.Enabled = false;

            listBox1.Enabled = true;

            buttonProcessCancel.Text = ButtonCancelTitle;

            Cursor = Cursors.WaitCursor;
            buttonProcessCancel.Cursor = Cursors.AppStarting;
            buttonClose.Cursor = Cursors.AppStarting;

        }
        
        private void ChangeScreenStateProcessCancelled()
        {
            textBoxFileName.Enabled = false;
            comboBoxSourceLang.Enabled = false;
            comboBoxTargeLang.Enabled = false;

            buttonClose.Enabled = true;
            buttonExploreFile.Enabled = false;
            buttonProcessCancel.Enabled = false;
            buttonOpenFolder.Enabled = false;

            listBox1.Enabled = true;

            buttonProcessCancel.Text = ButtonUndoTitle;

            Cursor = Cursors.WaitCursor;
            buttonProcessCancel.Cursor = Cursors.WaitCursor;
            buttonClose.Cursor = Cursors.AppStarting;
        }

        private void ChangeScreenState()
        {
            WriteLog();

            if (_currScreenState == _lastScreenState)
                return;

            switch (_currScreenState)
            {
                case ScreenState.AllDisabled:
                    ChangeScreenStateAllDisabled();
                    break;

                //input data - NO condition to process (button process DISABLE)
                case ScreenState.InputDataIncompleted:
                    ChangeScreenStateInputDataIncompleted();
                    break;

                //input data - WITH condition to process (button process ENABLE)
                case ScreenState.InputDataReadyToProcessing:
                    ChangeScreenStateInputDataReadyToProcessing();
                    break;

                //Process Started (DISABLE input data)
                case ScreenState.ProcessStarted:
                    ChangeScreenStateProcessStarted();
                    break;

                //Process Cancelled (DISABLE input data)
                case ScreenState.ProcessCancelled:
                    ChangeScreenStateProcessCancelled();
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

        private void BeforeProcessStartOrCancel()
        {
            //before execute
            switch (_currScreenState)
            {
                case ScreenState.InputDataReadyToProcessing:
                    _currScreenState = ScreenState.ProcessStarted;
                    ChangeScreenState();
                    break;

                case ScreenState.ProcessStarted:
                    _currScreenState = ScreenState.ProcessCancelled;
                    ChangeScreenState();
                    break;

                default:
                    return;
            }
        }

        private async Task ExecuteProcessStartOrCancelAsync()
        {
            //execute action
            switch (_currScreenState)
            {
                case ScreenState.ProcessStarted:
                    await TaskExecuteAsync();
                    WriteLog("After Process !!!!");
                    break;

                case ScreenState.ProcessCancelled:
                    //Cancel
                    await TaskCancelAsync();
                    WriteLog("After CANCEL !!!!");
                    break;

                default:
                    return;
            }
        }

        private async Task TaskExecuteAsync()
        {

        }

        private async Task TaskCancelAsync()
        {

        }

        private void AfterProcessStartOrCancel()
        {
            //after execute
            _currScreenState = ScreenState.InputDataReadyToProcessing;
            ChangeScreenState();
        }

        private void TextChangedAll()
        {
            if (!IsInputDataEnabled())
                return;

            if (IsValidInputData())
                _currScreenState = ScreenState.InputDataReadyToProcessing;
            else
                _currScreenState = ScreenState.InputDataIncompleted;

            ChangeScreenState();
        }

        private bool IsInputDataEnabled()
        {
            return ((_currScreenState == ScreenState.InputDataIncompleted)
                || (_currScreenState == ScreenState.InputDataReadyToProcessing));
        }

        private async Task ProcessOrCancelAsync()
        {
            BeforeProcessStartOrCancel();
            //await ProcessCancelAsync();
            await ExecuteProcessStartOrCancelAsync();
            await Task.Delay(5000);
            AfterProcessStartOrCancel();
        }

        private string OpenFileDialog()
        {
            OpenFileDialog openFileDialog1 = new();

            if (_initialDirectory == string.Empty)
                _initialDirectory = "c:\\";

            openFileDialog1.InitialDirectory = _initialDirectory;
            openFileDialog1.Filter = "Subtitle files (*.srt, *.sub, *.txt|*.srt;*.sub;*.txt";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return string.Empty;

            _initialDirectory = System.IO.Path.GetDirectoryName(openFileDialog1.FileName) ?? string.Empty;

            return openFileDialog1.FileName;

        }


        private void WriteLog(string message = "")
        {
#if DEBUG
            if (message == string.Empty)
                listBox1.Items.Add($"Curr ScreenState: {_currScreenState}, Last Screen State: {_lastScreenState}");
            else
                listBox1.Items.Add($"Msg: {message}");
#endif
        }

        #endregion

        #region Visual Event Methods

        private void Form1_Load(object sender, EventArgs e)
        {
            ChangeScreenState();

            //Application.DoEvents();
            textBoxFileName.Focus();
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
            {
                // await ProcessStartedAsync();
            }

            //espera que cancel termine !!!
            Close();
        }

        private async void buttonProcessCancel_Click(object sender, EventArgs e)
        {
            await ProcessOrCancelAsync();
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

        private void buttonExploreFile_Click(object sender, EventArgs e)
        {
            textBoxFileName.Text = OpenFileDialog();
        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_currScreenState == ScreenState.ProcessStarted)
            {
            }
        }

        #endregion






    }
}
