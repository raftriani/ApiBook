#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ApiBook/ApiBook.csproj", "ApiBook/"]
COPY ["ApiBook.Application/ApiBook.Application.csproj", "ApiBook.Application/"]
COPY ["ApiBook.Domain/ApiBook.Domain.csproj", "ApiBook.Domain/"]
COPY ["ApiBook.Infra/ApiBook.Infra.csproj", "ApiBook.Infra/"]
RUN dotnet restore "ApiBook/ApiBook.csproj"
COPY . .
WORKDIR "/src/ApiBook"
RUN dotnet build "ApiBook.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiBook.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiBook.dll"]