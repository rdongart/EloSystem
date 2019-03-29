using System;
using System.Runtime.Serialization;

namespace EloSystem
{

    public partial class EloData
    {
        [Serializable]
        internal class IDHandler : ISerializable
        {
            private int countryIDNext = 1;
            private int mapIDNext = 1;
            private int playerIDNext = 1;
            private int seasonIDNext = 1;
            private int teamSetIDNext = 1;
            private int tileSetIDNext = 1;
            private int tournamentIDNext = 1;

            internal IDHandler()
            {
            }

            #region Implementing ISerializable
            private enum Field
            {
                countryIDNext,
                mapIDNext,
                playerIDNext,
                seasonIDNext,
                teamSetIDNext,
                tileSetIDNext,
                tournamentIDNext

            }
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue(Field.countryIDNext.ToString(), (int)this.countryIDNext);
                info.AddValue(Field.mapIDNext.ToString(), (int)this.mapIDNext);
                info.AddValue(Field.playerIDNext.ToString(), (int)this.playerIDNext);
                info.AddValue(Field.seasonIDNext.ToString(), (int)this.seasonIDNext);
                info.AddValue(Field.teamSetIDNext.ToString(), (int)this.teamSetIDNext);
                info.AddValue(Field.tileSetIDNext.ToString(), (int)this.tileSetIDNext);
                info.AddValue(Field.tournamentIDNext.ToString(), (int)this.tournamentIDNext);
            }
            internal IDHandler(SerializationInfo info, StreamingContext context)
            {
                foreach (SerializationEntry entry in info)
                {
                    Field field;

                    if (Enum.TryParse<Field>(entry.Name, out field))
                    {
                        switch (field)
                        {
                            case Field.countryIDNext: this.countryIDNext = (int)info.GetInt32(field.ToString()); break;
                            case Field.mapIDNext: this.mapIDNext = (int)info.GetInt32(field.ToString()); break;
                            case Field.playerIDNext: this.playerIDNext = (int)info.GetInt32(field.ToString()); break;
                            case Field.seasonIDNext: this.seasonIDNext = (int)info.GetInt32(field.ToString()); break;
                            case Field.teamSetIDNext: this.teamSetIDNext = (int)info.GetInt32(field.ToString()); break;
                            case Field.tileSetIDNext: this.tileSetIDNext = (int)info.GetInt32(field.ToString()); break;
                            case Field.tournamentIDNext: this.tournamentIDNext = (int)info.GetInt32(field.ToString()); break;
                        }
                    }

                }
            }
            #endregion

            internal int GetCountryIDNext()
            {
                return this.countryIDNext++;
            }

            internal int GetTournamentIDNext() { return this.tournamentIDNext++; }

            internal int GetSeasonIDNext() { return this.seasonIDNext++; }

            internal int GetPlayerIDNext() { return this.playerIDNext++; }

            internal int GetMapIDNext() { return this.mapIDNext++; }

            internal int GetTeamSetIDNext() { return this.teamSetIDNext++; }

            internal int GetTileSetIDNext() { return this.tileSetIDNext++; }

        }
    }
}
