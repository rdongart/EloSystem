using System;
using System.Threading.Tasks;

namespace SCEloSystemGUI
{
    internal static class TimedChangeHandler
    {
        internal delegate void TimedChangesCallback();

        internal delegate bool TimerChangeKiller();

        internal async static void HandleChanges(TimedChangesCallback callBack, TimerChangeKiller killer, int updateLatency = 8)
        {
            while (!killer())
            {
                await TimedChangeHandler.PutTaskDelay(updateLatency);

                callBack();
            }
        }

        private static async Task PutTaskDelay(int duration)
        {
            await Task.Delay(duration);
        }
    }
}
