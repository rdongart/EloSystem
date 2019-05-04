using System.Linq;
using System.Runtime.Serialization;

namespace EloSystem
{
    public partial class EloData : ISerializable
    {
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {            
            if (this.idHandler == null)
            {
                this.idHandler = new IDHandler();

                foreach (Country country in this.GetCountries()) { country.ID = this.idHandler.GetCountryIDNext(); }

                foreach (Tournament tournament in this.GetTournaments()) { tournament.ID = this.idHandler.GetTournamentIDNext(); }

                foreach (Season season in this.GetTournaments().SelectMany(t => t.GetSeasons())) { season.ID = this.idHandler.GetSeasonIDNext(); }

                foreach (SCPlayer player in this.GetPlayers()) { player.ID = this.idHandler.GetPlayerIDNext(); }

                foreach (Map map in this.GetMaps()) { map.ID = this.idHandler.GetMapIDNext(); }

                foreach (Team team in this.GetTeams()) { team.ID = this.idHandler.GetTeamIDNext(); }

                foreach (Tileset tileset in this.GetTileSets()) { tileset.ID = this.idHandler.GetTileSetIDNext(); }
            }

        }
    }
}
