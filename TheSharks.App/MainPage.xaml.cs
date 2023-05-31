namespace TheSharks.App;

public partial class MainPage : ContentPage
{
    private string _firstNavigatedUrl = "";
    private bool _firstNavigation = true;
    private string _sessionId;

    public MainPage()
    {
        InitializeComponent();

#if ANDROID
        splashImage.IsVisible = false;
#endif

        mainWebView.IsVisible = false;

        _sessionId = Preferences.Get("SESSION_ID", $"{Guid.NewGuid()}");
        Preferences.Set("SESSION_ID", _sessionId);
    }

    public async Task<bool> NavigateBack()
    {
        var url = await mainWebView.EvaluateJavaScriptAsync("document.URL");

        if (url != _firstNavigatedUrl)
        {
            mainWebView.GoBack();

            return true;
        }

        return false;
    }

    private async Task InitializeWebView()
    {
        try
        {
            // Disable landing screen.
            var jsSetLandingScreen = "localStorage.setItem(\"nolandingscreen\", \"true\");";
            await mainWebView.EvaluateJavaScriptAsync(jsSetLandingScreen);

            // Set session ID.
            var jsSetSessionId = $"sessionStorage.setItem(\"SessionId\", \"{_sessionId}\");";
            await mainWebView.EvaluateJavaScriptAsync(jsSetSessionId);

            // Set IsApp.
            var jsIsApp = $"localStorage.setItem(\"IsApp\", \"true\");";
            await mainWebView.EvaluateJavaScriptAsync(jsIsApp);

            mainWebView.Source = "https://www.thesharks.be";

#if WINDOWS
            await Task.Delay(2000);
#endif

            mainWebView.IsVisible = true;
        }
        catch { }
    }

    private async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        if (_firstNavigation)
        {
            await InitializeWebView();
            _firstNavigation = false;

            await Task.Delay(1000);
            _firstNavigatedUrl = await mainWebView.EvaluateJavaScriptAsync("document.URL");
        }
    }

    private async void WebView_Navigating(object sender, WebNavigatingEventArgs e)
    {
        if (!e.Url.StartsWith("https://www.thesharks.be"))
        {
            e.Cancel = true;

            await Launcher.OpenAsync(e.Url);
        }
    }
}
