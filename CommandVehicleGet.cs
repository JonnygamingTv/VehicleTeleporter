using System;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using UnityEngine;
using SDG.Unturned;
using System.Text.RegularExpressions;

namespace VehicleTeleporter
{
    public class CommandVehicleGet : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "vehicleget";

        public string Help => "Retrieves one of your locked vehicles.";

        public string Syntax => "/vehicleget";

        public List<string> Aliases => new List<string>() { "vget" };

        public List<string> Permissions => new List<string>() { "vehicleteleporter.vehicleget" };


        public void Execute(IRocketPlayer rCaller, string[] Command)
        {
            UnturnedPlayer uPlayer = (UnturnedPlayer)rCaller;

            if (Command.Length == 0 || Command.Length > 1) { UnturnedChat.Say(uPlayer, Syntax, Color.red); return; }

            UtilPlayer utilPlayer = uPlayer.GetComponent<UtilPlayer>();
            if (VehicleTeleporter.Instance.HasVehicles(uPlayer.CSteamID) > 0)
            {
                if (Regex.IsMatch(Command[0], @"^\d+$"))
                {
                    List<InteractableVehicle> playerVehicles = utilPlayer.vehicleList;

                    if (int.Parse(Command[0]) <= playerVehicles.Count)
                    {
                        InteractableVehicle desiredVehicle = playerVehicles[int.Parse(Command[0]) - 1];
                        uPlayer.GiveVehicle(desiredVehicle.id);
                        InteractableVehicle newVehicle = VehicleManager.vehicles[VehicleManager.vehicles.Count - 1];
                        RetrieveVehicle(desiredVehicle, newVehicle, uPlayer);
                        UnturnedChat.Say(uPlayer, $"You have teleported your {desiredVehicle.asset.vehicleName} to you!", Color.yellow);
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
            }
            else
            {
                UnturnedChat.Say(uPlayer, "You do not have any locked vehicles!", Color.red);
            }
        }

        private void RetrieveVehicle(InteractableVehicle desiredVehicle, InteractableVehicle newVehicle, UnturnedPlayer uPlayer)
        {
            uint instanceID = newVehicle.instanceID;

            VehicleManager.instance.channel.send("tellVehicleHealth", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[2]
            {
                (object) instanceID,
                (object) desiredVehicle.health
            });

            VehicleManager.instance.channel.send("tellVehicleFuel", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[2]
            {
                (object) instanceID,
                (object) desiredVehicle.fuel
            });

            VehicleManager.instance.channel.send("tellVehicleBatteryCharge", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[2]
            {
                (object) instanceID,
                (object) desiredVehicle.batteryCharge
            });

            VehicleManager.instance.channel.send("tellVehicleLock", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[4]
            {
                (object) instanceID,
                (object) uPlayer.CSteamID,
                (object) uPlayer.SteamGroupID,
                (object) true
            });

            for (int i = 0; i < newVehicle.tires.Length; i++)
            {
                if (!desiredVehicle.tires[i].isAlive)
                {
                    newVehicle.tires[i].isAlive = false;
                    newVehicle.sendTireAliveMaskUpdate();
                }
            }

            if (desiredVehicle.headlightsOn)
            {
                VehicleManager.instance.channel.send("tellVehicleHeadlights", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[2]
                {
                    (object) instanceID,
                    (object) desiredVehicle.headlightsOn
                });
            }

            if (desiredVehicle.sirensOn)
            {
                VehicleManager.instance.channel.send("tellVehicleSirens", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[2]
                {
                    (object) instanceID,
                    (object) desiredVehicle.sirensOn
                });
            }

            for (int i = 0; i < desiredVehicle.turrets.Length; i++)
            {
                newVehicle.turrets[i].state[10] = (byte)desiredVehicle.turrets[i].state[10];
            }

            if (VehicleTeleporter.Instance.Configuration.Instance.RemoveVehicleOnGet)
            {
                VehicleManager.askVehicleDestroy(desiredVehicle);
                VehicleManager.instance.channel.send("tellVehicleDestroy", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, desiredVehicle.instanceID);
            }

            return;
        }
    }
}
