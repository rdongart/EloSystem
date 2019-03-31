using EloSystem;
using System;

namespace SCEloSystemGUI
{
    interface IPlayerFilter
    {
        event EventHandler FilterChanged;

        bool HasChangesNotApplied();

        bool PlayerFilter(SCPlayer player);

        void ApplyChanges();
    }
}
