using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PocketBar.Utils
{
	public class TypeAssistant
    {
        public delegate void TextChangedEventHandler();
        public event TextChangedEventHandler OnFinishedTyping;
        public int WaitingMilliSeconds { get; set; }
        System.Threading.Timer waitingTimer;

        public TypeAssistant(int waitingMilliSeconds = 600)
        {
            WaitingMilliSeconds = waitingMilliSeconds;
            waitingTimer = new Timer(p =>
            {
                OnFinishedTyping();
            });
        }
        public void TextChanged()
        {
            waitingTimer.Change(WaitingMilliSeconds, System.Threading.Timeout.Infinite);
        }
    }
}
