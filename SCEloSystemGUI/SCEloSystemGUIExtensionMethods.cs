using EloSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SCEloSystemGUI
{
    internal static class SCEloSystemGUIExtensionMethods
    {
        internal static IEnumerable<PlayerStatsCloneDev> FilterByInterval(this IEnumerable<PlayerStatsCloneDev> data, DevelopmentInterval interval)
        {
            DateTime now = DateTime.Now;

            DateTime intervalTime;

            switch (interval)
            {
                case DevelopmentInterval.All_Time: return data;
                case DevelopmentInterval._3_Years:
                    intervalTime = now.AddYears(-3);
                    return data.Where(item => item.Date.CompareTo(intervalTime) >= 0);
                case DevelopmentInterval._12_Months:
                    intervalTime = now.AddYears(-1);
                    return data.Where(item => item.Date.CompareTo(intervalTime) >= 0);
                case DevelopmentInterval._6_Months:
                    intervalTime = now.AddMonths(-6);
                    return data.Where(item => item.Date.CompareTo(intervalTime) >= 0);
                case DevelopmentInterval._3_Months:
                    intervalTime = now.AddMonths(-3);
                    return data.Where(item => item.Date.CompareTo(intervalTime) >= 0);
                default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(DevelopmentInterval).Name, interval.ToString()));

            }


        }

        internal static IEnumerable<MatchEditorItem> ToMatchEditorItems(this IEnumerable<Game> source)
        {
            return source.GroupBy(game => game.Match).Select(grp => new MatchEditorItem(grp, grp.Key));
        }
    }
}
