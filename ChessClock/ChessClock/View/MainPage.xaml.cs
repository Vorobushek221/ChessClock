using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ChessClock.ViewModel;

namespace ChessClock
{
    public partial class MainPage : ContentPage
    {
        private ClockViewModel clockViewModel = new ClockViewModel();

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = clockViewModel;
        }

        private void OnPauseTapped(object sender, EventArgs e)
        {
            if(clockViewModel.GameIsPaused)
            {
                pauseImg.Source = "play_btn.png";
            }
            else
            {
                pauseImg.Source = "pause_btn.png";
            }
        }
    }
}
