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
    class Class3 :BaseScript
    {
        public Class3()
        {

            Tick += Class3_Tick;
        }

        private async Task Class3_Tick()
        {
            if (Game.IsControlPressed(0, Control.MultiplayerInfo))
            {
                //var model = new Model(PedHash.Tonya);
                //await model.Request(-1);
                //new Ped(Function.Call<int>(Hash.CREATE_PED, (InputArgument) 26, (InputArgument) model.Hash,
                //    (InputArgument) LocalPlayer.Character.Position.X, (InputArgument) LocalPlayer.Character.Position.Y,
                //    (InputArgument) LocalPlayer.Character.Position.Z, (InputArgument) LocalPlayer.Character.Heading,
                //    (InputArgument) true, (InputArgument) false));
                CitizenFX.Core.UI.Screen.);
            }
        }
    }
}
