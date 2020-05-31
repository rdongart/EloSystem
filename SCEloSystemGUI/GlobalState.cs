using EloSystem;
using EloSystemExtensions;

namespace SCEloSystemGUI
{
    internal static class GlobalState
    {
        internal static EloData DataBase { get; private set; }
        internal static RankHandler RankSystem { get; private set; }
        internal static MirrorMatchupEvaluater MirrorMatchupEvaluation { get; private set; }

        internal static void Initialize(EloData dataBase)
        {
            GlobalState.DataBase = dataBase;

            GlobalState.RankSystem = new RankHandler(GlobalState.DataBase)
            {
                GamesPlayedThreshold = EloSystemGUIStaticMembers.GAMESPLAYED_DEFAULT_THRESHOLD,
                GamesPlayedVsRaceThreshold = EloSystemGUIStaticMembers.GAMESPLAYED_DEFAULT_RACE_THRESHOLD,
                RecentActivityGamesPlayedThreshold = EloSystemGUIStaticMembers.RECENTACTIVITY_GAMESPLAYED_DEFAULT_THRESHOLD,
                RecentActivityGamesPlayedVsRaceThreshold = EloSystemGUIStaticMembers.RECENTACTIVITY_GAMESPLAYED_DEFAULT_RACE_THRESHOLD,
                RecentActivityMonths = EloSystemGUIStaticMembers.RECENTACTIVITY_MONTHS_DEFAULT
            };

            GlobalState.MirrorMatchupEvaluation = new MirrorMatchupEvaluater(GlobalState.DataBase);
        }
    }
}
