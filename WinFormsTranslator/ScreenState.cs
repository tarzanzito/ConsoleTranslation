using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsTranslator
{
    internal enum ScreenState
    {
        AllDisabled,
        InputDataIncompleted,       // (button process DISABLE)
        InputDataReadyToProcessing, // (button process ENABLED)
        ProcessStarted,
        ProcessCancelled
    }
}

