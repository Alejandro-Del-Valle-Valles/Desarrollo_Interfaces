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

        private void OnNumberClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int inputNumber = int.Parse(button.Text);
            UpdateOperationConsole(inputNumber);
        }

        private void OnSignClicked(object sender, EventArgs e)
        {

        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            currentInput = DEFAULT_MESSAGE;
            operationLabel.Text = DEFAULT_MESSAGE;
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            if(currentInput != DEFAULT_MESSAGE && currentInput != "")
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                operationLabel.Text = currentInput;
        }

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


        private void OnComaClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(currentInput) && currentInput != DEFAULT_MESSAGE)
            {
                char lastNumber = currentInput[currentInput.Length - 1];
                if (char.IsDigit(lastNumber))
                {
                    float floatedLastNumber = (float)lastNumber;
                    currentInput = currentInput.Remove(lastNumber);
                    currentInput += floatedLastNumber;
                    UpdateOperationConsole(currentInput, false);
                    //TODO: FALLA
                }
            }
        }

        private void OnEqualCliked(object sender, EventArgs e)
        {

        }

        private void UpdateOperationConsole(string newImput, bool append = true)
        {
            if (currentInput == DEFAULT_MESSAGE) currentInput = "";
            if(append) currentInput += newImput;
            operationLabel.Text = currentInput;
        }

        private void UpdateOperationConsole(int newInput, bool append = true)
        {
            if (currentInput == DEFAULT_MESSAGE) currentInput = "";
            if(append) currentInput += newInput;
            operationLabel.Text = currentInput;
        }
    }
}
