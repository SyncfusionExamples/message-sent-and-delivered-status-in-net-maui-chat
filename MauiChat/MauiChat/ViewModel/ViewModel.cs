using Syncfusion.Maui.Chat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiChat
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Collection of messages or conversation.
        /// </summary>
        private ObservableCollection<object> messages;

        /// <summary>
        /// Value indicating whether the message has been seen by the recipient.
        /// </summary>
        private bool isMessageSeen = true;

        /// <summary>
        /// current user of chat.
        /// </summary>
        private Author currentUser;

        /// <summary>
        /// Represents the command used to send a message.
        /// </summary>
        private ICommand sendMessageCommand;

        internal SfChat chat;

        #endregion

        #region Constructor
        public ViewModel()
        {
            this.messages = new ObservableCollection<object>();
            this.currentUser = new Author() { Name = "Nancy", Avatar = "peoplecircle16.png" };
            this.GenerateMessages();
            SendMessageCommand = new Command<object>(OnSendMessage);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the group message conversation.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether the message has been seen by the recipient.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the current author.
        /// </summary>
        public Author CurrentUser
        {
            get
            {
                return this.currentUser;
            }
            set
            {
                this.currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        /// <summary>
        /// Gets or sets the command used to send a message.
        /// </summary>
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

        #endregion

        #region INotifyPropertyChanged
        /// <summary>
        /// Property changed handler.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Occurs when property is changed.
        /// </summary>
        /// <param name="propName">changed property name</param>
        public void RaisePropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /// <summary>
        /// Handles the logic for sending a chat message.
        /// </summary>
        /// <param name="obj"></param>
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

        #endregion

        #region Message Generation
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

            this.messages.Add(new TextMessageExt()
            {
                Author = currentUser,
                Text = "We should develop this app in .NET MAUI, since it provides native experience and perfomance as well as allowing for seamless cross-platform development.",
                IsMessageSeen = true,
            });
            this.messages.Add(new TextMessageExt()
            {
                Author = new Author() { Name = "Margaret", Avatar = "Margaret.png" },
                Text = "I haven't heard of .NET MAUI. What's .NET MAUI?",
            });
            this.messages.Add(new TextMessageExt()
            {
                Author = currentUser,
                Text = ".NET MAUI is a new library that lets you build native UIs for Android, iOS, macOS, and Windows from one shared C# codebase.",
                IsMessageSeen = true,
            });
        }
        #endregion
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
}
