using EloSystem;

namespace SCEloSystemGUI.UserControls
{
    public delegate bool HasNameContentRemoveCondition<T>(T content) where T : HasNameContent;
}
