using ChessClock.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChessClock.ViewModel
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand PushTopTimeCommand { protected set; get; }
        public ICommand PushBotTimeCommand { protected set; get; }
        public ICommand PushPauseCommand { protected set; get; }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void Clock_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "TopTime":
                    OnPropertyChanged("TopTime");
                    break;
                case "BotTime":
                    OnPropertyChanged("BotTime");
                    break;
            }
        }

        public Clock Clock { get; set; }

        public ClockViewModel()
        {
            Clock = new Clock(new TimeSpan(0, 3, 0), new TimeSpan(0, 3, 0));

            Clock.PropertyChanged += Clock_PropertyChanged;
            this.PushBotTimeCommand = new Command(PushBotTimer);
            this.PushTopTimeCommand = new Command(PushTopTimer);
            this.PushPauseCommand = new Command(PushPause);
        }

        public void PushTopTimer()
        {
            Clock.EnableBotTimer();
        }

        public void PushBotTimer()
        {
            Clock.EnableTopTimer();
        }

        public void PushPause()
        {
            Clock.TogglePause();
        }

        public TimeSpan TopTime
        {
            get
            {
                return Clock.TopTime;
            }
            set
            {
                Clock.TopTime = value;
                OnPropertyChanged("TopTime");
            }
        }

        public TimeSpan BotTime
        {
            get
            {
                return Clock.BotTime;
            }
            set
            {
                Clock.BotTime = value;
                OnPropertyChanged("BotTime");
            }
        }
    }
}
