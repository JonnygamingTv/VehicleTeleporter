# VehicleTeleporter
Allows users to view, teleport to and retrieve their locked vehicles.

## Configuration:
- `RemoveVehicleOnGet` - When true, removes the old locked vehicle when teleporting it to you. Default value is true.
- `BlacklistedVehicles` - A list of all the vehicle IDs you don't want users to be able to view, teleport & retrieve. Default value is 140.

## Commands:
- `/Vehicles` - Lists all your locked vehicles.
- `/VehicleTP [Vehicle #]` - Teleports you to one of your locked vehicles.
- `/VTP [Vehicle #]` - Alias to `/VehicleTP`.
- `/VehicleGet [Vehicle #]` - Gives you one of your locked vehicles. (Vehicle information included!)
- `/VGet [Vehicle #]` - Alias tot `/VehicleGet`

## Permissions:
- `vehicleteleporter.vehicles` - Users with this permission can execute the `/Vehicles` command.
- `vehicleteleporter.vehicletp` - Users with this permission can execute the `/VehicleTP` command.
- `vehicleteleporter.vehicleget` - Users with this permission can execute the `/VehicleGet` command.
- `vehicleteleporter.blacklist.ignore` - Users with this permission can view, teleport & retrieve blacklisted vehicles. (Set in configuration)

## Demonstration:
- https://www.youtube.com/watch?v=77-NDa9ObDc

## Update Video (5th March 2019)
- https://www.youtube.com/watch?v=2kkOXo9gQ68


#### Thanks to `!? Blue#0001` for the idea!
