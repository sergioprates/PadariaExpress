using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Localizacao
{
    public class Geolocalizacao
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public void CarregaGeolocalizacaoGoogleMaps(string json)
        {
            dynamic maps = JsonConvert.DeserializeObject<dynamic>(json);

            Latitude = (Double)maps.results[0].geometry.location.lat;
            Longitude = (Double)maps.results[0].geometry.location.lng;
        }
    }
}
