using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PocketBar.Utils
{
	public class TypeAssistant
    {
        public delegate void TextChangedEventHandler(string text);
        public event TextChangedEventHandler OnFinishedTyping;
        private string Text = "";
        public int WaitingMilliSeconds { get; set; }
        System.Threading.Timer waitingTimer;

        public TypeAssistant(int waitingMilliSeconds = 600)
        {
            WaitingMilliSeconds = waitingMilliSeconds;
            waitingTimer = new Timer(p =>
            {
                OnFinishedTyping(Text);
            });
        }
        public void TextChanged(string text)
        {
            this.Text = text;
            waitingTimer.Change(WaitingMilliSeconds, System.Threading.Timeout.Infinite);
        }
    }
}
