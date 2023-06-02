using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        decimal numOne, numTwo = 0.0m;
        string result;
        string operation = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            numOne = 0;
            numTwo = 0;
            operation = "";
            labelContentOutput.Text = "0";
            labelEquationPreview.Text = "";

            buttonPlus.IsEnabled = true;
            buttonMinus.IsEnabled = true;
            buttonMultiplied.IsEnabled = true;
            buttonDivided.IsEnabled = true;
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            labelContentOutput.Text = "0";
        }

        private void Button_Equals(object sender, RoutedEventArgs e)
        {
            numTwo = decimal.Parse(labelContentOutput.Text);
            labelEquationPreview.Text = $"{labelEquationPreview.Text} {labelContentOutput.Text} =";

            buttonPlus.IsEnabled = true;
            buttonMinus.IsEnabled = true;
            buttonMultiplied.IsEnabled = true;
            buttonDivided.IsEnabled = true;

            try
            {

                switch (operation)
                {
                    case "+":
                        result = labelContentOutput.Text = (numOne + numTwo).ToString();
                        break;
                    case "-":
                        result = labelContentOutput.Text = (numOne - numTwo).ToString();
                        break;
                    case "x":
                        result = labelContentOutput.Text = (numOne * numTwo).ToString();
                        break;
                    case "÷":
                        try
                        {
                            result = labelContentOutput.Text = (numOne / numTwo).ToString();
                        }
                        catch (System.DivideByZeroException)
                        {
                            result = labelContentOutput.Text = "Infinity";
                        }
                        break;

                    default:
                        labelEquationPreview.Text = $"{labelContentOutput.Text} =";
                        break;
                }
            }
            catch (System.OverflowException)
            {
                MessageBox.Show("The Number Is Too Long.");
            }
        }

        private void ButtonBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (labelContentOutput.Text.Length > 0)
            {
                labelContentOutput.Text = labelContentOutput.Text.Remove(labelContentOutput.Text.Length - 1, 1);
            }
            if (labelContentOutput.Text == "")
                labelContentOutput.Text = labelContentOutput.Text = "0";
        }

        private void Button_Numbers(object sender, RoutedEventArgs e)
        {
            if (labelContentOutput.Text == result)
                labelContentOutput.Text = "0";

            buttonPlus.IsEnabled = true;
            buttonMinus.IsEnabled = true;
            buttonMultiplied.IsEnabled = true;
            buttonDivided.IsEnabled = true;

            Button B = (Button)sender;
            if (labelContentOutput.Text == "0")
            {
                labelContentOutput.Text = B.Content.ToString();
            }
            else
            {
                labelContentOutput.Text += B.Content.ToString();
            }
        }

        private void Button_Operand(object sender, RoutedEventArgs e)
        {
            buttonEquals.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            Button B = (Button)sender;
            numOne = decimal.Parse(labelContentOutput.Text);
            operation = B.Content.ToString();
            labelContentOutput.Text = "0";

            labelEquationPreview.Text = numOne.ToString() + " " + operation;

            buttonPlus.IsEnabled = false;
            buttonMinus.IsEnabled = false;
            buttonMultiplied.IsEnabled = false;
            buttonDivided.IsEnabled = false;

        }

        private void Button_Negative(object sender, RoutedEventArgs e)
        {
            labelContentOutput.Text = Convert.ToString(-1 * double.Parse(labelContentOutput.Text));
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


    }
}
