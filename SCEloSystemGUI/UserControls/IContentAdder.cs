using System;
using System.Drawing;

namespace SCEloSystemGUI.UserControls
{
    interface IContentAdder
    {
        ContentTypes ContentType { get; }
        event EventHandler<ContentAddingEventArgs> OnAddButtonClick;
        Image SelectedImage { get; }
        string ContentName { get; }
    }
}
