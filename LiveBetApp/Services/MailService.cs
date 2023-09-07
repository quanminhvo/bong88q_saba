using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace LiveBetApp.Services
{
    public class MailService
    {
        private string[] _scopes = { GmailService.Scope.GmailReadonly };
        private string _applicationName = "Gmail API .NET Quickstart";

        public bool CheckAvailable()
        {
            try
            {
                string token = Common.Config.GetLicense().token;

                UserCredential credential;
                string credentialsPath = "credentials.json";
                string tokenStorePath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\token.json";

                using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        _scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(tokenStorePath, false)
                    ).Result;
                }

                // Create Gmail API service.
                var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = _applicationName,
                });


                var re = service.Users.Messages.List("me");
                re.LabelIds = "INBOX";

                var res = re.Execute();

                if (res != null && res.Messages != null)
                {
                    foreach (var email in res.Messages)
                    {
                        var emailInfoReq = service.Users.Messages.Get("me", email.Id);
                        var emailInfoResponse = emailInfoReq.Execute();

                        if (emailInfoResponse != null)
                        {
                            string from = "";
                            string subject = "";
                            //loop through the headers and get the fields we need...
                            foreach (var headerItem in emailInfoResponse.Payload.Headers)
                            {
                                if (headerItem.Name == "From")
                                {
                                    from = headerItem.Value;
                                }
                                else if (headerItem.Name == "Subject")
                                {
                                    subject = headerItem.Value;
                                }
                            }

                            if ((from.ToLower().Contains("quanminhvo@gmail.com") || from.ToLower().Contains("huuvinh2234@gmail.com")) && subject.ToUpper() == token)
                            {
                                return true;
                            }

                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckPassword(string password)
        {
            try
            {
                string token = password;

                UserCredential credential;
                string credentialsPath = "credentials.json";
                string tokenStorePath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\token.json";

                using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        _scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(tokenStorePath, false)
                    ).Result;
                }

                // Create Gmail API service.
                var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = _applicationName,
                });


                var re = service.Users.Messages.List("me");
                re.LabelIds = "INBOX";

                var res = re.Execute();

                if (res != null && res.Messages != null)
                {
                    foreach (var email in res.Messages)
                    {
                        var emailInfoReq = service.Users.Messages.Get("me", email.Id);
                        var emailInfoResponse = emailInfoReq.Execute();

                        if (emailInfoResponse != null)
                        {
                            string from = "";
                            string subject = "";
                            //loop through the headers and get the fields we need...
                            foreach (var headerItem in emailInfoResponse.Payload.Headers)
                            {
                                if (headerItem.Name == "From")
                                {
                                    from = headerItem.Value;
                                }
                                else if (headerItem.Name == "Subject")
                                {
                                    subject = headerItem.Value;
                                }
                            }

                            if ((from.ToLower().Contains("quanminhvo@gmail.com") || from.ToLower().Contains("huuvinh2234@gmail.com")) && subject.ToUpper() == token)
                            {
                                return true;
                            }

                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
