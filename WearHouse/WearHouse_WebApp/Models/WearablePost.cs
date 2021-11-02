using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WearHouse_WebApp.Models
{
    public class WearablePost
    {
        public Wearable wearable { get; set; }
        public DateTime date { get; set; }
        public WearablePostState state { get; set; }
        public double? price { get; set; }
        public WearablePost() { }
        public WearablePost(Wearable wearable, WearablePostState state = WearablePostState.Unavailable, double? price = null)
        {
            this.wearable = wearable;
            this.date = new DateTime();
            this.state = state;
            this.price = price;
        }
        public void ChangeState(WearablePostState newState, double? newPrice=null)
        {
            state = newState;
            price = newPrice;
        }
    }

    public enum WearablePostState
    {
        Unavailable,
        GiveAway,
        Borrow,
        Lease,
        Sale
    }
}
