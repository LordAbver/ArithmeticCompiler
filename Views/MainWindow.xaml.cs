using System;
using System.Windows;
using ArithmeticCompiler.CompilerCore;
using ArithmeticCompiler.Helpers;

namespace ArithmeticCompiler.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Private fields

        private readonly Compiler _compiler;
        private readonly CompileLogger _logger;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            _compiler = new Compiler();
            _logger = new CompileLogger(ref CompileLogTxtBlock, ref StatusIcon);
        }

        #endregion
       

        #region UI Event Handlers

        private void StatementTxtBoxChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ResultTxtBox.Text = String.Empty;
            if (String.IsNullOrEmpty(StatementTxtBox.Text))
            {
                _logger.Clear();
                EqualBtn.IsEnabled = false;
                return;  
            }
            try
            {
                _compiler.Compile(StatementTxtBox.Text);
                _logger.PrintMessage("No compilation errors!",MessageTypes.Ok);
                EqualBtn.IsEnabled = true;
            }
            catch(Exception ex)
            {
                _logger.PrintMessage("Compilation error:", MessageTypes.Error);
                EqualBtn.IsEnabled = false;
            }
            
        }

        private void ClearBtnClick(object sender, RoutedEventArgs e)
        {
            _logger.Clear();
            EqualBtn.IsEnabled = false;
            StatementTxtBox.Text = String.Empty;
            ResultTxtBox.Text = String.Empty;
        }

        private void EqualBtnClick(object sender, RoutedEventArgs e)
        {
            ResultTxtBox.Text = _compiler.GetResult().ToString();
        }

        #endregion

        

       

        
    }
}
