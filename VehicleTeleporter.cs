using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using Logger = Rocket.Core.Logging.Logger;

namespace VehicleTeleporter
{
    public class VehicleTeleporter : RocketPlugin<ConfigurationVehicleTeleporter>
    {
        public static VehicleTeleporter Instance;

        protected override void Load()
        {
            base.Load();
            Instance = this;
            Logger.Log("Loading VehicleTeleporter, made by Mr.Kwabs.", ConsoleColor.Yellow);
            Logger.Log("Successfully loaded VehicleTeleporter, made by Mr.Kwabs.", ConsoleColor.Yellow);
        }

        protected override void Unload()
        {
            Logger.Log("Unloading VehicleTeleporter, made by Mr.Kwabs.", ConsoleColor.Red);
            Instance = null;
            base.Unload();
        }

        public int HasVehicles(CSteamID uID)
        {
            UnturnedPlayer.FromCSteamID(uID).GetComponent<UtilPlayer>().vehicleList.Clear();

            int vehicleCount = 0;
            if (VehicleManager.vehicles.Count > 0)
            {               
                foreach (InteractableVehicle Vehicle in VehicleManager.vehicles)
                {
                    if (Vehicle.isLocked && Vehicle.lockedOwner == uID)
                    {
                        UnturnedPlayer.FromCSteamID(uID).GetComponent<UtilPlayer>().vehicleList.Add(Vehicle);
                        vehicleCount++;
                    }
                }       
            }
            return vehicleCount;
        }
        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList(){
                    {"vehicle_list_line","[{0}/{1}] {2} {3} ({4},{5},{6})"},
                    {"no_locked_vehicles","You do not have any locked vehicles!"},
                    {"select_vehicle","Please select a number between 1 and {0}."},
                    {"get_vehicle","You have teleported your {0} to you!"},
                    {"tp_vehicle","You have teleported to your {0}!"}
                };
            }
        }
    }
}
