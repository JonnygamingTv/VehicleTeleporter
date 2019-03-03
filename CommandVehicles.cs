using System;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using UnityEngine;
using SDG.Unturned;
using VehicleTeleporter;

namespace KillDisplay
{
    public class CommandVehicles : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "vehicles";

        public string Help => "Lists your locked vehicles.";

        public string Syntax => "/vehicles";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "vehicleteleporter.vehicles" };


        public void Execute(IRocketPlayer rCaller, string[] Command)
        {
            UnturnedPlayer uPlayer = (UnturnedPlayer)rCaller;

            if (Command.Length > 0) { UnturnedChat.Say(uPlayer, Syntax, Color.red); return; }

            UtilPlayer utilPlayer = uPlayer.GetComponent<UtilPlayer>();

            if (VehicleTeleporter.VehicleTeleporter.Instance.HasVehicles(uPlayer.CSteamID) > 0)
            {
                int vehicleCount = 0;
                foreach (InteractableVehicle Vehicle in utilPlayer.vehicleList)
                {
                    vehicleCount++;
                    UnturnedChat.Say(uPlayer, $"[{vehicleCount}/{utilPlayer.vehicleList.Count}] {Vehicle.asset.vehicleName}", Color.yellow);
                }
                return;
            }
            else
            {
                UnturnedChat.Say(uPlayer, "You do not have any locked vehicles!", Color.red);
                return;
            }

        }

    }
}
