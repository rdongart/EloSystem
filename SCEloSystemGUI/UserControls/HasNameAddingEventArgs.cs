using EloSystem;

namespace SCEloSystemGUI.UserControls
{
    public class HasNameAddingEventArgs
    {
        internal IHasName Content { get; private set; }

        internal HasNameAddingEventArgs(IHasName content)
        {
            this.Content = content;
        }
    }
}
