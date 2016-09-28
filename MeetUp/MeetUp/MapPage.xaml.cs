using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetUp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MeetUp
{
    public partial class MapPage : ContentPage
    {
        private readonly MeetUpEvent _meetUpEvent;

        public MapPage(MeetUpEvent meetUpEvent)
        {
            InitializeComponent();
            _meetUpEvent = meetUpEvent;
        }

        protected override void OnAppearing()
        {
            var position = new Position(_meetUpEvent.venue.lat, _meetUpEvent.venue.lon);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(300)));
            if (!MyMap.Pins.Any())
            {
              var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = _meetUpEvent.name,
                    Address = _meetUpEvent.venue.address_1
                };
                MyMap.Pins.Add(pin);
            }
        }
    }
}
