using Newtonsoft.Json;
using PlexRoulette.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PlexRoulette.Plex
{
    public class PlexApi
    {
        private const string SignInUri = "https://plex.tv/users/sign_in.json";
        private const string FriendsUri = "https://plex.tv/pms/friends/all";
        private const string GetAccountUri = "https://plex.tv/users/account.json";
        private const string ServerUri = "https://plex.tv/pms/servers.xml";

        public async Task<PlexAuthentication> SignIn(UserRequest user)
        {
            var userModel = new PlexUserRequest
            {
                user = user
            };
            var request = new Request(SignInUri, string.Empty, HttpMethod.Post);

            AddHeaders(request);
            request.AddJsonBody(userModel);

            var obj = await Request<PlexAuthentication>(request);

            return obj;
        }

        private void AddHeaders(Request request, string authToken)
        {
            request.AddHeader("X-Plex-Token", authToken);
            AddHeaders(request);
        }

        /// <summary>
        /// Adds the main required headers to the Plex Request
        /// </summary>
        /// <param name="request"></param>
        private void AddHeaders(Request request)
        {
            request.AddHeader("X-Plex-Client-Identifier", "test-value");
            request.AddHeader("X-Plex-Product", "test-value");
            request.AddHeader("X-Plex-Version", "1");
            request.AddContentHeader("Content-Type", request.ContentType == ContentType.Json ? "application/json" : "application/xml");
            request.AddHeader("Accept", "application/json");
        }

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public async Task<T> Request<T>(Request request)
        {
            using (var handler = new HttpClientHandler())
            using (var httpClient = new HttpClient(handler))
            {
                using (var httpRequestMessage = new HttpRequestMessage(request.HttpMethod, request.FullUri))
                {
                    // Add the Json Body
                    if (request.JsonBody != null)
                    {
                        httpRequestMessage.Content = new JsonContent(request.JsonBody);
                    }

                    // Add headers
                    foreach (var header in request.Headers)
                    {
                        httpRequestMessage.Headers.Add(header.Key, header.Value);

                    }
                    using (var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage))
                    {
                        // do something with the response
                        var data = httpResponseMessage.Content;
                        var receivedString = await data.ReadAsStringAsync();
                        if (request.ContentType == ContentType.Json)
                        {
                            request.OnBeforeDeserialization?.Invoke(receivedString);
                            return JsonConvert.DeserializeObject<T>(receivedString, Settings);
                        }
                        else
                        {
                            // XML
                            XmlSerializer serializer = new XmlSerializer(typeof(T));
                            StringReader reader = new StringReader(receivedString);
                            var value = (T)serializer.Deserialize(reader);
                            return value;
                        }
                    }
                }
            }
        }
    }
}
