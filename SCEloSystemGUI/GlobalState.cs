using SCEloSystemGUI.UserControls;
using EloSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEloSystemGUI
{
    internal static class GlobalState
    {
        internal static EloData DataBase { get; private set; }
        internal static RankHandler RankSystem { get; private set; }

        internal static void Initialize(EloData dataBase)
        {
            GlobalState.DataBase = dataBase;
            GlobalState.RankSystem = new RankHandler(GlobalState.DataBase);
        }
    }
}
