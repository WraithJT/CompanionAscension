using UnityModManagerNet;

namespace CompanionAscension.Utilities
{
    public class Settings : UnityModManager.ModSettings
    {
        public bool useCompanionAscension = true;
        public bool useCompanionSecondAscension = true;
        public bool useBasicAscensionsOnly = false;
        public bool useNoPathAscensions = false;

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
    }
}
