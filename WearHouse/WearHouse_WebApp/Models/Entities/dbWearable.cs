using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.ViewModels;

namespace WearHouse_WebApp.Models.Entities
{
    public class dbWearable
    {
        //Consider changing key to random generated string.
        [Key]
        public int WearableId { get; set; }
        [StringLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrls { get; set; }
        public string UserId { get; set; }
        public string UserContactInfo { get; set; }
        public string State { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public List<dbComments> Comments { get; set; }

        public dbWearable() { }

        public WearableModel ConvertToWearableModelWithoutOwner()
        {
            return new WearableModel(this, false);
        }
        public WearableModel ConvertToWearableModel()
        {
            return new WearableModel(this, true);
        }

        //26-11-2021 Sigurd
        //Måske noget i denne retning?
        public List<CommentModel> ConvertToDomainComments()
        {
            List<CommentModel> domainList = new List<CommentModel>();
            if (Comments != null)
            {
                foreach (dbComments comment in Comments)
                {
                    domainList.Add(comment.ConvertToDomainCommentModel());
                }
            }
            return domainList;
        }
    }
}
