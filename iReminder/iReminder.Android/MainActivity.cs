
using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.CurrentActivity;

namespace iReminder.Droid
{
    [Activity(Label = "iReminder", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(Application);
            Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this,savedInstanceState);
            LoadApplication(new App());
        }
    }
}