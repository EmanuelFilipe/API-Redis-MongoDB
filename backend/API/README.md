# baixando a imagem do mongoDB. Com o docker instalado, execute o comando:
docker pull mongo:7

# Com a imagem do docker no seu host, vamos criar um container de servidor de banco de dados. Para isso, você pode escolher uma das duas instruções abaixo:
docker run -d `
  --name mongodb `
  -p 27017:27017 `
  -v mongodb_data:/data/db `
  -e MONGO_INITDB_ROOT_USERNAME=root `
  -e MONGO_INITDB_ROOT_PASSWORD=mypass `
  mongo:7

# Criação de servidor especificando uma senha
docker run -d -p 27017:27017 -p 28017:28017 -e MONGODB_PASS="mypass" tutum/mongodb

# O próximo passo será subir o seu servidor mongo. Para isso, execute os passos abaixo:
docker ps -a

# parando imagem 
docker stop mongodb

## -----------------------------------------------------------------------------------
## Bibliotecas utilizadas

- **MongoDB.Bson 2.14.1**  
  Fornece classes e métodos para trabalhar com documentos BSON (Binary JSON), que é o formato nativo de armazenamento do MongoDB. Inclui suporte para serialização, tipos específicos (ObjectId, DateTime, etc.) e manipulação de dados no formato BSON.

- **MongoDB.Driver 2.13.2**  
  É o driver oficial do MongoDB para .NET. Permite conectar à base de dados, executar operações de CRUD (Create, Read, Update, Delete), consultas, agregações e gerenciar coleções e bancos diretamente a partir do código C#.
- Com Mongo não é necessário utilização de entityframework para gerar migrations.

## -----------------------------------------------------------------------------------
# Helth Checks for MongoDB - versao de acordo com o seu MongoDB instalado
AspNetCore.HealthChecks.MongoDb by nugget

AspNetCore.HealthChecks.UI
AspNetCore.HealthChecks.UI.Client
AspNetCore.HealthChecks.UI.InMemory.Storage 'grava todas as validações em memória'

## -----------------------------------------------------------------------------------

# Redis
Microsoft.Extensions.Caching.Redis by nugget

docker run --name rediscurso -p 6379:6379 -d redis redis-server --appendonly no

docker stop redis
docker start redis

## acessando o redis no docker para ver as chaves armazenadas
docker exec -it (container_ID - use o docker ps para pegar) /bin/bash
redis-cli
keys * (ao dar enter todas as chaves armazenadas serao listadas)

## -----------------------------------------------------------------------------------
# MONGO commands
var connectionString = "mongodb://root:mypass@localhost:27017/?authSource=admin";

var client = new MongoClient(connectionString);
var database = client.GetDatabase("db_portal");

// GET
Console.WriteLine("GET");
var newsList = database.GetCollection<News>("news").Find(n => !n.Deleted).ToList();

foreach (var item in newsList)
{
    Console.WriteLine($"{item.Title}");
}
Console.WriteLine("");

// GET - Contains
Console.WriteLine("GET - Contains");
var newsListContains = database.GetCollection<News>("news").Find(n => n.Title.Contains("teste")).ToList();

foreach (var item in newsListContains)
{
    Console.WriteLine($"{item.Title}");
}
Console.WriteLine("");

// GET - BUILDER FILTER
Console.WriteLine("GET - BUILDER FILTER");
var authorFilter = Builders<News>.Filter.Eq(n => n.Author, "Da redação");
var deletedFilter = Builders<News>.Filter.Eq(n => n.Deleted, false);
var combinedFilters = Builders<News>.Filter.And(authorFilter, deletedFilter);

var newsListCombinedFilters = database.GetCollection<News>("news").Find(combinedFilters).ToList();

foreach (var item in newsListCombinedFilters)
{
    Console.WriteLine($"{item.Title}");
}
Console.WriteLine("");

// GET - REGEX
Console.WriteLine("GET - REGEX");
//var filter = Builders<News>.Filter.Regex(n => n.Author, new BsonRegularExpression(".*redação*"));

//// traz os resultados que tiverem em maiusculo ou minusculo
var filter = Builders<News>.Filter.Regex(n => n.Author, new BsonRegularExpression("redação", "i"));
var newsListRegex = database.GetCollection<News>("news").Find(filter).ToList();
var newsListRegexProjection = database.GetCollection<News>("news").Find(filter).Project(n => new { n.Id, n.Slug }).ToList();

foreach (var item in newsListCombinedFilters)
{
    Console.WriteLine($"{item.Title}");
}
Console.WriteLine("");

//// GET - WITH DATES
//Console.WriteLine("GET - WITH DATES");
//var filterReg = Builders<News>.Filter.Regex(n => n.Author, new BsonRegularExpression("redação", "i"));

//var fromDate = DateTimeOffset.Parse("2026-01-02T18:39:37.604+00:00");
//var toDate = DateTimeOffset.Parse("2026-01-02T18:43:04.694+00:00");


////var fromDate = DateTime.SpecifyKind(DateTime.Parse("2026-01-02T18:39:37.604"), DateTimeKind.Utc);
////var toDate = DateTime.SpecifyKind(
////    DateTime.Parse("2026-01-02T18:43:04.694"),
////    DateTimeKind.Utc
////);

//// gt = maior que
//// gte = maior que ou igual
//// lt = menor que
//// lte = menor que ou igual

////var filterDates = new BsonDocument
////{
////    { "publishDate", new BsonDocument
////    {
////        { "$gte", fromDate },
////        { "$lte", toDate }
////    }}
////};

//var filterDates =
//    Builders<News>.Filter.Gte(n => n.PublishDate, fromDate) &
//    Builders<News>.Filter.Lte(n => n.PublishDate, toDate);

//var combinedFilter = filterReg & filterDates;

//var result = database
//    .GetCollection<News>("news")
//    .Find(combinedFilter)
//    .ToList();

//foreach (var item in result)
//{
//    Console.WriteLine($"{item.Title}");
//}
//Console.WriteLine("");