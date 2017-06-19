using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChessClock.Model
{
    public class Clock : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private TimeSpan topTime;
        public TimeSpan TopTime
        {
            get
            {
                return topTime;
            }
            set
            {
                if(topTime != value)
                {
                    topTime = value;
                    OnPropertyChanged("TopTime");

                }
            }
        }

        private TimeSpan botTime;
        public TimeSpan BotTime
        {
            get
            {
                return botTime;
            }
            set
            {
                if (botTime != value)
                {
                    botTime = value;
                    OnPropertyChanged("BotTime");

                }
            }
        }

        public ClockState State { get; set; }

        public Clock(TimeSpan topTime, TimeSpan botTime)
        {
            this.TopTime = topTime;
            this.BotTime = botTime;

            State = ClockState.NotStarted;
            StartTimer();
        }

        public Clock()
        {
        }

        public void TogglePause()
        {
            switch(State)
            {
                case ClockState.BotTimerIsOn:
                    State = ClockState.PausedByBot;
                    break;
                case ClockState.TopTimerIsOn:
                    State = ClockState.PausedByTop;
                    break;
                case ClockState.PausedByBot:
                    State = ClockState.BotTimerIsOn;
                    break;
                case ClockState.PausedByTop:
                    State = ClockState.TopTimerIsOn;
                    break;
            }
        }

        public void EnableTopTimer()
        {
            State = ClockState.TopTimerIsOn;
        }

        public void EnableBotTimer()
        {
            State = ClockState.BotTimerIsOn;
        }

        public void StartTimer()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () => HandleIteration());
        }

        private bool HandleIteration()
        {
            switch (State)
            {
                case ClockState.TopTimerIsOn:
                    TopTime = TopTime.Subtract(new TimeSpan(0, 0, 0, 0, 100));
                    if (TopTime.TotalMilliseconds == 0)
                    {
                        State = ClockState.Finished;
                    }
                    return true;

                case ClockState.BotTimerIsOn:
                    BotTime = BotTime.Subtract(new TimeSpan(0, 0, 0, 0, 100));
                    if (BotTime.TotalMilliseconds == 0)
                    {
                        State = ClockState.Finished;
                    }
                    return true;

                case ClockState.NotStarted:
                    return true;

                case ClockState.PausedByBot:
                    return true;

                case ClockState.PausedByTop:
                    return true;

                case ClockState.Finished:
                    return false;

                default:
                    return false;
            }
        }
    }
}
