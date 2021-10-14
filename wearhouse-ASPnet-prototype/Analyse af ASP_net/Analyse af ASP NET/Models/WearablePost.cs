using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analyse_af_ASP_NET.Models
{
    public class WearablePost
    {
        //Wearable
        public DateTime date { get; set; }
        public WearablePostState state { get; set; }
        public double? price { get; set; }
        WearablePost()
        {

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
