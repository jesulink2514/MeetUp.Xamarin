using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetUp.Infrastructure;
using MeetUp.Models;
using Xamarin.Forms;

namespace MeetUp
{
    public partial class MainPage : ContentPage
    {
        public const string GroupName = "Software-Craftsmanship-Peru";
        private readonly EventService _eventService = new EventService("");
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            LstEvents.IsRefreshing = true;
            await RefreshItems();
            LstEvents.IsRefreshing = false;
        }

        private async Task RefreshItems()
        {
            var events = (await _eventService.GetEvents(GroupName)).results;
            LstEvents.ItemsSource = events;
        }

        private void LstEvents_OnRefreshing(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async() =>
            {
                await RefreshItems();
                LstEvents.IsRefreshing = false;
            });
        }

        private void OpenBrowser_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                var item = menuItem.BindingContext as MeetUpEvent;
                Device.OpenUri(new Uri(item?.event_url));
            }
        }

        private async void LstEvents_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var meetup = e.Item as MeetUpEvent;
            if (meetup != null && meetup.venue != null)
            {
                var pageMap = new MapPage(meetup);
                await Navigation.PushAsync(pageMap);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Meetup", "No se ha especificado una direccion", "Ok");
            }

        }
    }
}
