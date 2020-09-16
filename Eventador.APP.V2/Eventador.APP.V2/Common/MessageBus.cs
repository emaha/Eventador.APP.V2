﻿using System;
using Xamarin.Forms;

namespace Eventador.APP.V2.Common
{
    public class MessageBus
    {
        private static readonly Lazy<MessageBus> LazyInstance = new Lazy<MessageBus>(() => new MessageBus(), true);
        static MessageBus Instance => LazyInstance.Value;

        private MessageBus()
        {
        }

        public static void SendMessage(string message)
        {
            MessagingCenter.Send(Instance, message);
        }

        public static void SendMessage<TArgs>(string message, TArgs args)
        {
            MessagingCenter.Send(Instance, message, args); 
        }
    }
}