using CustomExtensionMethods;
using EloSystem;
using EloSystemExtensions;
using SCEloSystemGUI.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace SCEloSystemGUI
{
    public static class RaceIconProvider
    {
        private static Dictionary<List<Race>, Image> raceListsCreated = new Dictionary<List<Race>, Image>();
        private static Dictionary<Matchup, Image> matchupListsCreated = new Dictionary<Matchup, Image>();

        internal static Image GetRaceUsageIcon(SCPlayer player)
        {
            if (player.Stats.GamesTotal() == 0) { return null; }

            if (player.PlaysMultipleRaces()) { return RaceIconProvider.GetMultipleRacesUsageIcon(player); }
            else { return RaceIconProvider.GetRaceBitmap(player.GetPrimaryRace()); }

        }

        internal static Image GetMultipleRacesUsageIcon(IEnumerable<Race> racesUsage)
        {
            if (racesUsage.IsEmpty()) { return null; }
            else if (racesUsage.Count() == 1) { return RaceIconProvider.GetRaceBitmap(racesUsage.ElementAt(0)); }
            else
            {
                if (!RaceIconProvider.RaceUsageIconExists(racesUsage)) { RaceIconProvider.CreateMultipleRaceUsageIcon(racesUsage); }

                return RaceIconProvider.raceListsCreated.First(lst => lst.Key.Count == racesUsage.Count() && lst.Key.SequenceEqual(racesUsage)).Value;
            }

        }

        internal static Image GetMatchupImage(Matchup matchup)
        {
            Image img;

            if (!RaceIconProvider.matchupListsCreated.TryGetValue(matchup, out img))
            {
                img = RaceIconProvider.CreateMatchupImage(matchup);

                RaceIconProvider.matchupListsCreated.Add(matchup, img);
            }

            return img;
        }

        private static Image CreateMatchupImage(Matchup matchup)
        {
            switch (matchup)
            {
                case Matchup.ZvP: return RaceIconProvider.CreateMatchupImage(Race.Zerg, Race.Protoss);
                case Matchup.PvT: return RaceIconProvider.CreateMatchupImage(Race.Protoss, Race.Terran);
                case Matchup.TvZ: return RaceIconProvider.CreateMatchupImage(Race.Terran, Race.Zerg);
                case Matchup.RvZ: return RaceIconProvider.CreateMatchupImage(Race.Random, Race.Zerg);
                case Matchup.RvP: return RaceIconProvider.CreateMatchupImage(Race.Random, Race.Protoss);
                case Matchup.RvT: return RaceIconProvider.CreateMatchupImage(Race.Random, Race.Terran);
                case Matchup.ZvZ: return RaceIconProvider.CreateMatchupImage(Race.Zerg, Race.Zerg);
                case Matchup.PvP: return RaceIconProvider.CreateMatchupImage(Race.Protoss, Race.Protoss);
                case Matchup.TvT: return RaceIconProvider.CreateMatchupImage(Race.Terran, Race.Terran);
                case Matchup.RvR: return RaceIconProvider.CreateMatchupImage(Race.Random, Race.Random);
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Matchup).Name, matchup.ToString()));
            }
        }

        private static Image CreateMatchupImage(Race race1, Race race2)
        {
            Bitmap raceFirstImg = RaceIconProvider.GetRaceBitmap(race1);
            Bitmap raceSecondImg = RaceIconProvider.GetRaceBitmap(race2);

            int summedWidth = raceFirstImg.Width + raceSecondImg.Width;
            int maxHeight = Math.Max(raceFirstImg.Height, raceSecondImg.Height);
            int extraWidth = (summedWidth * 0.5).RoundToInt();

            var img = new Bitmap(summedWidth + extraWidth, maxHeight, PixelFormat.Format64bppArgb);

            using (Graphics g = Graphics.FromImage(img))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                int firstImgHeightOffset = ((maxHeight - raceFirstImg.Height) / 2);

                g.DrawImage(raceFirstImg, new Rectangle(0, firstImgHeightOffset, raceFirstImg.Width, raceFirstImg.Height));

                int secondImgHeightOffset = ((maxHeight - raceSecondImg.Height) / 2);

                g.DrawImage(raceSecondImg, new Rectangle(img.Width - raceSecondImg.Width, secondImgHeightOffset, raceSecondImg.Width, raceSecondImg.Height));

                const string MATCHUP_TEXT = "vs";

                var font = new Font("Lucida Calligraphy", 9F, FontStyle.Bold & FontStyle.Italic);

                SizeF txtsize = g.MeasureString(MATCHUP_TEXT, font);
                
                g.DrawString(MATCHUP_TEXT, font, Brushes.Black, new PointF((img.Width / 2F) - (txtsize.Width / 2), (img.Height / 2F) - (txtsize.Height / 2)));
            }

            return img;
        }

        private static void CreateMultipleRaceUsageIcon(IEnumerable<Race> raceUsageList)
        {
            if (raceUsageList.Count() == 1) { RaceIconProvider.GetRaceBitmap(raceUsageList.ElementAt(0)); }
            else if (raceUsageList.Count() == 2) { RaceIconProvider.CreateDoubleRaceUsageIcon(raceUsageList.ElementAt(0), raceUsageList.ElementAt(1)); }
            else if (raceUsageList.Count() == 3) { RaceIconProvider.CreateTripleRaceUsageIcon(raceUsageList.ElementAt(0), raceUsageList.ElementAt(1), raceUsageList.ElementAt(2)); }
            else if (raceUsageList.Count() > 3) { RaceIconProvider.CreateQuadRaceUsageIcon(raceUsageList.ElementAt(0), raceUsageList.ElementAt(1), raceUsageList.ElementAt(2), raceUsageList.ElementAt(3)); }

        }

        private static Image GetMultipleRacesUsageIcon(SCPlayer player)
        {
            return RaceIconProvider.GetMultipleRacesUsageIcon(player.RaceUsageFrequency().Where(kvp => kvp.Value > 0).OrderByDescending(kvp => kvp.Value).ThenBy(kvp => (int)kvp.Key).Select(kvp =>
                kvp.Key).ToList());
        }

        private static void CreateDoubleRaceUsageIcon(Race raceFirst, Race raceSecond)
        {
            Bitmap raceFirstImg = RaceIconProvider.GetRaceBitmap(raceFirst);
            Bitmap raceSecondImg = RaceIconProvider.GetRaceBitmap(raceSecond);

            int summedWidth = raceFirstImg.Width + raceSecondImg.Width;
            int maxHeight = Math.Max(raceFirstImg.Height, raceSecondImg.Height);
            int extraWidth = (summedWidth * 0.25).RoundToInt();

            var img = new Bitmap(summedWidth + extraWidth, maxHeight, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(img))
            {
                const double SLASH_SLOPE = 0.13;

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                int firstImgHeightOffset = ((maxHeight - raceFirstImg.Height) / 2);

                g.DrawImage(raceFirstImg, new Rectangle(0, firstImgHeightOffset, raceFirstImg.Width, raceFirstImg.Height));

                g.DrawLine(new Pen(Brushes.Black, 2), raceFirstImg.Width + (extraWidth * SLASH_SLOPE).RoundToInt(), maxHeight, raceFirstImg.Width + (extraWidth * (1 - SLASH_SLOPE)).RoundToInt(), 0);

                int secondImgHeightOffset = ((maxHeight - raceSecondImg.Height) / 2);

                g.DrawImage(raceSecondImg, new Rectangle(img.Width - raceSecondImg.Width, secondImgHeightOffset, raceSecondImg.Width, raceSecondImg.Height));
            }

            RaceIconProvider.raceListsCreated.Add(new List<Race>() { raceFirst, raceSecond }, img);
        }

        private static void CreateTripleRaceUsageIcon(Race raceFirst, Race raceSecond, Race raceThird)
        {
            Bitmap raceFirstImg = RaceIconProvider.GetRaceBitmap(raceFirst);
            Bitmap raceSecondImg = RaceIconProvider.GetRaceBitmap(raceSecond);
            Bitmap raceThirdImg = RaceIconProvider.GetRaceBitmap(raceThird);

            int summedWidth = raceFirstImg.Width + raceSecondImg.Width + raceThirdImg.Width;
            int maxHeight = Math.Max(raceFirstImg.Height, Math.Max(raceSecondImg.Height, raceThirdImg.Height));
            int extraWidth = (summedWidth * 0.20).RoundToInt();

            var img = new Bitmap(summedWidth + extraWidth * 2, maxHeight, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(img))
            {
                const double SLASH_SLOPE = 0.11;

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                int firstImgHeightOffset = ((maxHeight - raceFirstImg.Height) / 2);

                g.DrawImage(raceFirstImg, new Rectangle(0, firstImgHeightOffset, raceFirstImg.Width, raceFirstImg.Height));

                var slashPen = new Pen(Brushes.Black, 2);

                g.DrawLine(slashPen, raceFirstImg.Width + (extraWidth * SLASH_SLOPE).RoundToInt(), maxHeight, raceFirstImg.Width + (extraWidth * (1 - SLASH_SLOPE)).RoundToInt(), 0);

                int secondImgHeightOffset = ((maxHeight - raceSecondImg.Height) / 2);

                g.DrawImage(raceSecondImg, new Rectangle(raceFirstImg.Width + extraWidth, secondImgHeightOffset, raceSecondImg.Width, raceSecondImg.Height));

                g.DrawLine(slashPen, raceFirstImg.Width + extraWidth + raceSecondImg.Width + (extraWidth * SLASH_SLOPE).RoundToInt(), maxHeight, raceFirstImg.Width + extraWidth + raceSecondImg.Width
                    + (extraWidth * (1 - SLASH_SLOPE)).RoundToInt(), 0);

                int thirdImgHeightOffset = ((maxHeight - raceThirdImg.Height) / 2);

                g.DrawImage(raceThirdImg, new Rectangle(summedWidth + extraWidth * 2 - raceThirdImg.Width, thirdImgHeightOffset, raceThirdImg.Width, raceThirdImg.Height));
            }

            RaceIconProvider.raceListsCreated.Add(new List<Race>() { raceFirst, raceSecond, raceThird }, img);
        }

        private static void CreateQuadRaceUsageIcon(Race raceFirst, Race raceSecond, Race raceThird, Race raceFourth)
        {
            Bitmap raceFirstImg = RaceIconProvider.GetRaceBitmap(raceFirst);
            Bitmap raceSecondImg = RaceIconProvider.GetRaceBitmap(raceSecond);
            Bitmap raceThirdImg = RaceIconProvider.GetRaceBitmap(raceThird);
            Bitmap raceFourthImg = RaceIconProvider.GetRaceBitmap(raceFourth);

            int maxWidth = (new int[4] { raceFirstImg.Width, raceSecondImg.Width, raceThirdImg.Width, raceFourthImg.Width }).Max();
            int maxHeight = (new int[4] { raceFirstImg.Height, raceSecondImg.Height, raceThirdImg.Height, raceFourthImg.Height }).Max();

            int summedWidth = maxWidth * 2;
            int summedHeight = maxHeight * 2;

            const double HORIZONTAL_IMAGE_SEPARATION_RATIO = 0.15;

            int extraWidth = (maxWidth * HORIZONTAL_IMAGE_SEPARATION_RATIO).RoundToInt();

            const double VERTICAL_IMAGE_SEPARATION_RATIO = 0.12;
            int extraHeight = (maxHeight * VERTICAL_IMAGE_SEPARATION_RATIO).RoundToInt();

            var img = new Bitmap(summedWidth + extraWidth, summedHeight + extraHeight, PixelFormat.Format64bppArgb);

            using (Graphics g = Graphics.FromImage(img))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                var firstImgOffset = new Point((maxWidth - raceFirstImg.Width) / 2, (maxHeight - raceFirstImg.Height) / 2);
                g.DrawImage(raceFirstImg, new Rectangle(firstImgOffset.X, firstImgOffset.Y, raceFirstImg.Width, raceFirstImg.Height));

                var secondImgOffset = new Point(maxWidth + extraWidth + (maxWidth - raceSecondImg.Width) / 2, (maxHeight - raceSecondImg.Height) / 2);
                g.DrawImage(raceSecondImg, new Rectangle(secondImgOffset.X, secondImgOffset.Y, raceSecondImg.Width, raceSecondImg.Height));

                var thirdImgOffset = new Point((maxWidth - raceThirdImg.Width) / 2, maxHeight + extraHeight + (maxHeight - raceThirdImg.Height) / 2);
                g.DrawImage(raceThirdImg, new Rectangle(thirdImgOffset.X, thirdImgOffset.Y, raceThirdImg.Width, raceThirdImg.Height));

                var fourthImgOffset = new Point(maxWidth + extraWidth + (maxWidth - raceFourthImg.Width) / 2, maxHeight + extraHeight + (maxHeight - raceFourthImg.Height) / 2);
                g.DrawImage(raceFourthImg, new Rectangle(fourthImgOffset.X, fourthImgOffset.Y, raceFourthImg.Width, raceFourthImg.Height));


                var slashPen = new Pen(Brushes.Black, 2);
                g.DrawLine(slashPen, img.Width / 2, 0, img.Width / 2, img.Height);
                g.DrawLine(slashPen, 0, img.Height / 2, img.Width, img.Height / 2);
            }

            RaceIconProvider.raceListsCreated.Add(new List<Race>() { raceFirst, raceSecond, raceThird, raceFourth }, img);
        }

        internal static Bitmap GetRaceBitmap(Race race)
        {
            switch (race)
            {
                case Race.Zerg: return Resources.Zicon;
                case Race.Terran: return Resources.Ticon;
                case Race.Protoss: return Resources.Picon;
                case Race.Random: return Resources.Ricon;
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, race.ToString()));
            }
        }

        private static bool RaceUsageIconExists(IEnumerable<Race> raceUsageList)
        {
            return RaceIconProvider.raceListsCreated.Keys.Any(lst => lst.Count() == raceUsageList.Count() && lst.SequenceEqual(raceUsageList));
        }
    }
}
