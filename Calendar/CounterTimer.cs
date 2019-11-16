using System;
using System.Threading;

namespace Calendar
{
    public class CounterTimer
    {

        public Timer timer;

        public delegate void EndOfACount();
        public EndOfACount endOfACount;
        public delegate void ViewDelegateText(string text);
        public delegate void ViewDelegateOpacity(double opacity);
        public ViewDelegateText ViewTimerText;
        public ViewDelegateText ViewTimerTextInfo;
        public ViewDelegateOpacity ViewTimerTextOpacity;

        public NowDay ObjectNowDay;
        public TimeSpan TimeToEndOfACounter = new TimeSpan(0);
        public TimeSpan TimeToStartOfACounter = new TimeSpan(0);
        public TimeSpan IntervalCounter = new TimeSpan(0);

        public CounterTimer(NowDay ObjectNowDay)
        {
            timer = new Timer(OnTimedEvent, 0, 10, 10);

            this.ObjectNowDay = ObjectNowDay;
        }
        public void StartCounterTimer() 
        {
           // timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        public void StopCounterTimer() 
        {
            timer.Change(Timeout.Infinite, 0);
        }
        private void OnTimedEvent(Object obj)
        {

            //Если изменилос оставшееся время в секундах, такая сложная конструкция, чтобы окрулить до секунды.
            if ((int)((TimeToEndOfACounter - DateTime.Now.TimeOfDay).TotalSeconds) != IntervalCounter.TotalSeconds)
            {
                IntervalCounter = TimeSpan.FromSeconds((int)((TimeToEndOfACounter - DateTime.Now.TimeOfDay).TotalSeconds));

                int progress_bar = (int)(100 - Math.Round(((IntervalCounter.TotalSeconds) / (TimeToEndOfACounter.TotalSeconds - TimeToStartOfACounter.TotalSeconds)) * 100.0));
                if (progress_bar > 100) { progress_bar = 100; }

                ViewTimerTextInfo("Время отметки: " + TimeToEndOfACounter.ToString("hh':'mm':'ss"));
                ViewTimerText(IntervalCounter.ToString("hh':'mm':'ss"));
                ViewTimerTextOpacity(0.6);


                ObjectNowDay.Progress_bar = progress_bar;

                int Counter = 0;


                if (IntervalCounter <= TimeSpan.FromSeconds(0))
                {

                    ViewTimerTextInfo("");
                    ViewTimerText(IntervalCounter.ToString("hh':'mm':'ss"));
                    ViewTimerTextOpacity(1);
                    StopCounterTimer();
                    endOfACount();
                }

            }
        }
    }

}


