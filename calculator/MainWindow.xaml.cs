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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Double Value = 0; // Store Decimal and Int Values if needed
        private string Operand;

        private bool OperandPressed;


        public MainWindow()
        {
            InitializeComponent();
            ClearAll();
        }

        public void ClearAll()
        {
            ShowNumber = 0;
            Result = 0;
            FirstNumber = 0;
            SecondNumber = 0;
            IsNegative = false;
            IsSecond = false;
            IsDecimal = false;
            DecimalValue = 1;
            NegativeValue = -1;
            Operand = string.Empty;
            UpdatePreviewEquation();
        }

        private double _showNumber;

        public double ShowNumber
        {
            get { return _showNumber; }
            set
            {
                _showNumber = value;
                labelContentOutput.Text = ShowNumber.ToString();
            }
        }

        private double _firstNumber;

        public double FirstNumber
        {
            get { return _firstNumber; }
            set { _firstNumber = value; }
        }

        public double SecondNumber { get; set; }

        public double Result { get; set; }

        public bool IsSecond { get; set; }

        public bool IsDecimal { get; set; }

        public double NegativeValue { get; set; }

        public int DecimalValue { get; set; }
        public bool IsNegative { get; private set; }

        private void Clear_Click(object senShowNumdEventArgs, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            if (Operand != "")
            {
                if (SecondNumber > 0)
                {
                    SecondNumber = 0;
                    ShowNumber = SecondNumber;
                    IsDecimal = false;
                    DecimalValue = 1;
                }
                else
                {
                    Operand = "";
                    IsSecond = false;
                }
            }
            else
            {
                ClearAll();
            }
            labelContentOutput.Text = "0";
            UpdatePreviewEquation();
        }

        private void ButtonBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (Operand != "")
            {
                var secondString = SecondNumber.ToString();
                if (secondString.Length >= 1)
                {
                    var newSecondNumberString = secondString.Substring(0, secondString.Length - 1);

                    if (newSecondNumberString == "" || newSecondNumberString == "-") // is backspaced number 0, blank, or negative ?
                    {
                        SecondNumber = 0;
                        ShowNumber = SecondNumber;
                    }
                    else
                    {
                        SecondNumber = double.Parse(newSecondNumberString); // else remove one number
                        ShowNumber = SecondNumber;
                    }
                }
            }
            else
            {
                var firstString = FirstNumber.ToString();
                if (firstString.Length >= 1)
                {
                    var newFirstNumberString = firstString.Substring(0, firstString.Length - 1);

                    if (newFirstNumberString == "" || newFirstNumberString == "-") // is backspaced number 0, blank, or negative ?
                    {
                        FirstNumber = 0;
                        ShowNumber = FirstNumber;
                    }
                    else
                    {
                        FirstNumber = double.Parse(newFirstNumberString); // else remove one number
                        ShowNumber = FirstNumber;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender; // Cast Generic Object To Buttom

            // To Get Rid of the Initial 0 in the Display
            if ((labelContentOutput.Text.ToString() == "0") || OperandPressed)
            {
                labelContentOutput.Text = " ";
            }

            if (B.Content.ToString() == ".")
            {
                IsDecimal = true;
            }
            else
            {
                if (IsSecond)
                {
                    if (IsDecimal)
                    {
                        SecondNumber = double.Parse($"{SecondNumber}.{B.Content}");
                    }
                    else
                    {
                        SecondNumber = double.Parse($"{SecondNumber}{B.Content}");
                    }
                    ShowNumber = SecondNumber;
                }
                else
                {
                    if (IsDecimal)
                    {
                        FirstNumber = double.Parse($"{FirstNumber}.{B.Content}");
                    }
                    else
                    {
                        FirstNumber = double.Parse($"{FirstNumber}{B.Content}");
                    }
                    ShowNumber = FirstNumber;
                }
            }
            OperandPressed = false;
            UpdatePreviewEquation();
        }

        private void UpdatePreviewEquation()
        {
            var currentEquationList = new List<string>();
            string currentValue = labelContentOutput.Text;
            currentEquationList.Add(currentValue);

            if (currentEquationList.Count <= 0)
            {
                textEquationPreview.Text = "";
            }
            for (int i = 0; i < currentEquationList.Count; i++)
            {
                textEquationPreview.Text = currentEquationList[i];
            }
        }

        private void Button_Operand(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;

            if ((labelContentOutput.Text.ToString() == "0") || OperandPressed)
            {
                labelContentOutput.Text = " ";
            }
            if (Operand == "")
            {
                Operand = B.Content.ToString(); // store operand
                IsSecond = true;
                ShowNumber = SecondNumber;
                IsNegative = false;
                IsDecimal = false;
                DecimalValue = 1;
            }
            else
            {
                DoCalcuation();
            }
            UpdatePreviewEquation();
        }

        public void DoCalcuation()
        {
            switch (Operand)
            {
                case "+":
                    Result = FirstNumber + SecondNumber;
                    ResetAfterCalculation();
                    break;

                case "-":
                    Result = FirstNumber - SecondNumber;
                    ResetAfterCalculation();
                    break;

                case "×":
                    Result = FirstNumber * SecondNumber;
                    ResetAfterCalculation();
                    break;

                case "÷":
                    Result = FirstNumber / SecondNumber;
                    ResetAfterCalculation();
                    break;

                default:
                    break;
            }
        }

        public void ResetAfterCalculation()
        {
            FirstNumber = Result;
            ShowNumber = Result;
            SecondNumber = 0;
            Operand = "";
            textEquationPreview.Text = "";
        }

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            bool shift = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
            if (shift == true && e.Key == Key.OemQuestion)
            {
                buttonMultiplied.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (shift == true && e.Key == Key.Oem7)
            {
                buttonDivided.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }

            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    buttonDivided.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D1:
                case Key.NumPad1:
                    buttonOne.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D2:
                case Key.NumPad2:
                    buttonTwo.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D3:
                case Key.NumPad3:
                    buttonThree.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D4:
                case Key.NumPad4:
                    buttonFour.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D5:
                case Key.NumPad5:
                    buttonFive.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D6:
                case Key.NumPad6:
                    buttonSix.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D7:
                case Key.NumPad7:
                    buttonSeven.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D8:
                case Key.NumPad8:
                    buttonEight.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D9:
                case Key.NumPad9:
                    buttonNine.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.OemPlus:
                case Key.Add:
                    buttonPlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.OemMinus:
                case Key.Subtract:
                    buttonMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.Multiply:
                    buttonMultiplied.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.Divide:
                    buttonDivided.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break; //bool shift  = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift); case Key.D5: return (shift ? '%' : '5');
                case Key.Enter:
                    buttonEquals.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.OemComma:
                    buttonDecimal.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
            }
        }

        private void Button_Negative(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;

            if (B.Content.ToString() == "+/-")
            {
                IsNegative = true;

                if (IsSecond)
                {
                    SecondNumber *= NegativeValue;
                    ShowNumber = SecondNumber;
                }
                else
                {
                    FirstNumber *= NegativeValue;
                    ShowNumber = FirstNumber;
                }
            }
            OperandPressed = false;
            UpdatePreviewEquation();
        }
    }
}
