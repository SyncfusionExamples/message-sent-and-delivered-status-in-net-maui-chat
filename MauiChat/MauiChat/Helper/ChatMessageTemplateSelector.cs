using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Maui.Chat;
using Syncfusion.Maui.Chat.Internals;

namespace MauiChat
{
    public class ChatMessageTemplateSelectorExt : ChatMessageTemplateSelector
    {
        #region Fields

        private readonly DataTemplate? outgoingDataTemplate;
        private readonly DataTemplate? inComingDataTemplate;

        #endregion

        #region Public Properties

        public SfChat? Chat { get; set; }
        #endregion

        #region Constructor

        public ChatMessageTemplateSelectorExt(SfChat chat) : base(chat)
        {
            this.Chat = chat;
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingMessageTemplate));
            this.inComingDataTemplate = new DataTemplate(typeof(IncomingMessageTemplate));
        }

        #endregion

        #region Override methods
        protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
        {
            var message = item as IMessage;
            if (message == null || Chat == null)
            {
                return null;
            }

            if (message.Author == Chat!.CurrentUser)
            {
                return outgoingDataTemplate;
            }
            else
            {
                return inComingDataTemplate;
            }
        }
        #endregion
    }
}
