using LiveBetApp.Common;
using LiveBetApp.Models.DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveBetApp.Services
{
    public class AlertService20
    {
        public AlertService20()
        {
        }

        private void ExecuteCore(int milisecond)
        {
            if (DataStore.SemiAutoBetOnly) return;
            while (true)
            {
                try
                {
                    List<Match> matchs = DataStore.Matchs.Values/*.Where(item => item.MatchId == 45784617)*/
                    .ToList();

                    List<Match> minimizedMatchs = new List<Match>();
                    for (int i = 0; i < matchs.Count; i++ )
                    {
                        //if (CheckMinimize(matchs[i], matchs))
                        {
                            minimizedMatchs.Add(matchs[i]);
                        }
                    }

                    try { UpdateAlert_Wd32(minimizedMatchs); }
                    catch (Exception ex)
                    { }

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Thread.Sleep(milisecond);
                }

            }
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

        private void UpdateAlert_Wd32(List<Match> matchs)
        {
            List<Match> localMatchs = new List<Match>(matchs);
            localMatchs = localMatchs
                .ToList();

            for (int i = 0; i < localMatchs.Count; i++)
            {
                if (CheckMatch_Wd32(localMatchs[i]))
                {
                    if (!DataStore.Alert_Wd32.ContainsKey(localMatchs[i].MatchId))
                    {
                        DataStore.Alert_Wd32.Add(localMatchs[i].MatchId, new Alert(false, true));
                    }
                    DataStore.Alert_Wd32[localMatchs[i].MatchId].CreateTime = DateTime.Now;
                }
            }


        }

        private bool CheckMatch_Wd32(Match match)
        {

            Dictionary<int, List<int>> data = Common.Functions.BuildOuScoreRemainingTime(match.MatchId);
            List<int> keys = data.Keys.OrderByDescending(item => item).ToList();
            if (keys.Count == 0) return false;
            int colCount = data.Values.ToList()[0].Count;

            for (int i = 0; i < colCount; i++)
            {
                bool hasTop = false;
                int idTop = 0;
                bool hasBottom = false;
                int idBottom = 0;
                for (int j = 0; j < keys.Count; j++)
                {
                    if (j < keys.Count - 1
                        && data[keys[j]][i] == 1
                        && data[keys[j + 1]][i] == 0)
                    {
                        hasTop = true;
                        idTop = j;
                    }

                    if (j > 0
                        && data[keys[j]][i] > 1
                        && data[keys[j - 1]][i] == 0)
                    {
                        hasBottom = true;
                        idBottom = j;
                    }
                }

                if (hasTop && hasBottom && idBottom > idTop)
                {
                    return true;
                }
            }

            return false;

        }

    }
}
