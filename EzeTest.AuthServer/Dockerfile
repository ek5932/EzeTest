FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["EzeTest.AuthServer/EzeTest.AuthServer.csproj", "EzeTest.AuthServer/"]
RUN dotnet restore "EzeTest.AuthServer/EzeTest.AuthServer.csproj"
COPY . .
WORKDIR "/src/EzeTest.AuthServer"
RUN dotnet build "EzeTest.AuthServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EzeTest.AuthServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EzeTest.AuthServer.dll"]