using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ArithmeticCompiler.Helpers
{
    public enum MessageTypes
    {
        Error,
        Ok
    }

    class CompileLogger
    {
        #region Private fields

        private readonly TextBlock _logger;
        private readonly Image _warningImg;

        #endregion

        #region Constructor

        public CompileLogger(ref TextBlock textBlock,ref Image warningImg)
        {
           if(textBlock==null || warningImg==null)
               throw new ArgumentNullException();

            _logger = textBlock;
            _warningImg = warningImg;
        }

        #endregion

        #region Public methods

        public void PrintMessage(String msg,MessageTypes msgType)
        {
            Clear();
            switch(msgType)
            {
                case MessageTypes.Error:
                    _warningImg.Source = new BitmapImage(new Uri(@Properties.Resources.IconError,UriKind.Relative));
                    _warningImg.Visibility = Visibility.Visible;
                    _logger.Text = msg;
                    break;
                case MessageTypes.Ok:
                    _warningImg.Source = new BitmapImage(new Uri(@Properties.Resources.IconOk, UriKind.Relative));
                    _warningImg.Visibility = Visibility.Visible;
                    _logger.Text = msg;
                    break; 
                default:
                    throw new ArgumentException();
            }               
        }

        public void Clear()
        {
            _logger.Text = String.Empty;
            _warningImg.Visibility = Visibility.Hidden;
        }

        #endregion


    }
}
