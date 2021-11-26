using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WearHouse_WebApp.Models.Entities
{
    public class dbComments
    {
        [Key]
        public int CommentId { get; set; }
        public string Comments { get; set; }
        public DateTime Moment { get; set; }

        public string userId { get; set; }
        public int WearableId { get; set; }
    }
}
