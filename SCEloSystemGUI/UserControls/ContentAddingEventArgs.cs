using System;

namespace SCEloSystemGUI.UserControls
{
    internal class ContentAddingEventArgs : EventArgs
    {
        internal ContentAdder ContentAdder { get; private set; }

        internal ContentAddingEventArgs(ContentAdder adder)
        {
            this.ContentAdder = adder;
        }
    }
}
