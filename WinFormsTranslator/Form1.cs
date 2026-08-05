#pragma warning disable IDE0066
#pragma warning disable IDE1006

using Candal.Translation;
using System.Drawing.Text;
using System.Text;

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

        CancellationTokenSource? _cancellationTokenSource;// = new();
        CancellationToken _cancellationToken;// = cancellationTokenSource.Token;

        #endregion

        #region Constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

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

        private void ProcessStartOrCancelBefore()
        {
            //before execute
            switch (_currScreenState)
            {
                //Execute
                case ScreenState.InputDataReadyToProcessing:
                    _currScreenState = ScreenState.ProcessStarted;
                    break;

                //Cancel
                case ScreenState.ProcessStarted:
                    _currScreenState = ScreenState.ProcessCancelled;
                    break;

                default:
                    throw new Exception("BeforeProcessStartOrCancel Default");
            }

            ChangeScreenState();
        }

        private async Task ProcessStartOrCancelExecuteAsync()
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

        private void ProcessStartOrCancelAfter()
        {
            //after Execute or Cancel return to return to 'ReadyToProcessing'
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
                listBox1.Items.Add($"Curr Screen State: '{_currScreenState}', Last Screen State: '{_lastScreenState}'");
            else
                listBox1.Items.Add($"Msg: {message}");
#endif
        }

        #endregion

        #region Tasks

        private async Task TaskExecuteAsync()
        {
            try
            {
                _cancellationTokenSource = new();
                _cancellationToken = _cancellationTokenSource.Token;


                WriteLog("TaskExecuteAsync Int.");
                TranslationData translationData = CreateTranslationData();

                WriteLog($"Read all file: '{translationData.FileNameIn}'.");
                string text = await File.ReadAllTextAsync(translationData.FileNameIn, Encoding.UTF8, _cancellationToken);

                //Cloose translator
                //Libretranslator translator = new();
                Googletranslator translator = new();

                //Create TranslatorHelper 
                //using NewLine to split
                TranslatorHelperUserInterface translatorHelper = new(translator);
                //TranslatorHelperApiRest translatorHelper = new(translator);

                //using special chars to split
                //char[] _charsToSplitAtEnd = new[] { '.', '?' , '!'};
                //TranslatorHelper translatorHelper = new(translator, _charsToSplitAtEnd);

                //splits All file string into List<string>
                WriteLog("Splits all string into List<string>.");
                List<string> textBlockList = await translatorHelper.CreateBlockListFromStringAsync(text, _cancellationToken);


                //translate List<atring>
                WriteLog("Translate List<atring>.");
                List<string> translatedTextBlockList = await translatorHelper.TranslateBlockListAsync(textBlockList, translationData.SourceLang, translationData.TargetLang, _cancellationToken);

                //for only compare in and out files. must be equals
                //List<string> translatedTextBlockList = textBlockList;

                //join List<string> into one string
                WriteLog("Join translated List<string> into one string.");
                string result = await translatorHelper.CreateStringFromBlockListAsync(translatedTextBlockList);

                //Write translated file
                WriteLog($"Write all string into file: '{translationData.FileNameOut}'.");
                await File.WriteAllTextAsync(translationData.FileNameOut, result, Encoding.UTF8);

                //string test = await translatorHelper.TranslateTextAsync(text, translationData.SourceLang, translationData.TargetLang, _cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error found: {ex.Message}");
                throw;
            }

            //await Task.Delay(15000);
        }

 
        private async Task TaskCancelAsync()
        {
            if (_cancellationTokenSource != null)
                await _cancellationTokenSource.CancelAsync();
            //await Task.Delay(10000);
        }

        private TranslationData CreateTranslationData()
        {
            string fileOut = textBoxFileName.Text.Replace($"-{comboBoxSourceLang.Text}", $"-{comboBoxTargeLang.Text}");
            //string fileOut = fileIn.Insert(pos, $"-{target}");

            TranslationData translationData = new()
            {
                FileNameIn = textBoxFileName.Text,
                SourceLang = comboBoxSourceLang.Text,
                TargetLang = comboBoxTargeLang.Text,
                FileNameOut = fileOut
            };

            return translationData;
        }

        #endregion

        #region Visual Event Methods

        private void Form1_Load(object sender, EventArgs e)
        {
            ChangeScreenState();

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
            //Executed one time
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
        //private bool allowClose = false;
        //private async void MyForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (!allowClose)
        //    {
        //        e.Cancel = true; // Stop immediate close
        //        _cancellationTokenSource.Cancel(); // Signal cancellation

        //        try
        //        {
        //            await _runningTask; // Wait for task to finish cleanup
        //        }
        //        catch (OperationCanceledException) { }

        //        allowClose = true;
        //        Close(); // Close the form for real
        //    }
        //}



        

        //multi canceltoken
        //https://www.youtube.com/watch?v=Fx5VIXIV7ho
        private async void buttonProcessCancel_Click(object sender, EventArgs e)
        {
            ProcessStartOrCancelBefore();
            await ProcessStartOrCancelExecuteAsync();
            ProcessStartOrCancelAfter();
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
