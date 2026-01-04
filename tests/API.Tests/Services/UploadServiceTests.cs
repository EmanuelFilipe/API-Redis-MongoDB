using API.Entities;
using API.Entities.Enums;
using API.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.Services
{
    public class UploadServiceTests
    {
        [Theory]
        [InlineData(Media.Image, "image.webp")]
        [InlineData(Media.Video, "video.mp4")]
        public void Should_Verify_If_Type_Is_Image_Or_Video(Media media, string fileName)
        {
            // Arrange
            var service = new UploadService();

            // Act
            var result = service.GetTypeMedia(fileName);

            // Assert
            Assert.Equal(media, result);
        }

        [Theory]
        [InlineData(Media.Image, "image.psd")]
        [InlineData(Media.Video, "video.mp3")]
        public void Should_Verify_If_Type_Sended_Is_Wrong_Format(Media media, string fileName)
        {
            // Arrange
            var service = new UploadService();

            // Act
            var act = () => service.GetTypeMedia(fileName);

            // Assert
            act.Should().ThrowExactly<DomainException>().WithMessage("Formato errado de arquivo!");
        }
    }
}
