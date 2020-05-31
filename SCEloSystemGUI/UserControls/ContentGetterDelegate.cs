using EloSystem;
using System.Collections.Generic;

namespace SCEloSystemGUI.UserControls
{
    public delegate IEnumerable<T> ContentGetterDelegate<T>() where T : EloSystemContent;
}
