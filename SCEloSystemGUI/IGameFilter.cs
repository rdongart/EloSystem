using EloSystem;

namespace SCEloSystemGUI
{
    interface IGameFilter : IFilter
    {
        bool FilterGame(Game game);
    }
}
