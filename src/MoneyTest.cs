using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace test
{
    public class MoneyTest :BaseScript
    {
        public MoneyTest()
        {
            Tick += MoneyTest_Tick;
        }

        private System.Threading.Tasks.Task MoneyTest_Tick()
        {
            if (Game.IsControlJustPressed(0, Control.MultiplayerInfo))
            {
                Function.Call(Hash.SET_MULTIPLAYER_HUD_CASH,100,100);
                Function.Call<bool>(Hash.STAT_SET_INT,Game.GenerateHash("BANK_BALANCE"),100.0,true);
            }
            return null;
        }
    }
}