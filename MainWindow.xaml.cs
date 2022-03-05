using System;
using System.Data;
using System.Windows;
using System.Windows.Media;

namespace Calcultor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static string expression = "";

        #region Numbers
        private void Btn7Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty((string)Solution.Content) == false)
                ChangeLastZero("7");

            else Solution.Content = "7";
        }

        private void Btn8Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty((string)Solution.Content) == false)
                ChangeLastZero("8");

            else Solution.Content = "8";
        }

        private void Btn9Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty((string)Solution.Content) == false)
                ChangeLastZero("9");

            else Solution.Content = "9";
        }

        private void Btn4Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty((string)Solution.Content) == false)
                ChangeLastZero("4");

            else Solution.Content = "4";
        }

        private void Btn5Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty((string)Solution.Content) == false)
                ChangeLastZero("5");

            else Solution.Content = "5";
        }

        private void Btn6Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty((string)Solution.Content) == false)
                ChangeLastZero("6");

            else Solution.Content = "6";
        }

        private void Btn1Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty((string)Solution.Content) == false)
                ChangeLastZero("1");
            
            else Solution.Content = "1";
        }

        private void Btn2Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty((string)Solution.Content) == false)
                ChangeLastZero("2");
            
            else Solution.Content = "2";
        }

        private void Btn3Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty((string)Solution.Content) == false)
                ChangeLastZero("3");

            else Solution.Content = "3";
        }

        private void Btn0Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty((string)Solution.Content) == false)
            {
                if(Solution.Content.ToString() != "0") 
                { 
                    expression = Solution.Content.ToString();
                    expression += "0";
                    Solution.Content = expression;
                }
            }

            else Solution.Content = "0";
        }

        #endregion

        private void BtnDelClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty((string)Solution.Content) == true) Solution.Content = "0";

            else if (String.IsNullOrEmpty((string)Solution.Content) == false) 
            {
                expression = Solution.Content.ToString();
                if (expression.Length == 1) Solution.Content = "0";

                else if (expression != "0")
                {
                    expression = expression.Remove(expression.Length - 1, 1);
                    Solution.Content = expression;
                }
            }
        }

        private void BtnClearClick(object sender, RoutedEventArgs e)
        {
            expression = "";
            Solution.HorizontalAlignment = HorizontalAlignment.Right;
            Solution.Content = "0";
            Result.HorizontalAlignment = HorizontalAlignment.Right;
            Result.Content = "0";
        }

        private void BtnExitClick(object sender, RoutedEventArgs e) => Environment.Exit(0);

        private void BtnFracClick(object sender, RoutedEventArgs e)
        {
            if(AnalizeFranction() == false) 
                if (String.IsNullOrEmpty((string)Solution.Content) == false) CheckSymbol(".");
        }

        private void BtnPlusClick(object sender, RoutedEventArgs e) => CheckSymbol("+");
          
        private void BtnTimesClick(object sender, RoutedEventArgs e) => CheckSymbol("*");
           
        private void BtnMinClick(object sender, RoutedEventArgs e) => CheckSymbol("-");
           
        private void BtnDivideClick(object sender, RoutedEventArgs e) => CheckSymbol("/");

        private void BtnEqualClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty((string)Solution.Content) == true || String.IsNullOrEmpty(expression)) Solution.Content = "0";

            else
            {
                CheckSymbol();
                if (ZeroHandler() == false)
                {
                    try
                    {
                        decimal result = Convert.ToDecimal(new DataTable().Compute(expression, null));

                        if (result.ToString().Length > 20)
                        {
                            Result.HorizontalAlignment = HorizontalAlignment.Center;
                            this.Width = 500;
                            Result.Content = "Too massive number";
                        }

                        else if (result.ToString().Length > 12)
                        {
                            this.Width = 500;
                            Result.HorizontalAlignment = HorizontalAlignment.Right;
                            Result.Content = result.ToString();
                        }

                        else Result.Content = result.ToString();
                    }

                    catch
                    {
                        Result.HorizontalAlignment = HorizontalAlignment.Center;
                        this.Width = 500;
                        Result.Content = "Too massive number";
                    }
                }
                
            }
        }

        private bool ZeroHandler() 
        {
            expression = Solution.Content.ToString();
            bool res = false;

            if (expression[expression.Length - 1] == '0' && expression[expression.Length - 2] == '/') 
            {
                Result.Content = "Infinity";
                res = true;
            }

            return res;
        }

        private bool AnalizeFranction() 
        {
            expression = Solution.Content.ToString();
            string temp = "";
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                if (expression[i] == '+' || expression[i] == '-' || expression[i] == '/' || expression[i] == '*') break;
                else temp += expression[i];
            }

            bool result = temp.Contains(".");
            return result;
        }

        private void BtnPercentClick(object sender, RoutedEventArgs e)
        {
            BtnEqualClick(sender, e);
            try
            {
                decimal temp = Convert.ToDecimal((Result.Content.ToString()));
                temp = temp / 100;
                Result.Content = temp.ToString();
            }
            catch 
            {
                Result.HorizontalAlignment = HorizontalAlignment.Right;
                Result.Content = "Error"; 
            }
        } 
            
        private void CheckSymbol(string symb = "") 
        {
            expression = Solution.Content.ToString();
            if (expression[expression.Length - 1] == '+' || expression[expression.Length - 1] == '/' ||
                expression[expression.Length - 1] == '*' || expression[expression.Length - 1] == '-' ||
                expression[expression.Length - 1] == '.' || expression[expression.Length - 1] == '%')
            {
                expression = expression.Remove(expression.Length - 1, 1);
                Solution.Content = expression;
            }

            if (symb != "")
            {
                expression += symb;
                Solution.Content = expression;
            }
        }

        private void ChangeLastZero(string number) 
        {
            expression = Solution.Content.ToString();
            if (expression == "0") expression = expression.Remove(0, 1);
            expression += number;

            Solution.Content = expression;
        }
    }
}
