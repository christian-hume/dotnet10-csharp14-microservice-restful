# ---- Etapa de compilación ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/dotnet10-csharp14-microservice-restful/dotnet10-csharp14-microservice-restful.csproj", "src/dotnet10-csharp14-microservice-restful/"]
COPY ["src/dotnet10-csharp14-microservice-restful.Application/dotnet10-csharp14-microservice-restful.Application.csproj", "src/dotnet10-csharp14-microservice-restful.Application/"]
COPY ["src/dotnet10-csharp14-microservice-restful.Infrastructure/dotnet10-csharp14-microservice-restful.Infrastructure.csproj", "src/dotnet10-csharp14-microservice-restful.Infrastructure/"]
COPY ["src/dotnet10-csharp14-microservice-restful.Domain/dotnet10-csharp14-microservice-restful.Domain.csproj", "src/dotnet10-csharp14-microservice-restful.Domain/"]

RUN dotnet restore "src/dotnet10-csharp14-microservice-restful/dotnet10-csharp14-microservice-restful.csproj"

COPY . .
WORKDIR "/src/src/dotnet10-csharp14-microservice-restful"
RUN dotnet build "dotnet10-csharp14-microservice-restful.csproj" -c Release -o /app/build

# ---- Etapa de publicación ----
FROM build AS publish
RUN dotnet publish "dotnet10-csharp14-microservice-restful.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ---- Etapa final (runtime) ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dotnet10-csharp14-microservice-restful.dll"]
