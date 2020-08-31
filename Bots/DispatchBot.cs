// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Covid19Bot.ProcessJson;



namespace Microsoft.BotBuilderSamples
{
    public class DispatchBot : ActivityHandler
    {
        private readonly ILogger<DispatchBot> _logger;
        private readonly IBotServices _botServices;

        private static readonly HttpClient client = new HttpClient();

        private readonly string[] _cards = {
            Path.Combine (".", "Cards", "Covid19Status.json"),
            Path.Combine (".", "Cards", "GlobalStatus.json"),
        };

        public DispatchBot(IBotServices botServices, ILogger<DispatchBot> logger)
        {
            _logger = logger;
            _botServices = botServices;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            // First, we use the dispatch model to determine which cognitive service (LUIS or QnA) to use.
            var recognizerResult = await _botServices.Dispatch.RecognizeAsync(turnContext, cancellationToken);
            // Top intent tell us which cognitive service to use.
            var topIntent = recognizerResult.GetTopScoringIntent();

            // Next, we call the dispatcher with the top intent.
            await DispatchToTopIntentAsync(turnContext, topIntent.intent, recognizerResult, cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            const string WelcomeText = "How can I help you? You can ask me informational questions about COVID-19 and statistical question regarding the pandemic's spread.";

            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Welcome to Covid19Bot. {WelcomeText}"), cancellationToken);
                }
            }
        }

        private async Task DispatchToTopIntentAsync(ITurnContext<IMessageActivity> turnContext, string intent, RecognizerResult recognizerResult, CancellationToken cancellationToken)
        {
            switch (intent)
            {
                case "l_covid19":
                    await ProcessCovid19LuisAsync(turnContext, recognizerResult.Properties["luisResult"] as LuisResult, cancellationToken);
                    break;
                // case "l_Weather":
                //     await ProcessWeatherAsync(turnContext, recognizerResult.Properties["luisResult"] as LuisResult, cancellationToken);
                //     break;
                case "q_covid19":
                    await ProcessSampleQnAAsync(turnContext, cancellationToken);
                    break;
                default:
                    _logger.LogInformation($"Covid19Bot unrecognized you.");
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Covid19Bot unrecognized you, kindly type a question as what is the symptoms of covid19."), cancellationToken);
                    break;
            }
        }

        private async Task ProcessCovid19LuisAsync(ITurnContext<IMessageActivity> turnContext, LuisResult luisResult, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProcessCovid19LuisAsync");

            // Retrieve LUIS result for Process Automation.
            var result = luisResult.ConnectedServiceResult;
            var topIntent = result.TopScoringIntent.Intent;


            if (topIntent == "Covid19US")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[177].TotalConfirmed}, Total Deaths: { root.Countries[177].TotalDeaths}, Total Recovered: { root.Countries[177].TotalRecovered}");
                }
            }

            else
            if (topIntent == "Covid19India")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[76].TotalConfirmed}, Total Deaths: { root.Countries[76].TotalDeaths}, Total Recovered: { root.Countries[76].TotalRecovered}");
                }

            }


            else
            if (topIntent == "Covid19China")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[35].TotalConfirmed}, Total Deaths: { root.Countries[35].TotalDeaths}, Total Recovered: { root.Countries[35].TotalRecovered}");
                }

            }

            else
            if (topIntent == "Covid19Russia")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[138].TotalConfirmed}, Total Deaths: { root.Countries[138].TotalDeaths}, Total Recovered: { root.Countries[138].TotalRecovered}");
                }

            }


            else
            if (topIntent == "Covid19UK")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[176].TotalConfirmed}, Total Deaths: { root.Countries[176].TotalDeaths}, Total Recovered: { root.Countries[176].TotalRecovered}");
                }

            }



            else
            if (topIntent == "Covid19France")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[59].TotalConfirmed}, Total Deaths: { root.Countries[59].TotalDeaths}, Total Recovered: { root.Countries[59].TotalRecovered}");
                }

            }


            else
            if (topIntent == "Covid19India")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[76].TotalConfirmed}, Total Deaths: { root.Countries[177].TotalDeaths}, Total Recovered: { root.Countries[177].TotalRecovered}");
                }

            }

            else
            if (topIntent == "Covid19Spain")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[156].TotalConfirmed}, Total Deaths: { root.Countries[156].TotalDeaths}, Total Recovered: { root.Countries[156].TotalRecovered}");
                }

            }


            else
            if (topIntent == "Covid19Germany")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[63].TotalConfirmed}, Total Deaths: { root.Countries[63].TotalDeaths}, Total Recovered: { root.Countries[63].TotalRecovered}");
                }

            }



            else
            if (topIntent == "Covid19Japan")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[84].TotalConfirmed}, Total Deaths: { root.Countries[84].TotalDeaths}, Total Recovered: { root.Countries[84].TotalRecovered}");
                }

            }


            else
            if (topIntent == "Covid19Italy")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[82].TotalConfirmed}, Total Deaths: { root.Countries[82].TotalDeaths}, Total Recovered: { root.Countries[82].TotalRecovered}");
                }

            }


            else
            if (topIntent == "Covid19Iran")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[78].TotalConfirmed}, Total Deaths: { root.Countries[78].TotalDeaths}, Total Recovered: { root.Countries[78].TotalRecovered}");
                }

            }


            else
            if (topIntent == "Covid19Turkey")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[172].TotalConfirmed}, Total Deaths: { root.Countries[172].TotalDeaths}, Total Recovered: { root.Countries[172].TotalRecovered}");
                }

            }

            else
            if (topIntent == "Covid19Brazil")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[23].TotalConfirmed}, Total Deaths: { root.Countries[23].TotalDeaths}, Total Recovered: { root.Countries[23].TotalRecovered}");
                }

            }

            else
            if (topIntent == "Covid19Egypt")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[51].TotalConfirmed}, Total Deaths: { root.Countries[51].TotalDeaths}, Total Recovered: { root.Countries[51].TotalRecovered}");
                }

            }

            else
            if (topIntent == "Covid19Portugal")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[138].TotalConfirmed}, Total Deaths: { root.Countries[138].TotalDeaths}, Total Recovered: { root.Countries[138].TotalRecovered}");
                }

            }

            else
            if (topIntent == "Covid19Afghanistan")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[1].TotalConfirmed}, Total Deaths: { root.Countries[1].TotalDeaths}, Total Recovered: { root.Countries[1].TotalRecovered}");
                }

            }

            else
            if (topIntent == "Covid19Canada")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[30].TotalConfirmed}, Total Deaths: { root.Countries[30].TotalDeaths}, Total Recovered: { root.Countries[30].TotalRecovered}");
                }

            }

            else
            if (topIntent == "Covid19Korea")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[88].TotalConfirmed}, Total Deaths: { root.Countries[88].TotalDeaths}, Total Recovered: { root.Countries[88].TotalRecovered}");
                }

            }

            else
            if (topIntent == "Covid19Argentina")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[6].TotalConfirmed}, Total Deaths: { root.Countries[6].TotalDeaths}, Total Recovered: { root.Countries[6].TotalRecovered}");
                }

            }

            else
            if (topIntent == "Covid19Israel")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[81].TotalConfirmed}, Total Deaths: { root.Countries[81].TotalDeaths}, Total Recovered: { root.Countries[81].TotalRecovered}");
                }

            }

            else
            if (topIntent == "Covid19Mexico")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Countries[109].TotalConfirmed}, Total Deaths: { root.Countries[109].TotalDeaths}, Total Recovered: { root.Countries[109].TotalRecovered}");
                }

            }

            else
            if (topIntent == "WorldCovid19")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://api.covid19api.com/summary");

                    Root root = JsonConvert.DeserializeObject<Root>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root.Global.TotalConfirmed}, Total Deaths: { root.Global.TotalDeaths}, Total Recovered: { root.Global.TotalRecovered}");
                }
                
            }

            else
            if (topIntent == "Covid19California")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[0].Cases}, Total Deaths: {root_Arjun[0].Deaths}, Total Recovered: {root_Arjun[0].Recovered}");
                }

            }

            else
            if (topIntent == "Texas")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[1].Cases}, Total Deaths: {root_Arjun[1].Deaths}, Total Recovered: {root_Arjun[1].Recovered}");
                }

            }

            else
            if (topIntent == "Florida")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[2].Cases}, Total Deaths: {root_Arjun[2].Deaths}, Total Recovered: {root_Arjun[2].Recovered}");
                }

            }

            else
            if (topIntent == "New York")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[3].Cases}, Total Deaths: {root_Arjun[3].Deaths}, Total Recovered: {root_Arjun[3].Recovered}");
                }

            }

            else
            if (topIntent == "Georgia")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[4].Cases}, Total Deaths: {root_Arjun[4].Deaths}, Total Recovered: {root_Arjun[4].Recovered}");
                }

            }

            else
            if (topIntent == "Illinois")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[5].Cases}, Total Deaths: {root_Arjun[5].Deaths}, Total Recovered: {root_Arjun[5].Recovered}");
                }

            }

            else
            if (topIntent == "Arizona")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[6].Cases}, Total Deaths: {root_Arjun[6].Deaths}, Total Recovered: {root_Arjun[6].Recovered}");
                }

            }

            else
            if (topIntent == "New Jersey")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[7].Cases}, Total Deaths: {root_Arjun[7].Deaths}, Total Recovered: {root_Arjun[7].Recovered}");
                }

            }

            else
            if (topIntent == "North Carolina")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[8].Cases}, Total Deaths: {root_Arjun[8].Deaths}, Total Recovered: {root_Arjun[8].Recovered}");
                }

            }

            else
            if (topIntent == "Tennessee")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[9].Cases}, Total Deaths: {root_Arjun[9].Deaths}, Total Recovered: {root_Arjun[9].Recovered}");
                }

            }


            else
            if (topIntent == "Louisiana")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[10].Cases}, Total Deaths: {root_Arjun[10].Deaths}, Total Recovered: {root_Arjun[10].Recovered}");
                }

            }




            else
            if (topIntent == "Pennsylvania")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[11].Cases}, Total Deaths: {root_Arjun[11].Deaths}, Total Recovered: {root_Arjun[11].Recovered}");
                }

            }



            else
            if (topIntent == "Massachusetts")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[12].Cases}, Total Deaths: {root_Arjun[12].Deaths}, Total Recovered: {root_Arjun[12].Recovered}");
                }

            }


            else
            if (topIntent == "Alabama")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[13].Cases}, Total Deaths: {root_Arjun[13].Deaths}, Total Recovered: {root_Arjun[13].Recovered}");
                }

            }

            else
            if (topIntent == "Ohio")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[14].Cases}, Total Deaths: {root_Arjun[14].Deaths}, Total Recovered: {root_Arjun[14].Recovered}");
                }

            }

            else
            if (topIntent == "Virginia")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[15].Cases}, Total Deaths: {root_Arjun[15].Deaths}, Total Recovered: {root_Arjun[15].Recovered}");
                }

            }

            else
            if (topIntent == "South Carolina")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[16].Cases}, Total Deaths: {root_Arjun[16].Deaths}, Total Recovered: {root_Arjun[16].Recovered}");
                }

            }

            else
            if (topIntent == "Michigan")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[17].Cases}, Total Deaths: {root_Arjun[17].Deaths}, Total Recovered: {root_Arjun[17].Recovered}");
                }

            }

            else
            if (topIntent == "Maryland")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[18].Cases}, Total Deaths: {root_Arjun[18].Deaths}, Total Recovered: {root_Arjun[18].Recovered}");
                }

            }

            else
            if (topIntent == "Indiana")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[19].Cases}, Total Deaths: {root_Arjun[19].Deaths}, Total Recovered: {root_Arjun[19].Recovered}");
                }

            }

            else
            if (topIntent == "Mississippi")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[20].Cases}, Total Deaths: {root_Arjun[20].Deaths}, Total Recovered: {root_Arjun[20].Recovered}");
                }

            }

            else
            if (topIntent == "Missouri")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[21].Cases}, Total Deaths: {root_Arjun[21].Deaths}, Total Recovered: {root_Arjun[21].Recovered}");
                }

            }

            else
            if (topIntent == "Washington")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[22].Cases}, Total Deaths: {root_Arjun[22].Deaths}, Total Recovered: {root_Arjun[22].Recovered}");
                }

            }

            else
            if (topIntent == "Wisconsin")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[23].Cases}, Total Deaths: {root_Arjun[23].Deaths}, Total Recovered: {root_Arjun[23].Recovered}");
                }

            }

            else
            if (topIntent == "Minnesota")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[24].Cases}, Total Deaths: {root_Arjun[24].Deaths}, Total Recovered: {root_Arjun[24].Recovered}");
                }

            }

            else
            if (topIntent == "Nevada")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[25].Cases}, Total Deaths: {root_Arjun[25].Deaths}, Total Recovered: {root_Arjun[25].Recovered}");
                }

            }


            else
            if (topIntent == "Iowa")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[26].Cases}, Total Deaths: {root_Arjun[26].Deaths}, Total Recovered: {root_Arjun[26].Recovered}");
                }

            }

            else
            if (topIntent == "Arkansas")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[27].Cases}, Total Deaths: {root_Arjun[27].Deaths}, Total Recovered: {root_Arjun[27].Recovered}");
                }

            }

            else
            if (topIntent == "Colorado")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[28].Cases}, Total Deaths: {root_Arjun[28].Deaths}, Total Recovered: {root_Arjun[28].Recovered}");
                }

            }

            else
            if (topIntent == "Oklahoma")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[29].Cases}, Total Deaths: {root_Arjun[29].Deaths}, Total Recovered: {root_Arjun[29].Recovered}");
                }

            }


            else
            if (topIntent == "Connecticut")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[30].Cases}, Total Deaths: {root_Arjun[30].Deaths}, Total Recovered: {root_Arjun[30].Recovered}");
                }

            }

            else
            if (topIntent == "Utah")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[31].Cases}, Total Deaths: {root_Arjun[31].Deaths}, Total Recovered: {root_Arjun[31].Recovered}");
                }

            }

            else
            if (topIntent == "Kentucky")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[32].Cases}, Total Deaths: {root_Arjun[32].Deaths}, Total Recovered: {root_Arjun[32].Recovered}");
                }

            }

            else
            if (topIntent == "Kansas")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[33].Cases}, Total Deaths: {root_Arjun[33].Deaths}, Total Recovered: {root_Arjun[34].Recovered}");
                }

            }

            else
            if (topIntent == "Nebraska")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[34].Cases}, Total Deaths: {root_Arjun[34].Deaths}, Total Recovered: {root_Arjun[34].Recovered}");
                }

            }

            else
            if (topIntent == "Idaho")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[35].Cases}, Total Deaths: {root_Arjun[35].Deaths}, Total Recovered: {root_Arjun[35].Recovered}");
                }

            }

            else
            if (topIntent == "Oregon")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[36].Cases}, Total Deaths: {root_Arjun[36].Deaths}, Total Recovered: {root_Arjun[36].Recovered}");
                }

            }

            else
            if (topIntent == "New Mexico")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[37].Cases}, Total Deaths: {root_Arjun[37].Deaths}, Total Recovered: {root_Arjun[37].Recovered}");
                }

            }

            else
            if (topIntent == "Rhode Island")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[38].Cases}, Total Deaths: {root_Arjun[38].Deaths}, Total Recovered: {root_Arjun[38].Recovered}");
                }

            }

            else
            if (topIntent == "Delaware")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[39].Cases}, Total Deaths: {root_Arjun[39].Deaths}, Total Recovered: {root_Arjun[39].Recovered}");
                }

            }

            else
            if (topIntent == "District Of Columbia")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[40].Cases}, Total Deaths: {root_Arjun[40].Deaths}, Total Recovered: {root_Arjun[40].Recovered}");
                }

            }

            else
            if (topIntent == "South Dakota")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[41].Cases}, Total Deaths: {root_Arjun[41].Deaths}, Total Recovered: {root_Arjun[41].Recovered}");
                }

            }

            else
            if (topIntent == "North Dakota")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[42].Cases}, Total Deaths: {root_Arjun[42].Deaths}, Total Recovered: {root_Arjun[42].Recovered}");
                }

            }

            else
            if (topIntent == "West Virginia")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[43].Cases}, Total Deaths: {root_Arjun[43].Deaths}, Total Recovered: {root_Arjun[43].Recovered}");
                }

            }

            else
            if (topIntent == "Hawaii")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[44].Cases}, Total Deaths: {root_Arjun[44].Deaths}, Total Recovered: {root_Arjun[44].Recovered}");
                }

            }

            else
            if (topIntent == "New Hampshire")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[45].Cases}, Total Deaths: {root_Arjun[45].Deaths}, Total Recovered: {root_Arjun[45].Recovered}");
                }

            }

            else
            if (topIntent == "Montana")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[46].Cases}, Total Deaths: {root_Arjun[46].Deaths}, Total Recovered: {root_Arjun[46].Recovered}");
                }

            }

            else
            if (topIntent == "Alaska")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[47].Cases}, Total Deaths: {root_Arjun[47].Deaths}, Total Recovered: {root_Arjun[47].Recovered}");
                }

            }

            else
            if (topIntent == "Maine")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[48].Cases}, Total Deaths: {root_Arjun[48].Deaths}, Total Recovered: {root_Arjun[48].Recovered}");
                }

            }

            else
            if (topIntent == "Wyoming")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[49].Cases}, Total Deaths: {root_Arjun[49].Deaths}, Total Recovered: {root_Arjun[49].Recovered}");
                }

            }

            else
            if (topIntent == "Vermont")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[50].Cases}, Total Deaths: {root_Arjun[50].Deaths}, Total Recovered: {root_Arjun[50].Recovered}");
                }

            }

            else
            if (topIntent == "Guam")
            {

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString("https://disease.sh/v3/covid-19/states");

                    List<StatesList_Arjun> root_Arjun = JsonConvert.DeserializeObject<List<StatesList_Arjun>>(rawJSON);

                    await turnContext.SendActivityAsync($"Total Confirmed: { root_Arjun[51].Cases}, Total Deaths: {root_Arjun[51].Deaths}, Total Recovered: {root_Arjun[51].Recovered}");
                }

            }

            else
            {
                _logger.LogInformation($"Luis unrecognized intent.");
                await turnContext.SendActivityAsync(MessageFactory.Text($"Bot unrecognized your inputs, kindly reply as live covid19 status."), cancellationToken);
            }
        }

        private async Task ProcessSampleQnAAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProcessSampleQnAAsync");

            var results = await _botServices.SampleQnA.GetAnswersAsync(turnContext);
            if (results.Any())
            {
                await turnContext.SendActivityAsync(MessageFactory.Text(results.First().Answer), cancellationToken);
            }
            else
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Sorry, could not find an answer in the Q and A system."), cancellationToken);
            }
        }
    }
}
