using System;
using System.Drawing;

namespace SCEloSystemGUI.UserControls
{
    interface IContentAdder
    {
        ContentTypes ContentType { get; }
        event EventHandler<ContentAddingEventArgs> OnAddPlayer;
        Image NewImage { get; }
        string ContentName { get; }
    }
}
