using System;

namespace SCEloSystemGUI
{
    interface IFilter
    {
        event EventHandler FilterChanged;

        bool HasChangesNotApplied();

        void ApplyChanges();
    }
}
