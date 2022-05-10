using Normal.Realtime;


namespace ETS.Realtime
{
    public class Stopwatch : RealtimeComponent<StopwatchModel>
    {
        public bool startWatchOnStart;
        public float time
        {
            get
            {
                // Return 0 if we're not connected to the room yet.
                if (model == null) return 0.0f;

                // Make sure the stopwatch is running
                if (model.startTime == 0.0) return 0.0f;

                // Calculate how much time has passed
                return (float)(realtime.room.time - model.startTime);
            }
        }

        private void Start()
        {
            if (startWatchOnStart)
                realtime.didConnectToRoom += StartStopwatch;
        }

        [EasyButtons.Button]
        public void StartStopwatch()
        {
            model.startTime = realtime.room.time;
        }

        public void StartStopwatch(Normal.Realtime.Realtime realtime)
        {
            model.startTime = realtime.room.time;
        }
    }
}
