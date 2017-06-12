using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ChessClock.Droid
{
    [Activity(Label = "ChessClock", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;


            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            this.Window.DecorView.SystemUiVisibility = StatusBarVisibility.Hidden;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var statusBarHeightInfo = typeof(FormsAppCompatActivity)
                    .GetField("_statusBarHeight", 
                        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                statusBarHeightInfo.SetValue(this, 0);
            }

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());


            var decorView = Window.DecorView;
            StatusBarVisibility uiOptions = decorView.SystemUiVisibility;
            var newUiOptions = (int)uiOptions;

            newUiOptions |= (int)SystemUiFlags.LowProfile;
            newUiOptions |= (int)SystemUiFlags.Fullscreen;
            newUiOptions |= (int)SystemUiFlags.HideNavigation;
            newUiOptions |= (int)SystemUiFlags.Immersive;
            newUiOptions |= (int)SystemUiFlags.ImmersiveSticky;
            newUiOptions |= (int)SystemUiFlags.LayoutFullscreen;

            decorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;

            this.Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            Forms.SetTitleBarVisibility(AndroidTitleBarVisibility.Never);
        }
    }
}

