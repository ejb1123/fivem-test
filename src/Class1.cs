using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace test
{
    public class Class1 : BaseScript
    {
        public Class1()
        {
            this.EventHandlers["kkk"] += new Action<int,int>(Target);
            this.Tick += OnTick;
            //Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, Game.GenerateHash("stats_controller"));
        }

        private void Target(int netId, int playerID)
        {
            Debug.WriteLine($"yea {this.Players[playerID].Character.CurrentVehicle.GetNetworkId()}");
            if (playerID == LocalPlayer.ServerId) return;
            Debug.WriteLine("target hit");
            Doteleoprt(netId.ToString());
        }

        private async Task OnTick()
        {
            if (Game.IsControlJustPressed(0,Control.Cover) && LocalPlayer.Character.IsInVehicle())
            {
                Game.DisableControlThisFrame(0, Control.VehicleRadioWheel);
                //RequestWarp(Game.PlayerPed.CurrentVehicle);
               var netidd =  LocalPlayer.Character.CurrentVehicle.GetNetworkId();
                Screen.ShowNotification($"Tell the other user to use the \"Z\" key and to enter \"{netidd}\"");
            }
            if (Game.IsControlJustPressed(0, Control.SelectWeaponUnarmed))
            {
                Game.DisableControlThisFrame(0,Control.SelectWeaponUnarmed);
                SpawnVehicle(false,false);
            }
            if (Game.IsControlJustPressed(0, Control.SelectWeaponMelee))
            {
                Game.DisableControlThisFrame(0, Control.SelectWeaponMelee);
                SpawnVehicle(false, true);
            }
            if (Game.IsControlJustPressed(0, Control.SelectWeaponShotgun))
            {
                Game.DisableControlThisFrame(0, Control.SelectWeaponShotgun);
                SpawnVehicle(true, false);
            }
            if (Game.IsControlJustPressed(0, Control.SelectWeaponHeavy))
            {
                Game.DisableControlThisFrame(0, Control.SelectWeaponHeavy);
                SpawnVehicle(true, true);
            }
        }

        private async void SpawnVehicle(bool one,bool two)
        {
            Screen.ShowNotification($"Running {one},{two} test.");
            if (LocalPlayer.Character.IsInVehicle())
            {
                await CitizenFX.Core.BaseScript.Delay(10000);
                RequestWarp(Game.PlayerPed.CurrentVehicle);
                var netidd = Function.Call<int>(Hash.NETWORK_GET_NETWORK_ID_FROM_ENTITY, LocalPlayer.Character.CurrentVehicle.Handle);
                Screen.ShowNotification($"Tell the other user to use the \"Z\" key and to enter \"{netidd}\"");
                return;
            }
            var k = new Model(VehicleHash.Police2);
            await k.Request(-1);
            //var veh = await World.CreateVehicle(VehicleHash.Police2, LocalPlayer.Character.Position);
            var veh = new Vehicle(Function.Call<int>(Hash.CREATE_VEHICLE,
                VehicleHash.Police2,
                LocalPlayer.Character.Position.X,
                LocalPlayer.Character.Position.Y,
                LocalPlayer.Character.Position.Z,
                LocalPlayer.Character.Heading,
                one,
                two));
            //veh.MarkAsNoLongerNeeded();
            k.MarkAsNoLongerNeeded();

            LocalPlayer.Character.SetIntoVehicle(veh, VehicleSeat.Driver);

            veh.RegisterAsNetworked();
            Screen.ShowNotification($"network status {Function.Call<bool>(Hash.NETWORK_GET_ENTITY_IS_NETWORKED, veh)}");

            veh.SetExistOnAllMachines(true);

            await CitizenFX.Core.BaseScript.Delay(10000);
            var netid = veh.GetNetworkId();
            await CitizenFX.Core.BaseScript.Delay(10000);
            Screen.ShowNotification($"network status {Function.Call<bool>(Hash.NETWORK_GET_ENTITY_IS_NETWORKED, veh)}");
            RequestWarp(veh);
            Screen.ShowNotification($"Tell the other user to use the \"Z\" key and to enter \"{netid}\"");
        }
        private bool Doteleoprt(string str)
        {
            if (int.TryParse(str, out int num))
            {
                //if (!Function.Call<bool>(Hash.NETWORK_DOES_NETWORK_ID_EXIST, num))
                //{
                //    Screen.ShowNotification($"Error validating the existance of NET_ID {num}");
                //    return true;
                //}
                //else
                {
                    if (extentions.HaveControlOfNetworkID(num))
                    {
                        Screen.ShowNotification($"You have control of entity NET_ID {num}");
                    }
                    else
                    {
                        Screen.ShowNotification($"You do not have control of entity NET_ID {num}\n" +
                                                $"Trying to request control");
                        if (Function.Call<bool>(Hash.NETWORK_REQUEST_CONTROL_OF_NETWORK_ID, num))
                        {
                            Screen.ShowNotification($"We think you have control will check");
                            if (extentions.HaveControlOfNetworkID(num))
                            {
                                Screen.ShowNotification($"Yup you have control continueing");
                            }
                            else
                            {
                                Screen.ShowNotification($"Yup you do not control continueing");
                            }
                        }
                        else
                        {
                            Screen.ShowNotification($"Well, shit it failed to get control");
                            //return;
                        }
                    }
                    if (!Function.Call<bool>(Hash.NETWORK_DOES_ENTITY_EXIST_WITH_NETWORK_ID, num))
                    {
                        Screen.ShowNotification($"Error validating the existance of a entity with a NET_ID of {num}");
                        //return;
                    }

                    var veh = new Vehicle(Function.Call<int>(Hash.NETWORK_GET_ENTITY_FROM_NETWORK_ID, num));
                    if (!WarpToVehicle(veh))
                    {
                        Screen.ShowNotification("warp failed");
                    }
                }
            }
            else
            {
                Screen.ShowNotification($"Error pasring \"{str}\" to a interger");
            }
            return false;
        }

        private bool WarpToVehicle(Vehicle veh)
        {
            if (NextFreeSeat(veh) != (VehicleSeat)55)
            {
                LocalPlayer.Character.SetIntoVehicle(veh, NextFreeSeat(veh));
                return true;
            }
            else
            {
                Screen.ShowNotification("No open seats are avalible");
                return false;
            }

        }
        //GAMEPLAY::TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME(GAMEPLAY::GET_HASH_KEY("stats_controller"))
        //natives.STATS.STAT_SET_INT(natives.GAMEPLAY.GET_HA SH_KEY("MP" .. playerPedId[i] .. "_CHAR_ARMOUR_1_COUNT"), 1, true)

        private VehicleSeat NextFreeSeat(Vehicle veh)
        {
            for (int x = -1; x <= 14; x++)
            {
                if (veh.IsSeatFree((VehicleSeat)x))
                {
                    return (VehicleSeat)x;
                }
            }
            return (VehicleSeat)55;

        }

        private void RequestWarp(Vehicle veh)
        {
            TriggerServerEvent("kzz",veh.GetNetworkId(),LocalPlayer.ServerId);
        }
    }
}
