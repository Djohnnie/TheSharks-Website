using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;

namespace TheSharks.App;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    private readonly Android.Graphics.Color _portraitNavigationColor = Android.Graphics.Color.Rgb(177, 234, 242);
    private readonly Android.Graphics.Color _landscapeNavigationColor = Android.Graphics.Color.Rgb(0, 172, 193);

    protected override void OnCreate(Bundle savedInstanceState)
    {
        Window.SetNavigationBarColor(_portraitNavigationColor);

        base.OnCreate(savedInstanceState);
    }

    public override void OnConfigurationChanged(Configuration newConfig)
    {
        if (newConfig.Orientation == Orientation.Portrait)
        {
            Window.SetNavigationBarColor(_portraitNavigationColor);
        }
        else
        {
            Window.SetNavigationBarColor(_landscapeNavigationColor);
        }

        base.OnConfigurationChanged(newConfig);
    }

    public override async void OnBackPressed()
    {
        var mainPage = ServiceHelper.GetService<MainPage>();

        if (mainPage != null && !(await mainPage.NavigateBack()))
        {
            base.OnBackPressed();
        }
    }
}