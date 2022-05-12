using UnityModManagerNet;

namespace CompanionAscension.Utilities
{
    public class Settings : UnityModManager.ModSettings
    {
        public bool useCompanionAscension = true;

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
    }
}
