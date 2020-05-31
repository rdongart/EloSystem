using EloSystem;
using System;

namespace EloSystemExtensions
{
    public static class GameExtensions
    {
        public static SCPlayer GetPlayer(this Game game, PlayerSlotType playerSlot)
        {
            switch (playerSlot)
            {
                case PlayerSlotType.Player1: return game.Player1;
                case PlayerSlotType.Player2: return game.Player2;
                default: throw new Exception(string.Format("Unknown {0} {1}.", typeof(PlayerSlotType).Name, playerSlot.ToString()));
            }
        }

        public static Matchup ToMatchupType(this MirrorMatchup mm)
        {
            switch (mm)
            {
                case MirrorMatchup.ZvZ: return Matchup.ZvZ;
                case MirrorMatchup.TvT: return Matchup.TvT;
                case MirrorMatchup.PvP: return Matchup.PvP;
                case MirrorMatchup.RvR: return Matchup.RvR;
                default: throw new Exception(String.Format("Unknown  {0} {1}.", typeof(Matchup).Name, mm.ToString()));
            }
        }
    }
}
