using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WearHouse_WebApp.Models.Domain
{
    public class CommentModel
    {
        public string Comment { get; }
        public DateTime Moment { get; }
        public UserModel Author { get; }


        public CommentModel(Entities.dbComments databaseCommentString)
        {
            Comment = databaseCommentString.Comments;
            Moment = databaseCommentString.Moment.ToString();

            if (databaseCommentString.Author != null)
                Author = databaseCommentString.Author.ConvertToUserModelWithoutWearables();
        }
        public CommentModel(string comment, DateTime moment, UserModel author)
        {
            Comment = comment;
            Moment = moment;
            Author = author;
        }
        public Entities.dbComments ConvertToDbModel(int wearableId)
        {
            if (Author != null)
            {
                return new Entities.dbComments
                {
                    Comments = this.Comment,
                    Moment = this.Moment,
                    userId = this.Author.UserId,
                    WearableId = wearableId,
                };
            }
            else
                throw new Exception("No author for comment");
        }
    }
}