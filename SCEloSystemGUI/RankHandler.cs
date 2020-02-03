﻿using CustomExtensionMethods;
using EloSystem;
using EloSystemExtensions;
using SCEloSystemGUI.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace SCEloSystemGUI
{
    internal enum Rank { U, F, E, D, C, B, A, APlus }


    internal class RankHandler
    {
        private const double APLUS_RANKS_PERCENTAGE = 0.01;
        private const double A_RANKS_PERCENTAGE = 0.05;
        private const double B_RANKS_PERCENTAGE = 0.1;
        private const double C_RANKS_PERCENTAGE = 0.21;
        private const double D_RANKS_PERCENTAGE = 0.21;
        private const double E_RANKS_PERCENTAGE = 0.21;

        private static Dictionary<int, Dictionary<Rank, Image>> rankImages = new Dictionary<int, Dictionary<Rank, Image>>();

        private ResourceGetter eloDataBase;
        private Dictionary<SCPlayer, RankHolder> ranksByPlayer;

        internal RankHandler(ResourceGetter resource)
        {
            this.eloDataBase = resource;

            this.eloDataBase().MatchPoolChanged += this.OnResourcesChanged;
            this.eloDataBase().PlayerPoolChanged += this.OnResourcesChanged;

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

        internal static Image CreateRankImage(Rank rank, int pixelSize)
        {
            int frameWidth = (pixelSize * 0.12).RoundToInt();
            float textSize = pixelSize * 0.36F;

            var img = new Bitmap(pixelSize, pixelSize, PixelFormat.Format32bppArgb);

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
                        backColor = new SolidBrush(Color.FromArgb(120, 120, 120));
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
                        backColor = Brushes.DarkRed;
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
                        txtColor = Brushes.DarkGoldenrod;
                        rankTxt = "A+";
                        break;
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Rank).Name, rank.ToString()));
                }

                g.FillRegion(backColor, new Region(new RectangleF(0, 0, pixelSize, pixelSize)));

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
        internal Image GetRankImageMain(SCPlayer player, int pixelSize, bool bufferImages)
        {
            if (RankHandler.rankImages.ContainsKey(pixelSize)) { return RankHandler.rankImages[pixelSize][this.ranksByPlayer[player].Main]; }
            else if (bufferImages)
            {
                RankHandler.rankImages.Add(pixelSize, RankHandler.CreateRankImages(pixelSize));

                return RankHandler.rankImages[pixelSize][this.ranksByPlayer[player].Main];
            }
            else { return RankHandler.CreateRankImage(this.ranksByPlayer[player].Main, pixelSize); }
        }

        internal Image GetRankImageVsRace(SCPlayer player, int pixelSize, bool bufferImages, Race race)
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

            Func<SCPlayer, bool> hasRankMain = player => player.Stats.GamesTotal() >= EloSystemGUIStaticMembers.GAMESPLAYED_DEFAULT_THRESHOLD && this.eloDataBase().GamesByPlayer(player).Where(game =>
                now.CompareTo(game.Match.DateTime.AddMonths(EloSystemGUIStaticMembers.RECENTACTIVITY_MONTHS_DEFAULT)) <= 0).Count() >= EloSystemGUIStaticMembers.RECENTACTIVITY_GAMESPLAYED_DEFAULT_THRESHOLD;

            Func<SCPlayer, Race, bool> hasRankForRace = (player, race) =>
            {
                return player.Stats.GamesVs(race) >= EloSystemGUIStaticMembers.GAMESPLAYED_DEFAULT_RACE_THRESHOLD
                    && this.eloDataBase().GamesByPlayer(player).Where(game => ((game.Player1.Equals(player) && game.Player2Race == race) || (game.Player2.Equals(player) && game.Player1Race == race))
                    && now.CompareTo(game.Match.DateTime.AddMonths(EloSystemGUIStaticMembers.RECENTACTIVITY_MONTHS_DEFAULT)) <= 0).Count()
                    >= EloSystemGUIStaticMembers.RECENTACTIVITY_GAMESPLAYED_DEFAULT_RACE_THRESHOLD;
            };

            List<SCPlayer> playersWithMainRank = this.eloDataBase().GetPlayers().Where(p => hasRankMain(p)).OrderByDescending(p => p.RatingTotal()).ToList();
            List<SCPlayer> playersWithProtossRank = this.eloDataBase().GetPlayers().Where(p => hasRankForRace(p, Race.Protoss)).OrderByDescending(p => p.RatingVs.Protoss).ToList();
            List<SCPlayer> playersWithTerranRank = this.eloDataBase().GetPlayers().Where(p => hasRankForRace(p, Race.Terran)).OrderByDescending(p => p.RatingVs.Terran).ToList();
            List<SCPlayer> playersWithZergRank = this.eloDataBase().GetPlayers().Where(p => hasRankForRace(p, Race.Zerg)).OrderByDescending(p => p.RatingVs.Zerg).ToList();

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

            foreach (SCPlayer thisPlayer in this.eloDataBase().GetPlayers())
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
