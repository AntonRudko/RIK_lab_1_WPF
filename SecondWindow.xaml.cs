using System;
using System.IO;
using System.Windows;
using Lab1_RIK;

namespace Lab1_RIK
{
    public partial class SecondWindow : Window
    {
        private string userName;
        private string email;

        public SecondWindow(string name, string email)
        {
            InitializeComponent();

            userName = name;
            this.email = email;

            NameTextBlock.Text = "Ім'я: " + userName;
            EmailTextBlock.Text = "Email: " + email;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = "SurveyResults.txt";

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Ім'я: " + userName);
                    writer.WriteLine("Email: " + email);
                    writer.WriteLine("Відгук: " + FeedbackTextBox.Text);
                    writer.WriteLine("Дата: " + DateTime.Now);
                    writer.WriteLine("----------------------------");
                }

                MessageBox.Show("Дані успішно збережено у файл SurveyResults.txt");

                FeedbackTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при збереженні: " + ex.Message);
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
