using System;
using System.Threading;

namespace Calendar
{
    public class CounterTimer
    {

        public Thread timer;

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

            this.ObjectNowDay = ObjectNowDay;
        }

        public void StartCounterTimer()
        {
            try
            {
                while (true)
                {
                    //Если изменилось оставшееся время в секундах, такая сложная конструкция, чтобы окрулить до секунды.
                    if ((int)((TimeToEndOfACounter - DateTime.Now.TimeOfDay).TotalSeconds) != IntervalCounter.TotalSeconds)
                    {
                        ObjectNowDay.ViewProgressBar = true;

                        IntervalCounter = TimeSpan.FromSeconds((int)((TimeToEndOfACounter - DateTime.Now.TimeOfDay).TotalSeconds));

                        int progress_bar = (int)(100 - Math.Round(((IntervalCounter.TotalSeconds) / (TimeToEndOfACounter.TotalSeconds - TimeToStartOfACounter.TotalSeconds)) * 100.0));
                        if (progress_bar > 100) { progress_bar = 100; }

                        ViewTimerTextInfo("Время отметки: " + TimeToEndOfACounter.ToString("hh':'mm':'ss"));
                        ViewTimerText(IntervalCounter.ToString("hh':'mm':'ss"));
                        ViewTimerTextOpacity(0.6);

                        ObjectNowDay.Progress_bar = progress_bar;


                        if (IntervalCounter <= TimeSpan.FromSeconds(0))
                        {

                            ViewTimerTextInfo("");
                            ViewTimerText(IntervalCounter.ToString("hh':'mm':'ss"));
                            ViewTimerTextOpacity(1);
                            endOfACount();
                            break;
                        }

                    }
                    Thread.Sleep(200);
                }

            }
            catch (ThreadAbortException) 
            {
                ViewTimerTextInfo("");
                ViewTimerText("");
                ObjectNowDay.ViewProgressBar = false;
            }
        }
    }

}


