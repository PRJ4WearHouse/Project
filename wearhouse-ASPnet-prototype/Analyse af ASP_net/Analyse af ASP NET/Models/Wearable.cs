using System;

namespace Analyse_af_ASP_NET.Models
{

    public abstract class Wearable
    {
        public uint size { get; private set; }
        public string wearableImagePath { get; private set; }
        public string brand { get; private set; }
        public string description { get; private set; }
        public string type { get; private set; }

        public Wearable(uint size, string wearableImagePath, string brand, string description, string type)
        {
            this.size = size;
            this.wearableImagePath = wearableImagePath;
            this.brand = brand;
            this.description = description;
            this.type = type;
        }
    }
}
