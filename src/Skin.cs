using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using CitizenFX.Core.Native;

namespace Skin
{
    public class Skin : BaseScript
    {
        Vector3[] shopLocations = new[] {
            new Vector3(-3172.0f, 1043.16f, 20.86f),
            new Vector3(-1450.89f, -236.81f, 49.81f),
            new Vector3(-1191.7f, -768.22f, 17.32f),
            new Vector3(-820.06f, -1073.47f, 11.33f),
            new Vector3(-710.62f, -153.33f, 37.42f),
            new Vector3(-162.74f, -303.3f, 39.73f),
            new Vector3(424.41f, -808.27f, 29.49f),
            new Vector3(76.65f, -1390.69f, 29.38f),
            new Vector3(-1099.14f, 2711.24f, 19.11f),
            new Vector3(1198.83f, 2709.06f, 38.22f),
            new Vector3(1693.05f, 4820.47f, 42.06f),
            new Vector3(2.15f, 6512.0f, 31.88f)
        };

        public Skin()
        {
            Tick += new Func<Task>(async delegate
            {
                await Task.FromResult(0);

                int i = 0;
                foreach (Vector3 coords in shopLocations)
                {
                    if (World.GetDistance(Game.PlayerPed.Position, coords) < 1.25 && Game.PlayerPed.IsOnFoot)
                    {
                        if (Game.IsControlJustReleased(1, Control.Context))
                        {
                            // Open a menu
                        }
                    }
                    i++;
                }
            });
        }
    }
}