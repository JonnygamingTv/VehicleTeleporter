using System;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using UnityEngine;
using SDG.Unturned;
using VehicleTeleporter;
using System.Text;
using System.Text.RegularExpressions;
using Logger = Rocket.Core.Logging.Logger;

namespace VehicleTeleporter
{
    class CommandVehicleTP : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "vehicletp";

        public string Help => "Teleports you to one of your saved vehicles.";

        public string Syntax => "/VehicleTP [Vehicle No.]";

        public List<string> Aliases => new List<string>() { "vtp" };

        public List<string> Permissions => new List<string>() { "vehicleteleporter.vehicletp" };


        public void Execute(IRocketPlayer rCaller, string[] Command)
        {
            UnturnedPlayer uPlayer = (UnturnedPlayer)rCaller;

            if (Command.Length > 1 || Command.Length == 0) { UnturnedChat.Say(uPlayer, Syntax, Color.red); return; }

            UtilPlayer utilPlayer = uPlayer.GetComponent<UtilPlayer>();

            if (VehicleTeleporter.VehicleTeleporter.Instance.HasVehicles(uPlayer.CSteamID) > 0)
            {
                if (Regex.IsMatch(Command[0], @"^\d+$"))
                {
                    List<InteractableVehicle> playerVehicles = utilPlayer.vehicleList;

                    if (int.Parse(Command[0]) <= playerVehicles.Count)
                    {
                        InteractableVehicle desiredVehicle = playerVehicles[int.Parse(Command[0]) - 1];
                      
                        uPlayer.Teleport(new Vector3(desiredVehicle.transform.position.x, desiredVehicle.transform.position.y + 3, desiredVehicle.transform.position.z), uPlayer.Rotation);
                        UnturnedChat.Say(uPlayer, $"You have teleported to your {desiredVehicle.asset.vehicleName}!", Color.yellow);
                        playerVehicles.Remove(desiredVehicle);
                    }
                    else
                    {
                        UnturnedChat.Say(uPlayer, $"Please select a number between 1 and {playerVehicles.Count}.", Color.red);
                        return;
                    }
                }
                else
                {
                    UnturnedChat.Say(uPlayer, Syntax, Color.red);
                    return;
                }
            } else
            {
                UnturnedChat.Say(uPlayer, "You do not have any locked vehicles!", Color.red);
            }
        }
    }
}
