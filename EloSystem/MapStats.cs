﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace EloSystem
{
    public enum Matchup
    {
        TvZ, ZvP, PvT, ZvZ, TvT, PvP, RvZ, RvT, RvP, RvR
    }

    [Serializable]
    public class MapStats : ISerializable
    {
        private const int PvT_INDEX = 0;
        private const int PvZ_INDEX = 1;
        private const int PvR_INDEX = 2;
        private const int ZvT_INDEX = 3;
        private const int ZvR_INDEX = 4;
        private const int TvR_INDEX = 5;
        private const int ZvZ_INDEX = 6;
        private const int TvT_INDEX = 7;
        private const int PvP_INDEX = 8;
        private const int RvR_INDEX = 9;

        private List<RaceMatchupResults> raceMatchupData;
        public RaceMatchupResults PvT { get { return this.raceMatchupData[MapStats.PvT_INDEX]; } }
        public RaceMatchupResults PvZ { get { return this.raceMatchupData[MapStats.PvZ_INDEX]; } }
        public RaceMatchupResults PvR { get { return this.raceMatchupData[MapStats.PvR_INDEX]; } }
        public RaceMatchupResults ZvT { get { return this.raceMatchupData[MapStats.ZvT_INDEX]; } }
        public RaceMatchupResults ZvR { get { return this.raceMatchupData[MapStats.ZvR_INDEX]; } }
        public RaceMatchupResults TvR { get { return this.raceMatchupData[MapStats.TvR_INDEX]; } }
        public RaceMatchupResults ZvZ { get { return this.raceMatchupData[MapStats.ZvZ_INDEX]; } }
        public RaceMatchupResults TvT { get { return this.raceMatchupData[MapStats.TvT_INDEX]; } }
        public RaceMatchupResults PvP { get { return this.raceMatchupData[MapStats.PvP_INDEX]; } }
        public RaceMatchupResults RvR { get { return this.raceMatchupData[MapStats.RvR_INDEX]; } }

        internal MapStats()
        {
            this.raceMatchupData = (new Tuple<Race, Race>[] {Tuple.Create(Race.Protoss, Race.Terran), Tuple.Create(Race.Protoss, Race.Zerg), Tuple.Create(Race.Protoss, Race.Random)
                , Tuple.Create(Race.Zerg, Race.Terran), Tuple.Create(Race.Zerg, Race.Random), Tuple.Create(Race.Terran, Race.Random), Tuple.Create(Race.Zerg, Race.Zerg), Tuple.Create(Race.Terran, Race.Terran), Tuple.Create(Race.Protoss, Race.Protoss), Tuple.Create(Race.Random, Race.Random)}).Select(tpl => new RaceMatchupResults(tpl.Item1, tpl.Item2)).ToList();

        }

        #region Implementing ISerializable
        private enum Field
        {
            RaceMatchupData
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.RaceMatchupData.ToString(), (List<RaceMatchupResults>)this.raceMatchupData);
        }
        internal MapStats(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.RaceMatchupData: this.raceMatchupData = (List<RaceMatchupResults>)info.GetValue(field.ToString(), typeof(List<RaceMatchupResults>)); break;
                    }
                }

            }
        }
        #endregion

        internal void ReportMatch(Race racePlayer1, Race racePlayer2, Race raceWinner, int player1Rating, int player2Rating)
        {
            RaceMatchupResults matchup = this.raceMatchupData.FirstOrDefault(m => m.IsMatchupFor(racePlayer1, racePlayer2));

            if (matchup != null)
            {
                if (racePlayer1 == matchup.Race1) { matchup.AddMatchupData(raceWinner, player1Rating, player2Rating); }
                else { matchup.AddMatchupData(raceWinner, player2Rating, player1Rating); }
            }

        }

        internal void RollBackGameResult(Race racePlayer1, Race racePlayer2, Race raceWinner, int race1Rating, int race2Rating)
        {
            RaceMatchupResults matchup = this.raceMatchupData.FirstOrDefault(m => m.IsMatchupFor(racePlayer1, racePlayer2));

            if (matchup != null)
            {
                if (racePlayer1 == matchup.Race1) { matchup.RollBackGameResult(raceWinner, race1Rating, race2Rating); }
                else { matchup.RollBackGameResult(raceWinner, race2Rating, race1Rating); }
            }

        }

        public bool TryGetMatchup(Race race1, Race race2, out RaceMatchupResults data)
        {
            data = this.raceMatchupData.FirstOrDefault(m => m.IsMatchupFor(race1, race2));

            return data != null;
        }

        public bool TryGetMatchup(Matchup matchup, out RaceMatchupResults data)
        {
            switch (matchup)
            {
                case Matchup.ZvP: return this.TryGetMatchup(Race.Zerg, Race.Protoss, out data);
                case Matchup.PvT: return this.TryGetMatchup(Race.Protoss, Race.Terran, out data);
                case Matchup.TvZ: return this.TryGetMatchup(Race.Terran, Race.Zerg, out data);
                case Matchup.RvZ: return this.TryGetMatchup(Race.Random, Race.Zerg, out data);
                case Matchup.RvP: return this.TryGetMatchup(Race.Random, Race.Protoss, out data);
                case Matchup.RvT: return this.TryGetMatchup(Race.Random, Race.Terran, out data);
                case Matchup.ZvZ: return this.TryGetMatchup(Race.Zerg, Race.Zerg, out data);
                case Matchup.PvP: return this.TryGetMatchup(Race.Protoss, Race.Protoss, out data);
                case Matchup.TvT: return this.TryGetMatchup(Race.Terran, Race.Terran, out data);
                case Matchup.RvR: return this.TryGetMatchup(Race.Random, Race.Random, out data);
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Matchup).Name, matchup.ToString()));
            }
        }

        public int TotalGames()
        {
            return this.raceMatchupData.Sum(stats => stats.TotalGames);
        }
    }
}
