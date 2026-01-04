using API.Entities.Enums;
using API.Infra;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Entities
{
    public class Gallery : BaseEntity
    {
        public Gallery(string title, string legend, string author, string tags, 
                       Status status, IList<string> galleryImages, string thumb)
        {
            Title = title;
            Legend = legend;
            Author = author;
            Tags = tags;
            Thumb = thumb;
            Slug = Helper.GenerateSlug(title);
            Status = status;
            GalleryImages = galleryImages;

            ValidateEntity();
        }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("legend")]
        public string Legend { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("tags")]
        public string Tags { get; set; }

        [BsonElement("thumb")]
        public string Thumb { get; set; }

        [BsonElement("galleryImages")]
        public IList<string> GalleryImages { get; set; }


        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "O título não pode estar vazio!");
            AssertionConcern.AssertArgumentNotEmpty(Legend, "A legenda não pode estar vazia!");

            AssertionConcern.AssertArgumentLength(Title, 90, "O título deve ter até 90 caracteres!");
            AssertionConcern.AssertArgumentLength(Legend, 40, "A legenda deve ter até 40 caracteres!");
        }
    }
}
