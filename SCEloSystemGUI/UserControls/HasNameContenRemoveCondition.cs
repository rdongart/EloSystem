using EloSystem;

namespace SCEloSystemGUI.UserControls
{
    public delegate bool HasNameContenRemoveCondition<T>(T content) where T : HasNameContent;
}
