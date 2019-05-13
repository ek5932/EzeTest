FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["EzeTest.TestRunner.WebApi/EzeTest.TestRunner.WebApi.csproj", "EzeTest.TestRunner.WebApi/"]
RUN dotnet restore "EzeTest.TestRunner.WebApi/EzeTest.TestRunner.WebApi.csproj"
COPY . .
WORKDIR "/src/EzeTest.TestRunner.WebApi"
RUN dotnet build "EzeTest.TestRunner.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EzeTest.TestRunner.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EzeTest.TestRunner.WebApi.dll"]