using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Walkland.Core.PlacesSearchBar
{
    /// <summary>
    /// Place.
    /// </summary>
    public class Place
    {
        /// <summary>
        /// Gets or sets the identifier (can be NULL).
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the place identifier.
        /// </summary>
        /// <value>The place identifier.</value>
        public string Place_ID { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>The reference.</value>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the individual address components.
        /// </summary>
        /// <value>The address components.</value>
        public List<AddressComponent> AddressComponents { get; set; }

        /// <summary>
        /// Gets or sets the address (formatted)
        /// </summary>
        /// <value>The formatted address.</value>
        public string FormattedAddress { get; set; }

        /// <summary>
        /// Gets or sets the (formatted) phone number
        /// </summary>
        /// <value>The phone number.</value>
        public string PhoneNumberFormatted { get; set; }

        /// <summary>
        /// Gets or sets the (international) phone number
        /// </summary>
        /// <value>The phone number.</value>
        public string PhoneNumberInternational { get; set; }

        /// <summary>
        /// Gets the types of this prediction
        /// see https://developers.google.com/places/web-service/supported_types
        /// </summary>
        /// <value>The types of the prediction.</value>
        public List<string> Types { get; set; }

        /// <summary>
        /// Gets or sets the Website URL.
        /// </summary>
        /// <value>The Website URL.</value>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the Vicinity.
        /// </summary>
        /// <value>The Vicinity.</value>
        public string Vicinity { get; set; }

        /// <summary>
        /// Gets or sets the UTC Offset (NULL if not set).
        /// </summary>
        /// <value>The UTC Offset.</value>
        public int? UTCOffset { get; set; }

        /// <summary>
        /// Gets or sets the raw json value.
        /// </summary>
        /// <value>json string.</value>
        public string Raw { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DurianCode.PlacesSearchBar.Place"/> class.
        /// </summary>
        /// <param name="jsonObject">Json object.</param>
        public Place(JObject jsonObject)
        {
            ID = jsonObject["result"]["id"]?.Value<string>();
            Place_ID = jsonObject["result"]["place_id"]?.Value<string>() ?? string.Empty;
            Reference = jsonObject["result"]["reference"]?.Value<string>() ?? string.Empty;
            Name = jsonObject["result"]["name"]?.Value<string>() ?? string.Empty;
            Latitude = jsonObject["result"]?["geometry"]?["location"]?["lat"]?.Value<double>();
            Longitude = jsonObject["result"]?["geometry"]?["location"]?["lng"]?.Value<double>();
            AddressComponents = jsonObject["result"]["address_components"]?.Value<JArray>()?.Select(p => AddressComponent.FromJSON(p.Value<JObject>()))?.ToList() ?? new List<AddressComponent>();
            FormattedAddress = jsonObject["result"]["formatted_address"]?.Value<string>() ?? string.Empty;
            PhoneNumberFormatted = jsonObject["result"]["formatted_phone_number"]?.Value<string>() ?? string.Empty;
            PhoneNumberInternational = jsonObject["result"]["international_phone_number"]?.Value<string>() ?? string.Empty;
            Types = jsonObject["result"]["types"]?.Value<JArray>()?.Select(p => p.Value<string>())?.ToList() ?? new List<string>();
            Website = jsonObject["result"]["website"]?.Value<string>() ?? string.Empty;
            Vicinity = jsonObject["result"]["vicinity"]?.Value<string>() ?? string.Empty;
            UTCOffset = jsonObject["result"]["utc_offset"]?.Value<int>();

            Raw = jsonObject.ToString();
        }

        public AddressComponent GetAddressComponentOrNull(string type)
        {
            foreach (var component in AddressComponents)
            {
                if (component.Types.Contains(type)) return component;
            }
            return null;
        }

        public AddressComponent AdminArea => GetAddressComponentOrNull("administrative_area_level_1");
        public AddressComponent SubAdminArea => GetAddressComponentOrNull("administrative_area_level_2");
        public AddressComponent SubSubAdminArea => GetAddressComponentOrNull("administrative_area_level_3");
        public AddressComponent Locality => GetAddressComponentOrNull("locality");
        public AddressComponent SubLocality => GetAddressComponentOrNull("sublocality_level_1") ?? GetAddressComponentOrNull("sublocality");
        public AddressComponent Thoroughfare => GetAddressComponentOrNull("route");
        public AddressComponent SubThoroughfare => GetAddressComponentOrNull("street_number");
        public AddressComponent PostalCode => GetAddressComponentOrNull("postal_code");
        public AddressComponent Country => GetAddressComponentOrNull("country");
        public AddressComponent StreetName => GetAddressComponentOrNull("route");
        public AddressComponent StreetNumber => GetAddressComponentOrNull("street_number");
    }
}