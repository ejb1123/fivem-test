using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class train : BaseScript
    {
        Vehicle trainn;
        public train()
        {
            Tick += Train_Tick;
        }

        private async Task Train_Tick()
        {
            if (Game.IsControlJustPressed(0, Control.Cover))
            {
                for (int x = 0; x < 10; x++)
                {
                    if (LocalPlayer.Character.CurrentVehicle.Doors.HasDoor((VehicleDoorIndex)x))
                    {
                        LocalPlayer.Character.CurrentVehicle.Doors[(VehicleDoorIndex)x].Close();
                        LocalPlayer.Character.CurrentVehicle.LockStatus = VehicleLockStatus.CanBeBrokenIntoPersist;
                    }
                }
                //Debug.WriteLine($"{trainn.Doors.GetAll()}");
            }
            if (Game.IsControlJustPressed(0, Control.MultiplayerInfo))
            {
                VehicleHash[] h = { VehicleHash.Freight, VehicleHash.FreightCar, VehicleHash.FreightGrain, VehicleHash.FreightCont1, VehicleHash.TankerCar, VehicleHash.FreightCont2, VehicleHash.FreightTrailer, (VehicleHash)Game.GenerateHash("metrotrain") };
                foreach (var t in h)
                {
                    await new Model(t).Request(-1);
                }
                trainn = new Vehicle(Function.Call<int>(Hash.CREATE_MISSION_TRAIN, 24, -498.4123f, 4304.3f, 88.40305f, true));
                if (trainn != null) LocalPlayer.Character.SetIntoVehicle(trainn, VehicleSeat.LeftRear);
            }
        }
    }
}
