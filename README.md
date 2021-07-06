# AppStore

API de processamento de pagamentos assíncrono.

Para este projeto foi utlizado .NET Core 5, conceitos de Clean Code, Clean Architecture, SOLID e DDD.

1. AppStore.Api: Reponsável pela criação de usuário, cadastro de aplicativo (necessário para teste), consulta de aplicativos disponíveis e solicitação de compra de um aplicativo, além claro, de inserir as ordens de pagamentos em fila.
2. AppStore.Consumer: Responsável pelo processamento das ordens de pagamentos, alterando apenas o status da compra.

* Para o processamento de logs; Utilizado junto ao Microsoft.Extensions.Logging o provider Elmah.Io (Provedor externo de gestão de logs, faz parte dos provedores indicados pela Microsoft).

https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0.

Para que possa ser visualizado os logs faz necessário o login https://app.elmah.io/login/

Usuário: sandredossantos.elmahio@gmail.com

Senha: Sandre!321


* O banco de dados para persistência utilizado foi o MongoDB Atlas.

Para que possa ser visualizado as collections faz necessário o login https://account.mongodb.com/account/login?signedOut=true

Usuário: sandredossantos@hotmail.com

Senha: Sandre!321


* O Message Broker utilizado foi o RabbitMQ.

* A documentação foi criada utilizando o Swagger.

* Para o versionamento utilizado gitflow com o básico de conventional commits