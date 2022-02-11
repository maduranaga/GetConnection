using GetConnection.Core.Entities;
using GetConnection.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Infrastructure.Services
{
  
        public  class PushNotificationLogic: IPushNotificationLogic
    {
            private static Uri FireBasePushNotificationsURL = new Uri("https://fcm.googleapis.com/fcm/send");
            private static string ServerKey = "589c04c8a8037e8dfc580a16f182bfb3dcb4e0e4";
   
            public  async Task<bool> SendPushNotification(string[] deviceTokens, string title, string body, object data)
            {
                bool sent = false;

                if (deviceTokens.Count() > 0)
                {

                    var messageInformation = new Message()
                    {
                        notification = new Notification()
                        {
                            title = title,
                            text = body
                        },
                        data = data,
                        registration_ids = deviceTokens
                    };

                    //Object to JSON STRUCTURE => using Newtonsoft.Json;
                    string jsonMessage = JsonConvert.SerializeObject(messageInformation);

                    /*
                     ------ JSON STRUCTURE ------
                     {
                        notification: {
                                        title: "",
                                        text: ""
                                        },
                        data: {
                                action: "Play",
                                playerId: 5
                                },
                        registration_ids = ["id1", "id2"]
                     }
                     ------ JSON STRUCTURE ------
                     */

                    //Create request to Firebase API
                    var request = new HttpRequestMessage(HttpMethod.Post, FireBasePushNotificationsURL);

                    request.Headers.TryAddWithoutValidation("Authorization", "key=" + ServerKey);
                    request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

                    HttpResponseMessage result;
                    using (var client = new HttpClient())
                    {
                        result = await client.SendAsync(request);
                        sent = sent && result.IsSuccessStatusCode;
                    }
                }

                return sent;
            }

        }
    }



