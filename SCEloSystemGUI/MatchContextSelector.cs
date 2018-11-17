using EloSystem;
using SCEloSystemGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public class MatchContextSelector
    {
        public Season SelectedSeason
        {
            get
            {
                var selItem = this.SeasonSelector.SelectedItem as Tuple<string, Season>;

                return selItem != null ? selItem.Item2 : null;
            }
        }
        public Tournament SelectedTournament
        {
            get
            {
                return this.TournamentSelector.SelectedValue as Tournament;
            }
        }
        public ImageComboBoxImprovedItemHandling TournamentSelector { get; private set; }
        public ComboBox SeasonSelector { get; private set; }

        public MatchContextSelector(ImageComboBoxImprovedItemHandling tournamentSelector) : this(tournamentSelector, new ComboBox()) { }
        public MatchContextSelector(ComboBox seasonSelector) : this(new ImageComboBoxImprovedItemHandling(), seasonSelector) { }
        public MatchContextSelector(ImageComboBoxImprovedItemHandling tournamentSelector, ComboBox seasonSelector)
        {
            this.TournamentSelector = tournamentSelector;
            this.SeasonSelector = seasonSelector;

            this.SeasonSelector.Enabled = false;

            this.TournamentSelector.SelectedIndexChanged += this.TournamentSelector_SelectedIndexChanged;
        }

        private void TournamentSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AddSeasonItems();

            this.SeasonSelector.Enabled = this.SelectedTournament != null;
        }

        private void AddSeasonItems()
        {
            if (this.SelectedTournament == null) { return; }

            List<Season> seasonList = this.SelectedTournament.GetSeasons().OrderBy(season => season.Name).ToList();

            var selectedItem = this.SeasonSelector.SelectedItem != null ? (this.SeasonSelector.SelectedItem as Tuple<string, Season>).Item2 : null;

            this.SeasonSelector.DisplayMember = "Item1";
            this.SeasonSelector.ValueMember = "Item2";

            this.SeasonSelector.Items.Clear();
            this.SeasonSelector.Items.AddRange(seasonList.Select(season => Tuple.Create<string, Season>(season.Name, season)).ToArray());

            if (selectedItem != null && seasonList.Contains(selectedItem)) { this.SeasonSelector.SelectedIndex = seasonList.IndexOf(selectedItem); }
            else { this.SeasonSelector.SelectedIndex = -1; }
        }

        public void AddItems(IEnumerable<Tournament> items, ImageGetter getter)
        {
            this.TournamentSelector.AddItems(items.ToArray(), getter, true);
        }

        public void UpdateSeason()
        {
            this.AddSeasonItems();
        }
    }
}
