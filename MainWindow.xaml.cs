using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab1_RIK
{
    public partial class MainWindow : Window
    {
        private const string NamePlaceholder = "ім'я";
        private const string EmailPlaceholder = "example@email.com";

        public MainWindow()
        {
            InitializeComponent();
            NextButton.IsEnabled = false;

            SetPlaceholder(NameTextBox, NamePlaceholder);
            SetPlaceholder(EmailTextBox, EmailPlaceholder);
        }

        private void UpdateButtonState()
        {
            bool nameValid = NameTextBox.Tag?.ToString() == "";
            bool emailValid = EmailTextBox.Tag?.ToString() == "";

            NextButton.IsEnabled = nameValid && emailValid;

            if (NextButton.IsEnabled)
                NextButton.Background = Brushes.LightGreen;
            else
                NextButton.ClearValue(Button.BackgroundProperty);
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = NameTextBox.Text;

            if (name == NamePlaceholder)
                return;

            if (name.Length == 0)
                SetError(NameTextBox, "Ім'я не повинно бути порожнім");
            else if (name.Length < 2)
                SetError(NameTextBox, "Ім'я повинно містити мінімум 2 символи");
            else if (name.StartsWith(" "))
                SetError(NameTextBox, "Ім'я не повинно починатися з пробілу");
            else if (name.Any(char.IsDigit))
                SetError(NameTextBox, "Ім'я не повинно містити цифри");
            else if (!name.All(c => char.IsLetter(c) || c == ' '))
                SetError(NameTextBox, "Ім'я не повинно містити спеціальні символи");
            else
                ClearError(NameTextBox);

            UpdateButtonState();
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string email = EmailTextBox.Text;

            if (email == EmailPlaceholder)
                return;

            if (email.Length == 0)
                SetError(EmailTextBox, "Email не повинен бути пустим");
            else if (email.Contains(" "))
                SetError(EmailTextBox, "Email не повинен містити пробіли");
            else if (!email.Contains("@"))
                SetError(EmailTextBox, "Email повинен містити символ @");
            else if (email.Count(c => c == '@') != 1)
                SetError(EmailTextBox, "Email повинен містити лише один символ @");
            else if (!email.Contains("."))
                SetError(EmailTextBox, "Email повинен містити символ .");
            else
                ClearError(EmailTextBox);

            UpdateButtonState();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = NameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();

            SecondWindow secondWindow = new SecondWindow(userName, email);
            secondWindow.Show();
            this.Hide();
        }

        private void SetError(TextBox textBox, string message)
        {
            textBox.BorderBrush = Brushes.Red;
            textBox.ToolTip = message;
            textBox.Tag = "error";
        }

        private void ClearError(TextBox textBox)
        {
            textBox.ClearValue(TextBox.BorderBrushProperty);
            textBox.ToolTip = null;
            textBox.Tag = "";
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.Foreground = Brushes.Gray;

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.Foreground = Brushes.Black;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.Foreground = Brushes.Gray;
                }
            };
        }
    }
}
