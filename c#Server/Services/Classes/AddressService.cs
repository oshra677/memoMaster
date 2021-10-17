using Common1;
using Google.Maps;
using Google.Maps.Direction;
using Google.Maps.Geocoding;
using Microsoft.Extensions.Configuration;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Services
{
    public class AddressService : IAddressService
    {
        GoogleSigned TestingApiKey;

        public AddressService(string key)
        {
            TestingApiKey = new GoogleSigned(key);
        }

        public AddressService(IConfiguration config)
        {
            var keyFromConfig = config.GetValue(typeof(string), "GoogleApiKey");
            TestingApiKey = new GoogleSigned(keyFromConfig.ToString());
        }

        public CAddress GetAddressFromText(string str, string language)
        {
            CAddress address = new CAddress();
            var request = new GeocodingRequest { Address = str, Language = language };
            var svc = new GeocodingService(TestingApiKey);
            var response = svc.GetResponse(request);


            if (response.Status == ServiceResponseStatus.Ok)
            {
                for (int i = 0; i < response.Results[0].AddressComponents.Length; i++)
                {
                    if (response.Results[0].AddressComponents[i].Types[0] ==
                        Google.Maps.Shared.AddressType.Neighborhood
                        || response.Results[0].AddressComponents[i].Types[0] ==
                        Google.Maps.Shared.AddressType.Intersection)
                    {
                        address.neighborhood = response.Results[0].AddressComponents[i].LongName;
                    }
                    if (response.Results[0].AddressComponents[i].Types[0] ==
                        Google.Maps.Shared.AddressType.Country)
                    {
                        address.country = response.Results[0].AddressComponents[i].LongName;
                    }
                    if (response.Results[0].AddressComponents[i].Types[0] ==
                        Google.Maps.Shared.AddressType.Locality)
                    {
                        address.city = response.Results[0].AddressComponents[i].LongName;
                    }
                    if (response.Results[0].AddressComponents[i].Types[0] ==
                        Google.Maps.Shared.AddressType.Route)
                    {
                        address.street = response.Results[0].AddressComponents[i].LongName;
                    }
                    if (response.Results[0].AddressComponents[i].Types[0] ==
                        Google.Maps.Shared.AddressType.StreetNumber)
                    {
                        address.num = response.Results[0].AddressComponents[i].LongName;
                    }

                }
            }
            return address;
        }

    }
}

    


