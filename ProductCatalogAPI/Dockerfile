FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ProductCatalogAPI/ProductCatalogAPI.csproj ProductCatalogAPI/
RUN dotnet restore ProductCatalogAPI/ProductCatalogAPI.csproj
COPY . .
WORKDIR /src/ProductCatalogAPI
RUN dotnet build ProductCatalogAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ProductCatalogAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProductCatalogAPI.dll"]
