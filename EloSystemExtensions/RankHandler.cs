using CustomExtensionMethods;
using EloSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;


namespace EloSystemExtensions
{
    public enum Rank { U, F, E, D, C, B, A, APlus }


    public class RankHandler
    {
        private const int GAMESPLAYED_DEFAULT_THRESHOLD = 8;
        private const int GAMESPLAYED_DEFAULT_RACE_THRESHOLD = 3;
        private const int RECENTACTIVITY_GAMESPLAYED_DEFAULT_THRESHOLD = 2;
        private const int RECENTACTIVITY_GAMESPLAYED_DEFAULT_RACE_THRESHOLD = 1;
        private const int RECENTACTIVITY_MONTHS_DEFAULT = 12;

        private const double APLUS_RANKS_PERCENTAGE = 0.01;
        private const double A_RANKS_PERCENTAGE = 0.05;
        private const double B_RANKS_PERCENTAGE = 0.1;
        private const double C_RANKS_PERCENTAGE = 0.21;
        private const double D_RANKS_PERCENTAGE = 0.21;
        private const double E_RANKS_PERCENTAGE = 0.21;

        private static Dictionary<int, Dictionary<Rank, Image>> rankImages = new Dictionary<int, Dictionary<Rank, Image>>();

        public int GamesPlayedThreshold { get; set; }
        public int GamesPlayedVsRaceThreshold { get; set; }
        public int RecentActivityGamesPlayedThreshold { get; set; }
        public int RecentActivityGamesPlayedVsRaceThreshold { get; set; }
        public int RecentActivityMonths { get; set; }
        private EloData eloDataBase;
        private Dictionary<SCPlayer, RankHolder> ranksByPlayer;

        public RankHandler(EloData resource)
        {
            this.GamesPlayedThreshold = RankHandler.GAMESPLAYED_DEFAULT_THRESHOLD;
            this.GamesPlayedVsRaceThreshold = RankHandler.GAMESPLAYED_DEFAULT_RACE_THRESHOLD;
            this.RecentActivityGamesPlayedThreshold = RankHandler.RECENTACTIVITY_GAMESPLAYED_DEFAULT_THRESHOLD;
            this.RecentActivityGamesPlayedVsRaceThreshold = RankHandler.RECENTACTIVITY_GAMESPLAYED_DEFAULT_RACE_THRESHOLD;
            this.RecentActivityMonths = RankHandler.RECENTACTIVITY_MONTHS_DEFAULT;

            this.eloDataBase = resource;

            this.eloDataBase.MatchPoolChanged += this.OnResourcesChanged;
            this.eloDataBase.PlayerPoolChanged += this.OnResourcesChanged;

            this.UpdateRanks();
        }

        private static Dictionary<Rank, int> CreateRankingCountThresholds(int playerCount)
        {
            var thresholds = new Dictionary<Rank, int>();

            thresholds.Add(Rank.APlus, (playerCount * RankHandler.APLUS_RANKS_PERCENTAGE).RoundToInt());
            thresholds.Add(Rank.A, (playerCount * (RankHandler.APLUS_RANKS_PERCENTAGE + RankHandler.A_RANKS_PERCENTAGE)).RoundToInt());
            thresholds.Add(Rank.B, (playerCount * (RankHandler.APLUS_RANKS_PERCENTAGE + RankHandler.A_RANKS_PERCENTAGE + RankHandler.B_RANKS_PERCENTAGE)).RoundToInt());
            thresholds.Add(Rank.C, (playerCount * (RankHandler.APLUS_RANKS_PERCENTAGE + RankHandler.A_RANKS_PERCENTAGE + RankHandler.B_RANKS_PERCENTAGE + RankHandler.C_RANKS_PERCENTAGE)).RoundToInt());
            thresholds.Add(Rank.D, (playerCount * (RankHandler.APLUS_RANKS_PERCENTAGE + RankHandler.A_RANKS_PERCENTAGE + RankHandler.B_RANKS_PERCENTAGE + RankHandler.C_RANKS_PERCENTAGE
                + RankHandler.D_RANKS_PERCENTAGE)).RoundToInt());
            thresholds.Add(Rank.E, (playerCount * (RankHandler.APLUS_RANKS_PERCENTAGE + RankHandler.A_RANKS_PERCENTAGE + RankHandler.B_RANKS_PERCENTAGE + RankHandler.C_RANKS_PERCENTAGE
                + RankHandler.D_RANKS_PERCENTAGE + RankHandler.E_RANKS_PERCENTAGE)).RoundToInt());

            return thresholds;
        }

        public static Image CreateRankImage(Rank rank, int pixelSize)
        {
            const double FRAME_PROPORTION = 0.13;
            const float TEXT_PROPORTION = 0.36F;

            int frameWidth = (pixelSize * FRAME_PROPORTION).RoundToInt();

            float textSize = pixelSize * TEXT_PROPORTION;

            var img = new Bitmap(pixelSize, pixelSize, PixelFormat.Format64bppPArgb);

            using (Graphics g = Graphics.FromImage(img))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Brush backColor = Brushes.Black;
                Brush txtColor = Brushes.DarkGoldenrod;
                string rankTxt = "";

                switch (rank)
                {
                    case Rank.U:
                        backColor = new SolidBrush(Color.FromArgb(127, 127, 127));
                        txtColor = Brushes.Black;
                        rankTxt = "U";
                        break;
                    case Rank.F:
                        backColor = Brushes.WhiteSmoke;
                        txtColor = Brushes.Black;
                        rankTxt = "F";
                        break;
                    case Rank.E:
                        backColor = Brushes.Magenta;
                        txtColor = Brushes.Black;
                        rankTxt = "E";
                        break;
                    case Rank.D:
                        backColor = new SolidBrush(Color.FromArgb(160, 0, 0));
                        txtColor = Brushes.White;
                        rankTxt = "D";
                        break;
                    case Rank.C:
                        backColor = new SolidBrush(Color.FromArgb(230, 230, 0));
                        txtColor = Brushes.Black;
                        rankTxt = "C";
                        break;
                    case Rank.B:
                        backColor = Brushes.Blue;
                        txtColor = Brushes.White;
                        rankTxt = "B";
                        break;
                    case Rank.A:
                        backColor = Brushes.ForestGreen;
                        txtColor = Brushes.White;
                        rankTxt = "A";
                        break;
                    case Rank.APlus:
                        backColor = Brushes.Black;
                        txtColor = new SolidBrush(Color.FromArgb(232, 170, 14));
                        rankTxt = "A+";
                        break;
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Rank).Name, rank.ToString()));
                }

                g.FillRegion(backColor, new Region(new Rectangle(0, 0, pixelSize, pixelSize)));

                var txtFont = new Font("Algerian", textSize, FontStyle.Regular, GraphicsUnit.Point, 0, false);

                SizeF txtSize = g.MeasureString(rankTxt, txtFont);

                g.DrawString(rankTxt, txtFont, txtColor, new PointF((pixelSize - txtSize.Width) / 2, (pixelSize - txtSize.Height) / 2));

                g.DrawRectangle(new Pen(txtColor, frameWidth), 0, 0, pixelSize - 1, pixelSize - 1);
            }

            return img;
        }

        private static Dictionary<Rank, Image> CreateRankImages(int pixelSize)
        {
            var imagesByRank = new Dictionary<Rank, Image>();

            foreach (Rank rank in Enum.GetValues(typeof(Rank)).Cast<Rank>()) { imagesByRank.Add(rank, RankHandler.CreateRankImage(rank, pixelSize)); }

            return imagesByRank;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="bufferImages">Set to true to buffer images in memory. This takes up more memory, but decreases processing load.</param>
        /// <returns></returns>
        public Image GetRankImageMain(SCPlayer player, int pixelSize, bool bufferImages)
        {
            if (RankHandler.rankImages.ContainsKey(pixelSize)) { return RankHandler.rankImages[pixelSize][this.ranksByPlayer[player].Main]; }
            else if (bufferImages)
            {
                RankHandler.rankImages.Add(pixelSize, RankHandler.CreateRankImages(pixelSize));

                return RankHandler.rankImages[pixelSize][this.ranksByPlayer[player].Main];
            }
            else { return RankHandler.CreateRankImage(this.ranksByPlayer[player].Main, pixelSize); }
        }

        public Image GetRankImageVsRace(SCPlayer player, int pixelSize, bool bufferImages, Race race)
        {
            Func<SCPlayer, int, Race, Image> GetImage = (ply, pxlSz, rc) =>
            {
                switch (rc)
                {
                    case Race.Zerg: return RankHandler.rankImages[pxlSz][this.ranksByPlayer[ply].vsZerg];
                    case Race.Terran: return RankHandler.rankImages[pxlSz][this.ranksByPlayer[ply].vsTerran];
                    case Race.Protoss: return RankHandler.rankImages[pxlSz][this.ranksByPlayer[ply].vsProtoss];
                    case Race.Random: return new Bitmap(0, 0);
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, rc.ToString()));
                }
            };

            if (RankHandler.rankImages.ContainsKey(pixelSize)) { return GetImage(player, pixelSize, race); }
            else if (bufferImages)
            {
                RankHandler.rankImages.Add(pixelSize, RankHandler.CreateRankImages(pixelSize));

                return GetImage(player, pixelSize, race);
            }
            else
            {
                switch (race)
                {
                    case Race.Zerg: return RankHandler.CreateRankImage(this.ranksByPlayer[player].vsZerg, pixelSize);
                    case Race.Terran: return RankHandler.CreateRankImage(this.ranksByPlayer[player].vsTerran, pixelSize);
                    case Race.Protoss: return RankHandler.CreateRankImage(this.ranksByPlayer[player].vsProtoss, pixelSize);
                    case Race.Random: return new Bitmap(0, 0);
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, race.ToString()));
                }
            }
        }

        private void OnResourcesChanged(object sender, EventArgs e)
        {
            this.UpdateRanks();
        }

        private void UpdateRanks()
        {
            var now = DateTime.Now;

            Func<SCPlayer, bool> hasRankMain = player => player.Stats.GamesTotal() >= this.GamesPlayedThreshold && this.eloDataBase.GamesByPlayer(player).Where(game =>
                 now.CompareTo(game.Match.DateTime.AddMonths(this.RecentActivityMonths)) <= 0).Count() >= this.RecentActivityGamesPlayedThreshold;

            Func<SCPlayer, Race, bool> hasRankForRace = (player, race) =>
            {
                return player.Stats.GamesVs(race) >= this.GamesPlayedVsRaceThreshold
                    && this.eloDataBase.GamesByPlayer(player).Where(game => ((game.Player1.Equals(player) && game.Player2Race == race) || (game.Player2.Equals(player) && game.Player1Race == race))
                    && now.CompareTo(game.Match.DateTime.AddMonths(this.RecentActivityMonths)) <= 0).Count()
                    >= this.RecentActivityGamesPlayedVsRaceThreshold;
            };


            List<SCPlayer> playersWithMainRank = this.eloDataBase.GetPlayers().Where(p => hasRankMain(p)).OrderByDescending(p => p.RatingTotal()).ThenByDescending(p =>
                this.eloDataBase.GamesByPlayer(p).OrderNewestFirst().First().Match.DateTime.Date).ThenByDescending(p => p.Stats.GamesTotal()).ToList();

            Func<Race, IOrderedEnumerable<SCPlayer>> GetPlayersWithRankVsRace = rc =>
            {
                return this.eloDataBase.GetPlayers().Where(p => hasRankForRace(p, rc)).OrderByDescending(p => p.RatingVs.GetValueFor(rc)).ThenByDescending(p =>
                    this.eloDataBase.GamesByPlayer(p).Where(g => (g.Player1.Equals(p) && g.Player2Race == rc) || (g.Player2.Equals(p) && g.Player1Race == rc)).OrderByDescending(game =>
                         game.Match.DateTime.Date).First().Match.DateTime.Date).ThenByDescending(p => p.Stats.GamesVs(rc));
            };

            List<SCPlayer> playersWithProtossRank = GetPlayersWithRankVsRace(Race.Protoss).ToList();
            List<SCPlayer> playersWithTerranRank = GetPlayersWithRankVsRace(Race.Terran).ToList();
            List<SCPlayer> playersWithZergRank = GetPlayersWithRankVsRace(Race.Zerg).ToList();

            Dictionary<Rank, int> rankThresholdsMain = RankHandler.CreateRankingCountThresholds(playersWithMainRank.Count);
            Dictionary<Rank, int> rankThresholdsProtoss = RankHandler.CreateRankingCountThresholds(playersWithProtossRank.Count);
            Dictionary<Rank, int> rankThresholdsTerran = RankHandler.CreateRankingCountThresholds(playersWithTerranRank.Count);
            Dictionary<Rank, int> rankThresholdsZerg = RankHandler.CreateRankingCountThresholds(playersWithZergRank.Count);

            Func<Dictionary<Rank, int>, int, Rank> GetRank = (rankingThresholds, rankingCount) =>
               {
                   if (rankingCount <= rankingThresholds[Rank.APlus]) { return Rank.APlus; }
                   else if (rankingCount <= rankingThresholds[Rank.A]) { return Rank.A; }
                   else if (rankingCount <= rankingThresholds[Rank.B]) { return Rank.B; }
                   else if (rankingCount <= rankingThresholds[Rank.C]) { return Rank.C; }
                   else if (rankingCount <= rankingThresholds[Rank.D]) { return Rank.D; }
                   else if (rankingCount <= rankingThresholds[Rank.E]) { return Rank.E; }
                   else { return Rank.F; }
               };

            this.ranksByPlayer = new Dictionary<SCPlayer, RankHolder>();

            foreach (SCPlayer thisPlayer in this.eloDataBase.GetPlayers())
            {
                var playersRank = new RankHolder();

                if (hasRankMain(thisPlayer)) { playersRank.Main = GetRank(rankThresholdsMain, playersWithMainRank.TakeWhile(p => !p.Equals(thisPlayer)).Count() + 1); }
                else { playersRank.Main = Rank.U; }

                if (hasRankForRace(thisPlayer, Race.Protoss)) { playersRank.vsProtoss = GetRank(rankThresholdsProtoss, playersWithProtossRank.TakeWhile(p => !p.Equals(thisPlayer)).Count() + 1); }
                else { playersRank.vsProtoss = Rank.U; }

                if (hasRankForRace(thisPlayer, Race.Zerg)) { playersRank.vsZerg = GetRank(rankThresholdsZerg, playersWithZergRank.TakeWhile(p => !p.Equals(thisPlayer)).Count() + 1); }
                else { playersRank.vsZerg = Rank.U; }

                if (hasRankForRace(thisPlayer, Race.Terran)) { playersRank.vsTerran = GetRank(rankThresholdsTerran, playersWithTerranRank.TakeWhile(p => !p.Equals(thisPlayer)).Count() + 1); }
                else { playersRank.vsTerran = Rank.U; }

                this.ranksByPlayer.Add(thisPlayer, playersRank);
            }

        }

    }
}
