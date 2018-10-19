using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
