using Microsoft.Win32;
using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace EmailSender
{
    public partial class MainWindow : Window
    {
        private string attachmentFilePath = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AttachFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                attachmentFilePath = openFileDialog.FileName;
                AttachmentLabel.Text = $"Додано: {attachmentFilePath}";
            }
        }

        private void SendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            string senderEmail = SenderEmailTextBox.Text;
            string password = PasswordBox.Password;
            string receiverEmail = ReceiverEmailTextBox.Text;
            string subject = SubjectTextBox.Text;
            string body = BodyTextBox.Text;
            bool isImportant = IsImportantCheckBox.IsChecked == true;

            try
            {
                SendEmail(senderEmail, password, receiverEmail, subject, body, attachmentFilePath, isImportant);
                MessageBox.Show("Повідомлення відправлено успішно", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при відправці: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SendEmail(string senderEmail, string password, string receiverEmail, string subject, string body, string attachmentPath, bool isImportant)
        {
            SmtpClient smtpClient = new SmtpClient("wodof62278@cironex.com", 587)
            {
                Credentials = new NetworkCredential("maksimkin268@gmail.com", "gfll mqzt guhg altv"),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            if (!string.IsNullOrEmpty(attachmentPath))
            {
                Attachment attachment = new Attachment(attachmentPath);
                mailMessage.Attachments.Add(attachment);
            }

            if (isImportant)
            {
                mailMessage.Priority = MailPriority.High;
            }

            smtpClient.Send(mailMessage);
        }
    }
}