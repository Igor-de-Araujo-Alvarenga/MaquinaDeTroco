# MaquinaDeTroco


# Domain

- Camada de domino da aplicação responsavel por ter as models do projeto.

# Infrastructure 

- Camada responsavel pela manipuladação do dados e depende apenas da Domain.

# MaquinaDeTroco.API

- Responsavel por receber e devolver os dados ou gerar uma mensagem de erro ao cliente, depende da Infrastructure e Domain.

# MaquinaDeTroco.Front

- Responsavel por ser a visualização do cliente e fazar requisições REST para a API, depende da Infrastructure e da Domain
