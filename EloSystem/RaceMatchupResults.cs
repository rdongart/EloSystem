﻿using System;
using System.Runtime.Serialization;

namespace EloSystem
{
    [Serializable]
    public class RaceMatchupResults : ISerializable
    {
        public Race Race1 { get; private set; }
        public Race race2 { get; private set; }
        public int Race1Wins { get; private set; }
        public int Race2Wins { get; private set; }
        public int TotalGames
        {
            get
            {
                return this.Race1Wins + this.Race2Wins;
            }
        }
        private double race1ExpWinRatio;

        internal RaceMatchupResults(Race race1, Race race2)
        {
            this.Race1 = race1;
            this.race2 = race2;
        }

        private static double CorrectedWinRatio(double expectedWinRatio, double actualWinRatio)
        {
            if (expectedWinRatio > actualWinRatio) { return EloData.EXP_WIN_RATIO_EVEN_RATING - ((expectedWinRatio - actualWinRatio) / expectedWinRatio) * EloData.EXP_WIN_RATIO_EVEN_RATING; }
            else if ((expectedWinRatio < actualWinRatio))
            {
                return EloData.EXP_WIN_RATIO_EVEN_RATING + ((actualWinRatio - expectedWinRatio) / (EloData.EXP_WIN_RATIO_MAX - expectedWinRatio)) * EloData.EXP_WIN_RATIO_EVEN_RATING;
            }
            else { return EloData.EXP_WIN_RATIO_EVEN_RATING; }

        }

        #region Implementing ISerializable
        private enum Field
        {
            Race1, Race1ExpWinRatio, Race1Wins, Race2, Race2Wins
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(Field.Race1.ToString(), (byte)this.Race1);
            info.AddValue(Field.Race1ExpWinRatio.ToString(), (double)this.race1ExpWinRatio);
            info.AddValue(Field.Race1Wins.ToString(), (int)this.Race1Wins);
            info.AddValue(Field.Race2.ToString(), (byte)this.race2);
            info.AddValue(Field.Race2Wins.ToString(), (int)this.Race2Wins);
        }
        internal RaceMatchupResults(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                Field field;

                if (Enum.TryParse<Field>(entry.Name, out field))
                {
                    switch (field)
                    {
                        case Field.Race1: this.Race1 = (Race)info.GetByte(field.ToString()); break;
                        case Field.Race1ExpWinRatio: this.race1ExpWinRatio = (double)info.GetDouble(field.ToString()); break;
                        case Field.Race1Wins: this.Race1Wins = (int)info.GetInt32(field.ToString()); break;
                        case Field.Race2: this.race2 = (Race)info.GetByte(field.ToString()); break;
                        case Field.Race2Wins: this.Race2Wins = (int)info.GetInt32(field.ToString()); break;
                    }
                }

            }
        }
        #endregion

        internal bool IsMatchupFor(Race race1, Race race2)
        {
            return (race1 == this.Race1 && race2 == this.race2) || (race1 == this.race2 && race2 == this.Race1);
        }

        internal void AddMatchupData(Race raceWinner, int race1Rating, int race2Rating)
        {
            if (this.Race1 == raceWinner) { this.Race1Wins++; }
            else if (this.race2 == raceWinner) { this.Race2Wins++; }
            else { return; }

            this.race1ExpWinRatio = EloData.ExpectedWinRatio(race1Rating, race2Rating);
        }

        internal double WinRatioRace1()
        {
            return this.Race1Wins / (double)(this.Race1Wins + this.Race2Wins);
        }

        internal double WinRatioRace2()
        {
            return this.Race2Wins / (double)(this.Race1Wins + this.Race2Wins);
        }

        internal double WinRatioRace1CorrectedForExpectedWR()
        {
            return RaceMatchupResults.CorrectedWinRatio(this.race1ExpWinRatio / (double)this.TotalGames, this.WinRatioRace1());
        }

        internal double WinRatioRace2CorrectedForExpectedWR()
        {
            return RaceMatchupResults.CorrectedWinRatio((EloData.EXP_WIN_RATIO_MAX - this.race1ExpWinRatio) / (double)this.TotalGames, this.WinRatioRace1());
        }
    }
}
