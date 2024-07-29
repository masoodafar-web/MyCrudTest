
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CrudTestMicroservice.WebApi/CrudTestMicroservice.WebApi.csproj", "CrudTestMicroservice.WebApi/"]
COPY ["CrudTestMicroservice.Application/CrudTestMicroservice.Application.csproj", "CrudTestMicroservice.Application/"]
COPY ["CrudTestMicroservice.Domain/CrudTestMicroservice.Domain.csproj", "CrudTestMicroservice.Domain/"]
COPY ["CrudTestMicroservice.Infrastructure/CrudTestMicroservice.Infrastructure.csproj", "CrudTestMicroservice.Infrastructure/"]
COPY ["CrudTestMicroservice.Protobuf/CrudTestMicroservice.Protobuf.csproj", "CrudTestMicroservice.Protobuf/"]
RUN dotnet restore "CrudTestMicroservice.WebApi/CrudTestMicroservice.WebApi.csproj"
COPY . .
WORKDIR "/src/CrudTestMicroservice.WebApi"
RUN dotnet build "CrudTestMicroservice.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrudTestMicroservice.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrudTestMicroservice.WebApi.dll"]
