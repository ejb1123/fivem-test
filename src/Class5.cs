using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace test
{
    class Class5 : BaseScript
    {
        public Class5()
        {
            this.Tick += Class5_Tick;
        }
        bool state;
        int soundid = Function.Call<int>(Hash.GET_SOUND_ID);
        private async Task Class5_Tick()
        {

            if (Game.Player.Character.IsInVehicle())
            {
                if (Game.IsControlJustReleased(0, Control.Cover))
                {
                    int netid = Function.Call<int>(Hash.GET_NETWORK_ID_FROM_SOUND_ID, soundid);
                    Function.Call(Hash.SET_NETWORK_ID_EXISTS_ON_ALL_MACHINES, netid);
                }
                if (Game.IsControlJustPressed(0, Control.MultiplayerInfo))
                {
                    if (Function.Call<bool>(Hash.HAS_SOUND_FINISHED,soundid))
                    {
                        Function.Call(Hash.PLAY_SOUND_FROM_ENTITY, soundid, "VEHICLES_HORNS_SIREN_1", Game.Player.Character.CurrentVehicle.Handle, 0, true, 0);
                    }
                    else
                    {
                        Audio.StopSound(soundid);
                    }
                }

            }
        }
    }
}
