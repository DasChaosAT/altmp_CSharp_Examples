using System;
using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Events;

namespace CSharp_Examples
{
    public class Events : IResource
    {
        public void OnStart()
        {
            Alt.OnCheckpoint += Alt_OnCheckpoint;
            Alt.OnEntityRemove += Alt_OnEntityRemove;
            Alt.OnPlayerConnect += Alt_OnPlayerConnect;
            Alt.OnPlayerDamage += Alt_OnPlayerDamage;
            Alt.OnPlayerDead += Alt_OnPlayerDead;
            Alt.OnPlayerDisconnect += Alt_OnPlayerDisconnect;
            Alt.OnVehicleChangeSeat += Alt_OnVehicleChangeSeat;
            Alt.OnVehicleEnter += Alt_OnVehicleEnter;
            Alt.OnVehicleLeave += Alt_OnVehicleLeave;

            Alt.Server.LogInfo("Resource \"CSharp_Examples\" has been started.");
        }

        public void OnStop()
        {
            Alt.Server.LogInfo("Resource \"CSharp_Examples\" has been stopped.");
        }

        public IPlayerFactory GetPlayerFactory()
        {
            throw new NotImplementedException();
        }

        public IVehicleFactory GetVehicleFactory()
        {
            throw new NotImplementedException();
        }

        public IBlipFactory GetBlipFactory()
        {
            throw new NotImplementedException();
        }

        public ICheckpointFactory GetCheckpointFactory()
        {
            throw new NotImplementedException();
        }

        private void Alt_OnCheckpoint(ICheckpoint checkpoint, IEntity entity, bool state)
        {
            if (!checkpoint.Exists || !entity.Exists)
                return;

            if (state)
            {
                //Executed if entity enters checkpoint
                switch (entity.Type)
                {
                    case EntityType.Player:
                        Alt.Server.LogDebug("A player has entered the checkpoint: player-position " + entity.Position + " - checkpoint-position " + checkpoint.Position);
                        break;
                    case EntityType.Vehicle:
                        Alt.Server.LogDebug("A vehicle has entered the checkpoint: vehicle-position " + entity.Position + " - checkpoint-position " + checkpoint.Position);
                        break;
                    default:
                        Alt.Server.LogDebug("A entity has entered the checkpoint: entity-position " + entity.Position + " - checkpoint-position " + checkpoint.Position);
                        break;
                }
            }
            else
            {
                //Executed if entity leaves checkpoint
                switch (entity.Type)
                {
                    case EntityType.Player:
                        Alt.Server.LogDebug("A player has left the checkpoint: player-position " + entity.Position + " - checkpoint-position " + checkpoint.Position);
                        break;
                    case EntityType.Vehicle:
                        Alt.Server.LogDebug("A vehicle has left the checkpoint: vehicle-position " + entity.Position + " - checkpoint-position " + checkpoint.Position);
                        break;
                    default:
                        Alt.Server.LogDebug("A entity has left the checkpoint: entity-position " + entity.Position + " - checkpoint-position " + checkpoint.Position);
                        break;
                }
            }
        }

        private void Alt_OnEntityRemove(IEntity entity)
        {
            switch (entity.Type)
            {
                case EntityType.Player:
                    Alt.Server.LogDebug("A player has been removed, at position " + entity.Position + ".");
                    break;
                case EntityType.Vehicle:
                    Alt.Server.LogDebug("A vehicle has been removed, at position " + entity.Position + ".");
                    break;
                case EntityType.Blip:
                    Alt.Server.LogDebug("A blip has been removed, at position " + entity.Position + ".");
                    break;
                case EntityType.Checkpoint:
                    Alt.Server.LogDebug("A checkpoint has been removed, at position " + entity.Position + ".");
                    break;
                default:
                    Alt.Server.LogDebug("A entity has been removed, at position " + entity.Position + ".");
                    break;
            }
        }


        private void Alt_OnPlayerConnect(IPlayer player, string reason)
        {
            if (!player.Exists)
                return;

            Alt.Server.LogInfo(player.Name + "has joined the server. (" + reason + ")");
        }

        private void Alt_OnPlayerDamage(IPlayer player, IEntity attacker, uint weapon, byte damage)
        {
            if (!player.Exists || !attacker.Exists)
                return;

            switch (attacker.Type)
            {
                case EntityType.Player:
                    var playerAttacker = (IPlayer) attacker;
                    Alt.Server.LogDebug(player.Name + " has been damaged by " + playerAttacker + " with weapon " + weapon + " and suffered " + damage + " HP.");
                    break;
                case EntityType.Vehicle:
                    var vehicleAttacker = (IVehicle)attacker;
                    Alt.Server.LogDebug(player.Name + " has been damaged by a vehicle (Driver: " + vehicleAttacker.Driver + ") and suffered " + damage + " HP.");
                    break;
                default:
                    Alt.Server.LogDebug(player.Name + " has been damaged by an Entity and suffered " + damage + ".");
                    break;
            }
        }

        private void Alt_OnPlayerDead(IPlayer player, IEntity killer, uint weapon)
        {
            if (!player.Exists || !killer.Exists)
                return;

            switch (killer.Type)
            {
                case EntityType.Player:
                    var playerKiller = (IPlayer)killer;
                    Alt.Server.LogDebug(player.Name + " has been killed by " + playerKiller + " with weapon " + weapon + ".");
                    break;
                case EntityType.Vehicle:
                    var vehicleAttacker = (IVehicle)killer;
                    Alt.Server.LogDebug(player.Name + " has been killed by a vehicle (Driver: " + vehicleAttacker.Driver + ").");
                    break;
                default:
                    Alt.Server.LogDebug(player.Name + " has been killed by an Entity.");
                    break;
            }
        }

        private void Alt_OnPlayerDisconnect(IPlayer player, string reason)
        {
            if (!player.Exists)
                return;

            Alt.Server.LogInfo(player.Name + "has left the server. (" + reason + ")");
        }

        private void Alt_OnVehicleChangeSeat(IVehicle vehicle, IPlayer player, sbyte oldSeat, sbyte newSeat)
        {
            if (!vehicle.Exists || !player.Exists)
                return;

            Alt.Server.LogDebug(player.Name + " has changed seat in vehicle (Driver: " + vehicle.Driver.Name + ") from " + oldSeat + " to " + newSeat + ".");
        }
        
        private void Alt_OnVehicleEnter(IVehicle vehicle, IPlayer player, sbyte seat)
        {
            if (!vehicle.Exists || !player.Exists)
                return;

            Alt.Server.LogDebug(player.Name + " entered vehicle (Driver: " + vehicle.Driver.Name + ") on seat " + seat +  ".");
        }

        private void Alt_OnVehicleLeave(IVehicle vehicle, IPlayer player, sbyte seat)
        {
            if (!vehicle.Exists || !player.Exists)
                return;

            Alt.Server.LogDebug(player.Name + " left vehicle (Driver: " + vehicle.Driver.Name + ") from seat " + seat + ".");
        }
    }
}
