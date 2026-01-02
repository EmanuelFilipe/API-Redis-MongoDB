using API.Entities;

namespace API.Tests.Entities
{
    public class NewsTests
    {
        [Fact]
        public void News_Validate_Title_Lenght()
        {
            // Arrange & Act
            var result = Assert.Throws<DomainException>(() => new News(
                "Entretenimento",
                "'Chef se emocionou ao assistir apresentação da cantora Maria Rita e do Maestro João Carlos Martins'",
                "Erick Jacquin se emocionou ao som da cantora Maria Rita e do maestro João Carlos Martins no MasterChef Brasil desta terça-feira (9). Os artistas se apresentaram ao término da 1ª prova da noite e, nas redes sociais, o público se divertiu com o biquinho feito pelo jurado enquanto ele chorava. O que também rendeu memes na web foi a confusão na praça das sobremesas, ainda no desafio inicial. Eduardo, Daphne e Kelyn não trabalharam em harmonia e o resultado final desagradou os jurados. A tensão na cozinha foi palpável e o clima parecia uma panela de pressão prestes a explodir.",
                "Da Redação",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                API.Entities.Enums.Status.Active
                ));

            // Assert
            Assert.Equal("O título deve ter até 90 caracteres!", result.Message);
        }

        [Fact]
        public void News_Validate_Hat_Lenght()
        {
            // Arrange & Act
            var result = Assert.Throws<DomainException>(() => new News(
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                "Erick Jacquin se emocionou ao som da cantora Maria Rita e do maestro João Carlos Martins no MasterChef Brasil desta terça-feira (9). Os artistas se apresentaram ao término da 1ª prova da noite e, nas redes sociais, o público se divertiu com o biquinho feito pelo jurado enquanto ele chorava. O que também rendeu memes na web foi a confusão na praça das sobremesas, ainda no desafio inicial. Eduardo, Daphne e Kelyn não trabalharam em harmonia e o resultado final desagradou os jurados. A tensão na cozinha foi palpável e o clima parecia uma panela de pressão prestes a explodir.",
                "Da Redação",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                API.Entities.Enums.Status.Active
                ));

            // Assert
            Assert.Equal("O chapéu deve ter até 40 caracteres!", result.Message);
        }

        [Fact]
        public void News_Validate_Title_Empty()
        {
            // Arrange & Act
            var result = Assert.Throws<DomainException>(() => new News(
                "Entretenimento",
                string.Empty,
                "Erick Jacquin se emocionou ao som da cantora Maria Rita e do maestro João Carlos Martins no MasterChef Brasil desta terça-feira (9). Os artistas se apresentaram ao término da 1ª prova da noite e, nas redes sociais, o público se divertiu com o biquinho feito pelo jurado enquanto ele chorava. O que também rendeu memes na web foi a confusão na praça das sobremesas, ainda no desafio inicial. Eduardo, Daphne e Kelyn não trabalharam em harmonia e o resultado final desagradou os jurados. A tensão na cozinha foi palpável e o clima parecia uma panela de pressão prestes a explodir.",
                "Da Redação",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                API.Entities.Enums.Status.Active
                ));

            // Assert
            Assert.Equal("o título não pode estar vazio!", result.Message);
        }

        [Fact]
        public void News_Validate_Hat_Empty()
        {
            // Arrange & Act
            var result = Assert.Throws<DomainException>(() => new News(
                string.Empty,
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                "Erick Jacquin se emocionou ao som da cantora Maria Rita e do maestro João Carlos Martins no MasterChef Brasil desta terça-feira (9). Os artistas se apresentaram ao término da 1ª prova da noite e, nas redes sociais, o público se divertiu com o biquinho feito pelo jurado enquanto ele chorava. O que também rendeu memes na web foi a confusão na praça das sobremesas, ainda no desafio inicial. Eduardo, Daphne e Kelyn não trabalharam em harmonia e o resultado final desagradou os jurados. A tensão na cozinha foi palpável e o clima parecia uma panela de pressão prestes a explodir.",
                "Da Redação",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                API.Entities.Enums.Status.Active
                ));

            // Assert
            Assert.Equal("o chapéu não pode estar vazio!", result.Message);
        }

        [Fact]
        public void News_Validate_Description_Empty()
        {
            // Arrange & Act
            var result = Assert.Throws<DomainException>(() => new News(
                "Entretenimento",
                "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                string.Empty,
                "Da Redação",
                "https://pubimg.band.uol.com.br/files/0e1973594486e928bf38.webp",
                API.Entities.Enums.Status.Active
                ));

            // Assert
            Assert.Equal("o texto não pode estar vazio!", result.Message);
        }
    }
}
