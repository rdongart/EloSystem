using EloSystem;
using System.Collections.Generic;

namespace SCEloSystemGUI.UserControls
{
    public delegate IEnumerable<T> HasNameContentGetterDelegate<T>() where T : HasNameContent;
}
