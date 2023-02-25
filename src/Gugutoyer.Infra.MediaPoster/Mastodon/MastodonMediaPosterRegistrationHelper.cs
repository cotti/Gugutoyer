using Mastonet.Entities;
using Mastonet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gugutoyer.Infra.MediaPoster.Mastodon
{
    public class MastodonMediaPosterRegistrationHelper
    {
        private readonly MastodonMediaPosterRegistrationHelperSettings _settings;
        private readonly HttpClient _httpClient;
        public MastodonMediaPosterRegistrationHelper(MastodonMediaPosterRegistrationHelperSettings settings, IHttpClientFactory httpClientFactory)
        {
            _settings = settings;
            _httpClient = httpClientFactory.CreateClient("mastodon");
        }

        public void Register()
        {
            AppRegistration? appRegistration = null;
            var authClient = new AuthenticationClient(_settings.Instance);
            try
            {
                appRegistration = authClient.CreateApp(_settings.AppName, Scope.Read | Scope.Write | Scope.Follow).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            authClient.AppRegistration = appRegistration;

            var settingsFile = File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json"));

            var config = JsonConvert.DeserializeObject<dynamic>(settingsFile);

            config!["MastodonMediaPosterSettings"]["ClientId"] = appRegistration!.ClientId;
            config!["MastodonMediaPosterSettings"]["ClientSecret"] = appRegistration!.ClientSecret;
            config!["Register"]["MastodonApp"] = false;

            Console.WriteLine("The application cannot run without manual intervention. Authorize the application in the following link:");
            Console.WriteLine(authClient.OAuthUrl());
            Console.WriteLine("Paste the token below:");
            
            string authCode = Console.ReadLine()!;
            config!["MastodonMediaPosterSettings"]["AuthCode"] = authCode;

            var accessToken = authClient.ConnectWithCode(authCode).Result;
            config!["MastodonMediaPosterSettings"]["AccessToken"] = accessToken.AccessToken;

            File.WriteAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json"), JsonConvert.SerializeObject(config, Formatting.Indented));
        }
    }
}
