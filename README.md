## ApiBook

Esta é uma aplicação simples para listar livros com uma capa inserida no S3.

## Como rodar esse projeto

Como pré-requisitos, será necessário possuir o [Docker](https://www.docker.com/) instalado em sua máquina.

### Subindo o container com Docker-Compose

Primeiramente, para subir a aplicação, através de um prompt de comando, vá até o diretório na raíz do projeto, onde se encontra o arquivo `docker-compose.yaml` e rode o seguinte comando:

```powershell
docker-compose -f .\docker-compose.yaml up
```

Com isso a aplicação estará funcionando.
