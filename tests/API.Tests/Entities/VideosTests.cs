using API.Entities;

namespace API.Tests.Entities
{
    public class VideosTests
    {
        [Fact]
        public void Video_Validate_Title_Lenght()
        {
            // Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                "Entretenimento",
                "'Chef se emocionou ao assistir apresentação da cantora Maria Rita e do Maestro João Carlos Martins'",
                "Da Redação",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.mp4",
                API.Entities.Enums.Status.Active
                ));

            // Assert
            Assert.Equal("O título deve ter até 90 caracteres!", result.Message);
        }

        [Fact]
        public void Video_Validate_Hat_Lenght()
        {
            // Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",                
                "Da Redação",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.mp4",
                API.Entities.Enums.Status.Active
                ));

            // Assert
            Assert.Equal("O chapéu deve ter até 40 caracteres!", result.Message);
        }

        [Fact]
        public void Video_Validate_Title_Empty()
        {
            // Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                "Entretenimento",
                string.Empty,                
                "Da Redação",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.mp4",
                API.Entities.Enums.Status.Active
                ));

            // Assert
            Assert.Equal("O título não pode estar vazio!", result.Message);
        }

        [Fact]
        public void Video_Validate_Hat_Empty()
        {
            // Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                string.Empty,
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                "Da Redação",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.mp4",
                API.Entities.Enums.Status.Active
                ));

            // Assert
            Assert.Equal("O chapéu não pode estar vazio!", result.Message);
        }
    }
}