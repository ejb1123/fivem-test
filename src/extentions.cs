using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace test
{
    public static class extentions
    {
        public static Int64 GetNetworkId(this Entity entity)
        {
            return Function.Call<Int64>(Hash.VEH_TO_NET, entity.Handle);
        }

        public static void RegisterAsNetworked(this Entity entity)
        {
            Function.Call((Hash)0x06FAACD625D80CAA,entity.Handle);
        }

        public static void SetExistOnAllMachines(this Entity entity,bool b)
        {
            Function.Call(Hash.SET_NETWORK_ID_EXISTS_ON_ALL_MACHINES,entity.GetNetworkId(),b);
        }

        public static bool HaveControlOfNetworkID(int NetworkID)
        {
            return Function.Call<bool>(Hash.NETWORK_HAS_CONTROL_OF_NETWORK_ID, NetworkID);
        }
    }
}