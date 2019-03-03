using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;

namespace VehicleTeleporter
{
    public class UtilPlayer : UnturnedPlayerComponent
    {

        public List<InteractableVehicle> vehicleList = new List<InteractableVehicle>();

        public UtilPlayer()
        {
        }

    }
}
