using EloSystem;

namespace SCEloSystemGUI.UserControls
{
    public delegate bool ContentRemoveCondition<T>(T content) where T : EloSystemContent;
}
