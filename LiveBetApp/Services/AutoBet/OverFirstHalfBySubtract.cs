using LiveBetApp.Common;
using LiveBetApp.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveBetApp.Services.AutoBet
{
    public class OverFirstHalfBySubtract
    {
        private List<long> _bettedMaths;

        public OverFirstHalfBySubtract()
        {
            _bettedMaths = new List<long>();
        }

        public void Execute(int milisecond)
        {
            Thread coreServiceThread;
            coreServiceThread = new Thread(() =>
            {
                ExecuteCore(milisecond);
            });
            coreServiceThread.IsBackground = true;
            coreServiceThread.Start();
        }

        private void ExecuteCore(int milisecond)
        {
            while (true)
            {
                try
                {
                    if (DataStore.RunAutobet)
                    {
                        UpdateShouldBetMatchIds();
                    }
                }
                catch (Exception ex)
                {
                    Common.Functions.WriteExceptionLog(ex);
                }
            }
        }

        private void UpdateShouldBetMatchIds()
        {
            List<Match> matchs = DataStore.Matchs.Values.Where(item => 
                item.LivePeriod == 1
                && !_bettedMaths.Contains(item.MatchId)
                && item.TimeSpanFromStart.TotalMinutes >= 5
            ).ToList();

            for (int i = 0; i < matchs.Count; i++)
            {

            }
        }

        private bool ShouldBet(long matchId)
        {
            Dictionary<int, List<OverUnderScoreTimesV2Item>> data = null;
            if (!DataStore.OverUnderScoreTimesFirstHalfV3.ContainsKey(matchId)) return false;
            data = DataStore.OverUnderScoreTimesFirstHalfV3[matchId];
            List<int> hdps = data.Keys.ToList();
            List<List<int>> subtractValue = new List<List<int>>();

            for (int i = 0; i < hdps.Count - 1; i++)
            {
                List<int> topRow    = data[hdps[i] + 0].Select(item => item.Over).ToList();
                List<int> bottomRow = data[hdps[i] + 1].Select(item => item.Over).ToList();
                List<int> subtractRow = new List<int>();
                for (int j = 0; j < topRow.Count; j++)
                {
                    subtractRow.Add(
                        Common.Functions.OverStepPrice(topRow[j])
                        - Common.Functions.OverStepPrice(bottomRow[j])
                    );
                }
                subtractValue.Add(subtractRow);
            }

            return false;
        }
    }
}
