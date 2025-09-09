# message-sent-and-delivered-status-in-net-maui-chat
This example demonstrates how to add ticks in message item to indicate the message delivered status using custom logic in .NET MAUI Chat(SfChat).

## Sample

```xaml

OutgoingMessageTemplate: 

    <ContentView.Resources>
        <ResourceDictionary>
            <convertor:OutgoingMessageSeenIconColorConverter x:Key="OutgoingMessageSeenIconColorConverter"/>
        </ResourceDictionary>
    </ContentView.Resources>
    
    <ContentView.Content>

                   ....

            <!-- Outgoing Message Bubble -->
            <Frame Grid.Row="0"
                   Grid.Column="0"
                   BackgroundColor="#DCF8C6"
                   CornerRadius="20"
                   Padding="12,8"
                   HasShadow="False"
                   BorderColor="Transparent">
                    <!-- Message Text -->
                    <Label Grid.Column="0"
                           Text="{Binding Text}"
                           TextColor="#212121"
                           FontFamily="Roboto"
                           FontSize="15"
                           LineBreakMode="WordWrap"
                           VerticalTextAlignment="End"
                           HorizontalTextAlignment="Start" />

                    <!-- Seen Icon -->
                    <Label Grid.Column="1"
                           Text="&#xe727;"
                           FontFamily="MauiSampleFontIcon"
                           FontSize="12"
                           VerticalOptions="End"
                           HorizontalOptions="End"
                           Margin="6,0,0,0"
                           TextColor="{Binding IsMessageSeen, Converter={StaticResource OutgoingMessageSeenIconColorConverter}}" />
            </Frame>
               ....

    </ContentView.Content>

View Model:

    public class ViewModel : INotifyPropertyChanged
    {
    
        private ICommand sendMessageCommand;

        internal SfChat chat;


        public ViewModel()
        {
            this.messages = new ObservableCollection<object>();
            this.currentUser = new Author() { Name = "Nancy", Avatar = "peoplecircle16.png" };
            this.GenerateMessages();
            SendMessageCommand = new Command<object>(OnSendMessage);
        }

        public ObservableCollection<object> Messages
        {
            get
            {
                return this.messages;
            }

            set
            {
                this.messages = value;
            }
        }

        public bool IsMessageSeen
        {
            get
            {
                return this.isMessageSeen;
            }
            set
            {
                this.isMessageSeen = value;
                RaisePropertyChanged(nameof(IsMessageSeen));
            }
        }

        public ICommand SendMessageCommand
        {
            get
            {
                return this.sendMessageCommand;
            }
            set
            {
                this.sendMessageCommand = value;
                RaisePropertyChanged("SendMessageCommand");
            }
        }

        private async void OnSendMessage(object obj)
        {
            var e = obj as Syncfusion.Maui.Chat.SendMessageEventArgs;
            e.Handled = true;
            if (e.Message is TextMessage message && !string.IsNullOrWhiteSpace(message.Text))
            {
                var data = new TextMessageExt()
                {
                    Author = CurrentUser,
                    Text = message.Text,
                    IsMessageSeen = false,
                };

                Messages.Add(data);

                chat.Editor.Text = string.Empty;

                // Simulate a delay before marking message as seen
                await Task.Delay(2000);
                data.IsMessageSeen = true;
            }
        }

        private void GenerateMessages()
        {
            this.messages.Add(new TextMessageExt()
            {
                Author = currentUser,
                Text = "Hi guys, good morning! I'm very delighted to share with you the news that our team is going to launch a new mobile application.",
                IsMessageSeen = true,
            });

            this.messages.Add(new TextMessageExt()
            {
                Author = new Author() { Name = "Andrea", Avatar = "Andrea.png" },
                Text = "Oh! That's great.",
            });

            this.messages.Add(new TextMessageExt()
            {
                Author = new Author() { Name = "Harrison", Avatar = "Harrison.png" },
                Text = "That is good news.",
            });

            this.messages.Add(new TextMessageExt()
            {
                Author = new Author() { Name = "Margaret", Avatar = "Margaret.png" },
                Text = "Are we going to develop the app natively or hybrid?"
            });
        }
    }

    public class TextMessageExt : TextMessage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool isMessageSeen;
        public bool IsMessageSeen
        {
            get
            {
                return isMessageSeen;
            }
            set
            {
                isMessageSeen = value;
                RaisePropertyChanged(nameof(IsMessageSeen));
            }
        }
        public void RaisePropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
    
Converter:

    internal class OutgoingMessageSeenIconColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value != null && (bool)value)
            {
                return Colors.Blue;
            }

            return Colors.DarkGray;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

```
## Requirements to run the demo

To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements)

## Troubleshooting:
### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

## License

Syncfusion® has no liability for any damage or consequence that may arise from using or viewing the samples. The samples are for demonstrative purposes. If you choose to use or access the samples, you agree to not hold Syncfusion® liable, in any form, for any damage related to use, for accessing, or viewing the samples. By accessing, viewing, or seeing the samples, you acknowledge and agree Syncfusion®'s samples will not allow you seek injunctive relief in any form for any claim related to the sample. If you do not agree to this, do not view, access, utilize, or otherwise do anything with Syncfusion®'s samples.
