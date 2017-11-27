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
    class Class4 :BaseScript
    {
        Class4()
        {
            this.Tick += Class4_Tick;
        }

        private async Task Class4_Tick()
        {
            if (Game.IsControlJustPressed(0, Control.SelectWeaponUnarmed))
            {
                Game.DisableControlThisFrame(0, Control.SelectWeaponUnarmed);
                SpawnVehicle(false, false);
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

        private async void SpawnVehicle(bool one, bool two)
        {
            Screen.ShowNotification($"Running {one},{two} test.");
            if (LocalPlayer.Character.IsInVehicle())
            {
                await CitizenFX.Core.BaseScript.Delay(10000);
                var netidd = Function.Call<int>(Hash.NETWORK_GET_NETWORK_ID_FROM_ENTITY, LocalPlayer.Character.CurrentVehicle.Handle);
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
            //veh.RegisterAsNetworked();
            var netid = veh.GetNetworkId();
            //veh.SetExistOnAllMachines(true);
            await Delay(10000);
            Screen.ShowNotification($"Tell the other user to use the \"Z\" key and to enter \"{netid}\"");
        }
    }
}
