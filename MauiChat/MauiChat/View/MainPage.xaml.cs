using Syncfusion.Maui.Chat;

namespace MauiChat
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.sfChat.MessageTemplate = new ChatMessageTemplateSelectorExt(this.sfChat);
        }

        private async void sfChat_SendMessage(object sender, SendMessageEventArgs e)
        {
            e.Handled = true;
            var data = new TextMessageExt()
            {
                Author = viewModel.CurrentUser,
                Text = e.Message!.Text,
                IsMessageSeen = false,
            };

            viewModel.Messages.Add(data);

            sfChat.Editor.Text = "";

            await Task.Delay(2000);
            data.IsMessageSeen = true;
        }
    }
}
