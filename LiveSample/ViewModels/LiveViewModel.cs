using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Messages;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using TinyMvvm;



namespace LiveSample.ViewModels
{
    public class LiveViewModel : ViewModelBase
    {
        private HubConnection hub;
        private HttpClient client;

        private int homeGoal = 0;
        public int HomeGoal
        {
            private set => Set(ref homeGoal, value);
            get => homeGoal;
        }

        private int awayGoal = 0;
        public int AwayGoal
        {
            private set => Set(ref awayGoal, value);
            get => awayGoal;
        }

        private string text;
        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        public ObservableCollection<CommentMessage> Comments { get; private set; } = new ObservableCollection<CommentMessage>();

        public ICommand Send => new TinyCommand(async() =>
        {
            var comment = new CommentMessage(QueryParameters["name"], Text);
            var json = JsonConvert.SerializeObject(comment);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("https://livesample.azurewebsites.net/api/AddMessage", content);

            Text = string.Empty;
        });


        public async override Task Initialize()
        {
            await base.Initialize();

            client = new HttpClient();

            var result = await client.GetStringAsync("https://livesample.azurewebsites.net/api/GetInfo");
            var info = JsonConvert.DeserializeObject<ConnectionInfo>(result);

            var connectionBuilder = new HubConnectionBuilder();
            connectionBuilder.WithUrl(info.Url, (Microsoft.AspNetCore.Http.Connections.Client.HttpConnectionOptions obj) =>
            {
                obj.AccessTokenProvider = () => Task.Run(() => info.AccessToken);
            });

            hub = connectionBuilder.Build();


            hub.On<object>(nameof(ResultMessage), (message) =>
            {
                var json = message.ToString();
                var obj = JsonConvert.DeserializeObject<ResultMessage>(json);

                HomeGoal = obj.HomeGoals;
                AwayGoal = obj.AwayGoals;
            });

            hub.On<object>(nameof(CommentMessage), (message) =>
            {
                var json = message.ToString(); 
                 var obj = JsonConvert.DeserializeObject<CommentMessage>(json);

                Comments.Insert(0, obj);
            });

            await hub.StartAsync();
        }
    }
}
