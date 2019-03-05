using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VehicleTeleporter
{
    public class ConfigurationVehicleTeleporter : IRocketPluginConfiguration
    {
        public bool RemoveVehicleOnGet;
        [XmlArrayItem(ElementName = "VehicleID")]
        public List<string> BlacklistedVehicles;

        public void LoadDefaults()
        {
            RemoveVehicleOnGet = true;
            BlacklistedVehicles = new List<string>()
            {
                "140"
            };
        }

    }
}
