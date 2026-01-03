using API.Entities.Enums;
using API.Infra;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Entities
{
    public class News : BaseEntity
    {
        public News(string hat, string title, string text, string author, string img, Status status)
        {
            Hat = hat;
            Title = title;
            Text = text;
            Author = author;
            Img = img;
            PublishDate = DateTime.UtcNow;
            Slug = Helper.GenerateSlug(Title);
            Status = status;

            ValidateEntity();
        }

        [BsonElement("hat")]
        public string Hat { get; private set; } = string.Empty;

        [BsonElement("title")]
        public string Title { get; private set; } = string.Empty;

        [BsonElement("text")]
        public string Text { get; private set; } = string.Empty;

        [BsonElement("author")]
        public string Author { get; private set; } = string.Empty;

        [BsonElement("img")]
        public string Img { get; private set; } = string.Empty;


        public Status ChangeStatus(Status status)
        {
            Status newStatus = Status.Active;

            switch(status)
            {
                case Status.Active:
                    newStatus = Status.Active;
                    break;
                case Status.Inactive:
                    newStatus = Status.Inactive;
                    break;
                case Status.Draft:
                    newStatus = Status.Draft;
                    break;
            }

            return newStatus;
        }

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "o título não pode estar vazio!");
            AssertionConcern.AssertArgumentNotEmpty(Hat, "o chapéu não pode estar vazio!");
            AssertionConcern.AssertArgumentNotEmpty(Text, "o texto não pode estar vazio!");

            AssertionConcern.AssertArgumentLength(Title, 90, "O título deve ter até 90 caracteres!");
            AssertionConcern.AssertArgumentLength(Hat, 40, "O chapéu deve ter até 40 caracteres!");
        }
    }
}
