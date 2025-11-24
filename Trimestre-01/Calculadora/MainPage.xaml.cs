using System.Text.RegularExpressions;

namespace Calculadora
{
    public partial class MainPage : ContentPage
    {
        private static string DEFAULT_MESSAGE = "Operación";
        private string currentInput = "";
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Añade a la consola el número pulsado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNumberClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string inputNumber = button.Text;
            UpdateOperationConsole(inputNumber);
        }

        /// <summary>
        /// Añade a la consola la operacion a realizar (Suma, resta, etc)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSignClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string sign = button.Text;

            // Normalizar el símbolo de multiplicación y potencia
            if (sign == "X") sign = "*";
            if (sign == "Pow") sign = "^";

            if (string.IsNullOrWhiteSpace(currentInput) || currentInput == DEFAULT_MESSAGE)
                return;

            // Evitar operadores consecutivos
            if (Regex.IsMatch(currentInput[^1].ToString(), @"[\+\-\*/\^]"))
                // Reemplazar el último operador por el nuevo
                currentInput = currentInput.Substring(0, currentInput.Length - 1) + sign;
            else currentInput += sign;

            operationLabel.Text = currentInput;
        }

        /// <summary>
        /// Limpia la consola cuando se pulsa el botón de A o de AC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClearClicked(object sender, EventArgs e)
        {
            currentInput = DEFAULT_MESSAGE;
            operationLabel.Text = DEFAULT_MESSAGE;
        }

        /// <summary>
        /// Elimina el último caracter de la consola
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeleteClicked(object sender, EventArgs e)
        {
            if(currentInput != DEFAULT_MESSAGE && currentInput != "")
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                operationLabel.Text = currentInput;
        }

        /// <summary>
        /// Cambia entre positivio y negativo el último número.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChangeSymbolClicked(object sender, EventArgs e)
        {
            if (currentInput != DEFAULT_MESSAGE && !string.IsNullOrWhiteSpace(currentInput))
            {
                int lastIndex = currentInput.Length - 1;
                string totalNumber = "";
                int startIndex = -1;

                for (int i = lastIndex; i >= 0; i--)
                {
                    char c = currentInput[i];
                    if (char.IsDigit(c))
                    {
                        totalNumber = c + totalNumber;
                        startIndex = i;
                    }
                    else if (c == '-' && startIndex == i + 1)
                    {
                        totalNumber = "-" + totalNumber;
                        startIndex = i;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                if (startIndex >= 0 && int.TryParse(totalNumber, out int parsed))
                {
                    int changed = -parsed;
                    string newInput = currentInput.Substring(0, startIndex) + changed.ToString();
                    currentInput = newInput;
                    operationLabel.Text = currentInput;
                }
            }
        }

        /// <summary>
        /// Convierte el último número en decimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnComaClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(currentInput) && currentInput != DEFAULT_MESSAGE)
            {
                // Buscar el último número completo (puede tener coma o punto)
                var match = Regex.Match(currentInput, @"(\d+[.,]?\d*)(?!.*\d)");
                if (match.Success)
                {
                    string lastNumberStr = match.Value;

                    // Verificar si ya tiene coma o punto decimal
                    if (!lastNumberStr.Contains(",") && !lastNumberStr.Contains("."))
                    {
                        // Añadir coma decimal al final del número
                        string newNumber = lastNumberStr + ",";
                        currentInput = currentInput.Substring(0, match.Index) + newNumber;
                        UpdateOperationConsole(currentInput, false);
                    }
                }
            }
        }

        /// <summary>
        /// Realiza la operación y muestra el resultado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEqualCliked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentInput) || currentInput == DEFAULT_MESSAGE)
                return;

            try
            {
                string expression = currentInput.Replace(",", "."); // Usar punto decimal

                double result = EvaluateExpression(expression);
                operationLabel.Text = result.ToString();
                currentInput = result.ToString();
            }
            catch
            {
                operationLabel.Text = "Error";
                currentInput = "";
            }
        }

        // Evaluador simple para operaciones básicas y potencia
        private double EvaluateExpression(string expr)
        {
            // Solo soporta una operación (ejemplo: 2+3, 4*5, 2^3)
            var match = Regex.Match(expr, @"^(-?\d+(\.\d+)?)([\+\-\*/\^])(-?\d+(\.\d+)?)$");
            if (!match.Success) throw new FormatException();

            double left = double.Parse(match.Groups[1].Value);
            string op = match.Groups[3].Value;
            double right = double.Parse(match.Groups[4].Value);

            return op switch
            {
                "+" => left + right,
                "-" => left - right,
                "*" => left * right,
                "/" => right != 0 ? left / right : throw new DivideByZeroException(),
                "^" => Math.Pow(left, right),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Actualiza la consola con el nuevo caracter introducido
        /// </summary>
        /// <param name="newInput">String caracter introducido</param>
        /// <param name="append">Bool, True por defecto para añadir el valor, false para cambiar
        ///  todo lo de la consola por el nuevo input</param>
        private void UpdateOperationConsole(string newInput, bool append = true)
        {
            if (currentInput == DEFAULT_MESSAGE) currentInput = "";
            if(append) currentInput += newInput;
            operationLabel.Text = currentInput;
        }

        /// <summary>
        /// Actualiza la consola con el nuevo caracter introducido
        /// </summary>
        /// <param name="newInput">Int numero introducido</param>
        /// <param name="append">Bool, True por defecto para añadir el valor, false para cambiar
        ///  todo lo de la consola por el nuevo input</param>
        private void UpdateOperationConsole(int newInput, bool append = true)
        {
            if (currentInput == DEFAULT_MESSAGE) currentInput = "";
            if(append) currentInput += newInput;
            operationLabel.Text = currentInput;
        }
    }
}
