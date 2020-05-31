using EloSystem;
using System;

namespace SCEloSystemGUI
{
    interface IPlayerFilter : IFilter
    {
        bool PlayerFilter(SCPlayer player);
    }
}
