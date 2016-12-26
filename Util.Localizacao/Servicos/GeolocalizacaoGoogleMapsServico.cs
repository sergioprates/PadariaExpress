using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Localizacao.Servicos
{
    public class GeolocalizacaoGoogleMapsServico
    {
        private static readonly string _urlGoogleMaps = "http://maps.googleapis.com/maps/api/geocode/json?address=";

        public static Geolocalizacao Geolocalizar(string endereco)
        {
            StringBuilder url = new StringBuilder(_urlGoogleMaps);
            url.Append(endereco);

            string json = Web.GetRequest(url.ToString());

            Geolocalizacao geo = new Geolocalizacao();
            geo.CarregaGeolocalizacaoGoogleMaps(json);
            return geo;
        }
    }
}
