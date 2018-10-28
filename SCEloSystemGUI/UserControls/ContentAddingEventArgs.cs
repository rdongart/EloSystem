using System;

namespace SCEloSystemGUI.UserControls
{
    public class ContentAddingEventArgs : EventArgs
    {
        internal IContentAdder ContentAdder { get; private set; }

        internal ContentAddingEventArgs(IContentAdder adder)
        {
            this.ContentAdder = adder;
        }
    }
}
