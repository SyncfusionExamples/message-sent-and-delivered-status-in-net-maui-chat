using Syncfusion.Maui.Chat;

namespace MauiChat
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.sfChat.MessageTemplate = new ChatMessageTemplateSelectorExt(this.sfChat);
            viewModel.chat = this.sfChat;
        }
    }
}
