using System.Threading.Tasks;
using CustomExtensionMethods;
using EloSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EloSystemExtensions
{
    public class MirrorMatchupEvaluater
    {
        private const int GAMES_PLAYED_BY_PLAYER_IN_MATCHUP_DEFAULT_THRESHOLD = 7;
        private const int GAMES_PLAYED_ON_MAP_IN_MATCHUP_DEFAULT_THRESHOLD = 20;

        private bool evaluationNeedsToUpdate = true;
        private bool playerInitialRatingRegistrationHasRun = false;
        private Dictionary<Map, Dictionary<MirrorMatchup, double>> cointossEffectsByMaps = new Dictionary<Map, Dictionary<MirrorMatchup, double>>();
        private Dictionary<SCPlayer, int> initialRatingByPlayer = new Dictionary<SCPlayer, int>();
        private EloData eloDataBase;
        public int GamesPlayedByPlayerThreshold { get; set; }
        public int GamesPlayedOnMapInMatchupThreshold { get; set; }

        public MirrorMatchupEvaluater(EloData resource)
        {
            this.eloDataBase = resource;

            this.GamesPlayedByPlayerThreshold = MirrorMatchupEvaluater.GAMES_PLAYED_BY_PLAYER_IN_MATCHUP_DEFAULT_THRESHOLD;
            this.GamesPlayedOnMapInMatchupThreshold = MirrorMatchupEvaluater.GAMES_PLAYED_ON_MAP_IN_MATCHUP_DEFAULT_THRESHOLD;

            this.eloDataBase.MatchPoolChanged += this.OnMatchPoolChanged;
        }

        private void OnMatchPoolChanged(object sender, EventArgs e)
        {
            this.ScheduleMirrorMatchupEvaluation();
        }

        /// <summary>
        /// This method ensures that evaluations will be updated if evaluaton results are enquired.
        /// </summary>
        public void ScheduleMirrorMatchupEvaluation()
        {
            this.evaluationNeedsToUpdate = true;
        }

        /// <summary>
        /// This method forces an update of evaluations to be done immediately.
        /// </summary>
        public void InitiateMirrorMatchupEvaluations()
        {
            this.EvaluateMirrorMatchups();
        }

        private void RegisterPlayersInitialRatings()
        {
            SCPlayer[] playersToRegister = this.eloDataBase.GetPlayers().ToArray();

            var ratingByPlayerArray = new KeyValuePair<SCPlayer, int>[playersToRegister.Length];

            Parallel.For(0, playersToRegister.Length, index =>
            {
                SCPlayer player = playersToRegister[index];

                ratingByPlayerArray[index] = new KeyValuePair<SCPlayer, int>(player, this.eloDataBase.BacktraceInitialRating(player));
            });

            this.initialRatingByPlayer = ratingByPlayerArray.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            this.playerInitialRatingRegistrationHasRun = true;
        }

        private int GetInitialRating(SCPlayer player)
        {
            int initialRating;

            if (!this.initialRatingByPlayer.TryGetValue(player, out initialRating))
            {
                initialRating = this.eloDataBase.BacktraceInitialRating(player);

                this.initialRatingByPlayer.Add(player, initialRating);
            }

            return initialRating;
        }

        private void EvaluateMirrorMatchups()
        {
            this.evaluationNeedsToUpdate = false;

            if (!this.playerInitialRatingRegistrationHasRun) { this.RegisterPlayersInitialRatings(); }

            const double EXPECTEDWINRATIO_FOR_EVEN_MATCHUP = 0.5;
            const double EXPECTEDWINRATIO_FOR_UNDERDOG_LOWER_THRESHOLD = 0.01;

            var playerMirrorMatchupData = new Dictionary<SCPlayer, MirrorMathcupPlayerData>();
            var gamesByMaps = new Dictionary<Map, List<ExtendedGameData>>();

            Func<MirrorMathcupPlayerData, MirrorMatchup, bool> PlayerHasEnoughGamesInMatchup = (mmPlayerData, mmT) =>
            {
                return mmPlayerData.GamesIn(mmT) >= this.GamesPlayedByPlayerThreshold;
            };


            // this method register player data and the games that satisfy criteria for mirror matchup evaluation
            Action<Game, MirrorMatchup> RegisterGameData = (gm, mm) =>
            {
                // first get players data if they have been registered, otherwise register them
                MirrorMathcupPlayerData winnersMirrorMatchupData;

                if (!playerMirrorMatchupData.TryGetValue(gm.Winner, out winnersMirrorMatchupData))
                {
                    winnersMirrorMatchupData = new MirrorMathcupPlayerData(this.GetInitialRating(gm.Winner));

                    playerMirrorMatchupData.Add(gm.Winner, winnersMirrorMatchupData);
                }


                MirrorMathcupPlayerData losersMirrorMatchupData;

                if (!playerMirrorMatchupData.TryGetValue(gm.Loser, out losersMirrorMatchupData))
                {
                    losersMirrorMatchupData = new MirrorMathcupPlayerData(this.GetInitialRating(gm.Loser));

                    playerMirrorMatchupData.Add(gm.Loser, losersMirrorMatchupData);
                }


                double expectedWinRatioForWinner = EloData.ExpectedWinRatio(winnersMirrorMatchupData.RatingIn(mm), losersMirrorMatchupData.RatingIn(mm));

                int ratingChange = EloData.RatingChange(winnersMirrorMatchupData.GamesIn(mm), losersMirrorMatchupData.GamesIn(mm), expectedWinRatioForWinner);

                winnersMirrorMatchupData.RegisterResult(mm, ratingChange);
                losersMirrorMatchupData.RegisterResult(mm, -ratingChange);

                double expectedWinRatioForUnderdog = Math.Min(expectedWinRatioForWinner, 1 - expectedWinRatioForWinner).Round(2);

                if (expectedWinRatioForUnderdog != EXPECTEDWINRATIO_FOR_EVEN_MATCHUP && expectedWinRatioForUnderdog >= EXPECTEDWINRATIO_FOR_UNDERDOG_LOWER_THRESHOLD
                    && PlayerHasEnoughGamesInMatchup(winnersMirrorMatchupData, mm) && PlayerHasEnoughGamesInMatchup(losersMirrorMatchupData, mm))
                {
                    List<ExtendedGameData> gameList;

                    if (!gamesByMaps.TryGetValue(gm.Map, out gameList))
                    {
                        gameList = new List<ExtendedGameData>();

                        gamesByMaps.Add(gm.Map, gameList);
                    }

                    gameList.Add(new ExtendedGameData(gm, expectedWinRatioForWinner, mm));
                }

            };


            // go throug each game from first to last and register player mirror matchup stats and game data
            var mmType = new MirrorMatchup();

            foreach (Game game in this.eloDataBase.GetAllGames().Where(gm => gm.Map != null).OrderOldestFirst().Where(gm => MirrorMatchupEvaluater.GameIsMirrorMatchup(gm, out mmType)
                && mmType != MirrorMatchup.RvR))
            {
                RegisterGameData(game, mmType);
            }


            int gamesCountTotalPvP = 0;
            int gamesCountTotalTvT = 0;
            int gamesCountTotalZvZ = 0;

            Func<MirrorMatchup, int> GetMirrorMatchupTotalGamesCount = (matchupType) =>
            {
                switch (matchupType)
                {
                    case MirrorMatchup.ZvZ: return gamesCountTotalZvZ;
                    case MirrorMatchup.TvT: return gamesCountTotalTvT;
                    case MirrorMatchup.PvP: return gamesCountTotalPvP;
                    case MirrorMatchup.RvR: throw new Exception(String.Format("{0} {1} was unexpected in the current switch statement context.", typeof(MirrorMatchup).Name, matchupType.ToString()));
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(MirrorMatchup).Name, matchupType.ToString()));
                }
            };


            Func<IEnumerable<ExtendedGameData>, MirrorMatchup, double> CalculateCoinTossFactor = (gms, matchupType) =>
            {
                return Enumerable.Range(1, 49).Select(x => (x / 100.0).Round(2)).Select(underDogEWROnThisIteration =>
                {
                    IEnumerable<ExtendedGameData> mmTypeGames = gms.Where(gm => gm.Matchup == matchupType && gm.ExpectedWinRatioUnderdog == underDogEWROnThisIteration);

                    double mmTypeGamesCountThisEWRStep = mmTypeGames.Count();

                    if (GetMirrorMatchupTotalGamesCount(matchupType) == 0 || mmTypeGamesCountThisEWRStep == 0) { return 0; }
                    else
                    {
                        return    // first we calculate how the underdogs' performance has as a difference to their expected win rate on this iteration
                                  (((mmTypeGames.Where(gm => gm.UnderDogWasWinner).Count() / mmTypeGamesCountThisEWRStep) - underDogEWROnThisIteration)
                                  // and see that in comparison to the gap between the underdogs' expected win rato and the gap up to a 50/50 result                        
                                  / (EXPECTEDWINRATIO_FOR_EVEN_MATCHUP - underDogEWROnThisIteration))
                                  // and this factor is weighed by the number of games on this iteration
                                  * (mmTypeGamesCountThisEWRStep / GetMirrorMatchupTotalGamesCount(matchupType));
                    }

                }).Sum();
            };

            this.cointossEffectsByMaps = new Dictionary<Map, Dictionary<MirrorMatchup, double>>();

            // now evaluate the cointoss factor for each map
            foreach (KeyValuePair<Map, List<ExtendedGameData>> gmsByMap in gamesByMaps)
            {
                gamesCountTotalPvP = gmsByMap.Value.Where(gm => gm.Matchup == MirrorMatchup.PvP).Count();
                gamesCountTotalTvT = gmsByMap.Value.Where(gm => gm.Matchup == MirrorMatchup.TvT).Count();
                gamesCountTotalZvZ = gmsByMap.Value.Where(gm => gm.Matchup == MirrorMatchup.ZvZ).Count();

                this.cointossEffectsByMaps.Add(gmsByMap.Key, new Dictionary<MirrorMatchup, double>());

                foreach (KeyValuePair<MirrorMatchup, int> gamesCountByMatchup in new Dictionary<MirrorMatchup, int>() {
                    { MirrorMatchup.PvP, gamesCountTotalPvP }
                    , { MirrorMatchup.TvT, gamesCountTotalTvT }
                    , { MirrorMatchup.ZvZ, gamesCountTotalZvZ } })
                {
                    if (gamesCountByMatchup.Value >= this.GamesPlayedOnMapInMatchupThreshold)
                    {
                        this.cointossEffectsByMaps[gmsByMap.Key].Add(gamesCountByMatchup.Key, CalculateCoinTossFactor(gmsByMap.Value, gamesCountByMatchup.Key));
                    }
                }
            }

        }

        private static bool GameIsMirrorMatchup(Game game, out MirrorMatchup mmType)
        {
            mmType = MirrorMatchup.RvR;

            if (game.Player1Race == game.Player2Race)
            {
                switch (game.Player1Race)
                {
                    case Race.Zerg: mmType = MirrorMatchup.ZvZ; break;
                    case Race.Terran: mmType = MirrorMatchup.TvT; break;
                    case Race.Protoss: mmType = MirrorMatchup.PvP; break;
                    case Race.Random: mmType = MirrorMatchup.RvR; break;
                    default: throw new Exception(String.Format("Unknown {0} {1}.", typeof(Race).Name, game.Player1Race.ToString()));
                }

                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Returns a factor representing the degree to which underdogs to as well as favorites when playing on a given map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="cointossFactor"></param>
        /// <returns></returns>
        /// <remarks>A value above zero means that the underdogs perform better than expected, a value equal zero means they perform as expected, and below zero means they perform worse than expected. A value of one means that underdogs perform as well as favorites.</remarks>
        public bool TryGetCointossFactor(Map map, MirrorMatchup matchupType, out double cointossFactor)
        {
            if (this.evaluationNeedsToUpdate) { this.EvaluateMirrorMatchups(); }

            Dictionary<MirrorMatchup, double> cointossEffectByMap;

            cointossFactor = 0;

            return this.cointossEffectsByMaps.TryGetValue(map, out cointossEffectByMap) && cointossEffectByMap.TryGetValue(matchupType, out cointossFactor);
        }

    }
}
