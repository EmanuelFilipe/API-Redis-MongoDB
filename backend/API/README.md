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

## Bibliotecas utilizadas

- **MongoDB.Bson 2.14.1**  
  Fornece classes e métodos para trabalhar com documentos BSON (Binary JSON), que é o formato nativo de armazenamento do MongoDB. Inclui suporte para serialização, tipos específicos (ObjectId, DateTime, etc.) e manipulação de dados no formato BSON.

- **MongoDB.Driver 2.13.2**  
  É o driver oficial do MongoDB para .NET. Permite conectar à base de dados, executar operações de CRUD (Create, Read, Update, Delete), consultas, agregações e gerenciar coleções e bancos diretamente a partir do código C#.
- Com Mongo não é necessário utilização de entityframework para gerar migrations.

# Helth Checks for MongoDB - versao de acordo com o seu MongoDB instalado
AspNetCore.HealthChecks.MongoDb by nugget

AspNetCore.HealthChecks.UI
AspNetCore.HealthChecks.UI.Client
AspNetCore.HealthChecks.UI.InMemory.Storage 'grava todas as validações em memória'