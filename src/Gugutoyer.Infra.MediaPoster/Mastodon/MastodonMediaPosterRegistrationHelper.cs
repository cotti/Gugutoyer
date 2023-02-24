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

        public async Task Register()
        {
            AppRegistration? appRegistration = null;
            var authClient = new AuthenticationClient(_settings.Instance!);
            try
            {
                appRegistration = authClient.CreateApp(_settings.AppName!, Scope.Read | Scope.Write | Scope.Follow).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var settingsFile = File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json"));

            var config = JsonConvert.DeserializeObject<dynamic>(settingsFile);

            config!["MastodonMediaPosterSettings"]["ClientId"] = appRegistration!.ClientId;
            config!["MastodonMediaPosterSettings"]["ClientSecret"] = appRegistration!.ClientSecret;
            config!["Register"]["MastodonApp"] = false;

            Console.WriteLine("The application cannot run without manual intervention. Authorize the application in the following link:");
            Console.WriteLine(authClient.OAuthUrl());
            Console.WriteLine("Paste the token below:");
            
            config!["MastodonMediaPosterSettings"]["AuthCode"] = Console.ReadLine();



            File.WriteAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json"), JsonConvert.SerializeObject(config, Formatting.Indented));
        }
    }
}
