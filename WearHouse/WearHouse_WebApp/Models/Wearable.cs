using System;

namespace WearHouse_WebApp.Models
{

    public class Wearable
    {
        public uint size { get; private set; }
        public string wearableImagePath { get; private set; }
        public string brand { get; private set; }
        public string description { get; private set; }
        public string type { get; set; } = default;
        public Gender gender { get; private set; }

        public Wearable() { }

        public Wearable(uint size, string wearableImagePath, string brand, string description, Gender gender)
        {
            this.size = size;
            this.wearableImagePath = wearableImagePath;
            this.brand = brand;
            this.description = description;
            this.gender = gender;
        }
    }

    public enum Gender
    {
        Mens,
        Womens,
        Unisex
    }
}
