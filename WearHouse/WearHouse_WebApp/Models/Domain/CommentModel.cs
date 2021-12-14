using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Models.Domain
{
    public class CommentModel
    {
        public string Comment { get; set; }
        public DateTime Moment { get; set; }
        public UserModel Author { get; set; }
        public int WearableId { get; set; }

        public CommentModel() { }
        public CommentModel(dbComments databaseCommentString)
        {
            Comment = databaseCommentString.Comments;
            Moment = databaseCommentString.Moment;

            if (databaseCommentString.Author != null)
                Author = databaseCommentString.Author.ConvertToUserModelWithoutWearables();
        }
        public dbComments ConvertToDbModel()
        {
            if (Author != null)
            {
                return new Entities.dbComments
                {
                    Comments = this.Comment,
                    Moment = this.Moment,
                    userId = this.Author.UserId,
                    WearableId = this.WearableId,
                };
            }
            else
                throw new Exception("No author for comment");
        }
    }
}