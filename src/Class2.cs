using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Class2 : BaseScript
    {
        private Vehicle veh;
        private Ped driver;

        public Class2()
        {
            this.Tick += Class2_Tick;
        }

        private async Task Class2_Tick()
        {
            if (Game.IsControlJustReleased(0, Control.MultiplayerInfo))
            {
                while(!Function.Call<bool>(Hash.HAS_SCRIPT_LOADED, "gunclub_shop"))
                {
                    Function.Call(Hash.REQUEST_SCRIPT, "gunclub_shop", 170000);
                    await BaseScript.Delay(1);
                }
                
                //Debug.WriteLine(Function.Call<int>(Hash.START_NEW_SCRIPT, Game.GenerateHash("MPPLY_TOTAL_SVC"), 55,false).ToString());
                //var model = new Model(PedHash.Security01SMM);
                //await model.Request(-1);
                //if (!model.IsInCdImage || !model.IsValid)
                //{
                //    throw new Exception($"error loading model s_m_m_security_01");
                //}
                //model.MarkAsNoLongerNeeded();

                //veh = await World.CreateVehicle(VehicleHash.Adder, LocalPlayer.Character.GetOffsetPosition(new Vector3(0, -20, 0)),LocalPlayer.Character.CurrentVehicle.Heading);

                // Function.Call(Hash.CREATE_PED_INSIDE_VEHICLE, veh, 6, Game.GenerateHash("s_m_m_security_01"), -1,
                //        false, true);

                //driver = veh.GetPedOnSeat(VehicleSeat.Driver);//veh.CreateRandomPedOnSeat(VehicleSeat.Driver);
                //Function.Call(Hash.TASK_VEHICLE_ESCORT, driver, veh, LocalPlayer.Character.CurrentVehicle, 0, 60.0f,6, 0.0f, 0, -20);
            }
        }
    }
}
