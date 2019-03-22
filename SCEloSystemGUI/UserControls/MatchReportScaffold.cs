using System.Linq;
using System.Collections.Generic;
using System;
using EloSystem;
using System.Windows.Forms;


namespace SCEloSystemGUI.UserControls
{
    public partial class MatchReport : UserControl
    {
        internal class MatchReportScaffold
        {
            internal Tournament Tournament { get; set; }
            internal Season Season { get; set; }
            internal DateTime Date { get; set; }
            internal SCPlayer Player1 { get; set; }
            internal SCPlayer Player2 { get; set; }
            private List<GameReport> games;

            internal MatchReportScaffold(IEnumerable<GameReport> games)
            {
                this.games = games.ToList();
            }

            internal IEnumerable<GameReport> GetGameReports()
            {
                foreach (GameReport game in this.games) { yield return game; }
            }
        }
    }
}