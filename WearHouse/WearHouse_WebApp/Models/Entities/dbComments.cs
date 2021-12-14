using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WearHouse_WebApp.Models.Entities
{
    public class dbComments //Der er kun 1 kommentar
    {
        [Key]
        public int CommentId { get; set; }
        public string Comments { get; set; } //Der er kun 1 kommentar
        public DateTime Moment { get; set; }

        public string userId { get; set; }
        public int WearableId { get; set; }
        public ApplicationUser Author { get; set; }
        public dbWearable Wearable { get; set; }
        public Domain.CommentModel ConvertToCommentModel()
        {
            return new Domain.CommentModel(this);
        }
    }
}
