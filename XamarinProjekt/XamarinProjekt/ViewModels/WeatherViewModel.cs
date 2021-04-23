using MonkeyCache.SQLite;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using XamarinProjekt.Constants;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinProjekt.Models;
using XamarinProjekt.Services;
using Entry = Microcharts.ChartEntry;
using Microcharts;
using SkiaSharp;

namespace XamarinProjekt.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        public ObservableCollection<WeatherForecast> weatherForecasts { get;}
        public Command LoadItemsCommand { get; }

        public List<Entry> chartEntries { get; }

        private Chart tempChart;
        public Chart TempChart
        {
            get { return tempChart; }
            set { SetProperty(ref tempChart, value); }
        }

        public WeatherViewModel()
        {
            Title = "Weather";
            weatherForecasts = new ObservableCollection<WeatherForecast>();
            tempChart = new LineChart()
            {
                LineSize = 3,
                LineMode = LineMode.Spline

            };
            chartEntries = new List<Entry>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            

            

            IsConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;     //

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;   //

        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)    //
        {
            IsConnected = e.NetworkAccess != NetworkAccess.Internet;
        }

        async Task<IEnumerable<WeatherForecast>> ExecuteLoadItemsCommand()
        {
            weatherForecasts.Clear();
            chartEntries.Clear();
            IsBusy = true;

            

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.ForecastsEndpoint
            };

            string url = builder.Path;

            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                var Forecasts = Barrel.Current.Get<IEnumerable<WeatherForecast>>(key: url);
                foreach (var forecast in Forecasts)
                {
                    weatherForecasts.Add(forecast);
                    
                }
                IsBusy = false;

                foreach (WeatherForecast forecast in weatherForecasts)
                {
                    chartEntries.Add(new Entry(forecast.Temperatur)
                    {
                        Label = forecast.Timestamp.ToString(),
                        ValueLabel = forecast.Temperatur.ToString(),
                        Color = SKColor.Parse("f32121") 
                       
                    });
                }
                tempChart.Entries = chartEntries;
                return weatherForecasts;
            }
            if (!Barrel.Current.IsExpired(key: url))
            {
                
                var Forecasts = Barrel.Current.Get<IEnumerable<WeatherForecast>>(key: url);
                foreach (var forecast in Forecasts)
                {
                    weatherForecasts.Add(forecast);

                }
                IsBusy = false;

                foreach (WeatherForecast forecast in weatherForecasts)
                {
                    chartEntries.Add(new Entry(forecast.Temperatur)
                    {
                        Label = forecast.Timestamp.ToString(),
                        ValueLabel = forecast.Temperatur.ToString(),
                        Color = SKColor.Parse("f32121")

                    });
                }
                tempChart.Entries = chartEntries;
                return weatherForecasts;
            }

            var repos = RestService.For<IBackendService>("https://192.168.1.2:45455/");
            //var repos = RestService.For<IBackendService>("https://azureiothubfunctions.azurewebsites.net/api/GetTelemetryData");

            try
            {
                var Forecasts = await repos.GetWeatherForecast();
                foreach (var forecast in Forecasts)
                {
                    weatherForecasts.Add(forecast);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                
            }
            Barrel.Current.Add(key: url, data: weatherForecasts, expireIn: TimeSpan.FromSeconds(20));

            foreach (WeatherForecast forecast in weatherForecasts)
            {
                chartEntries.Add(new Entry(forecast.Temperatur)
                {
                    Label = forecast.Timestamp.ToString(),
                    ValueLabel = forecast.Temperatur.ToString(),
                    Color = SKColor.Parse("f32121")

                });
            }
            tempChart.Entries = chartEntries;
            return weatherForecasts;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }


    }
}
