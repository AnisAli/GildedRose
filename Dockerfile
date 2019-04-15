FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY GildedRose.API/GildedRose.API.csproj GildedRose.API/
COPY GildedRose.Data/GildedRose.Data.csproj GildedRose.Data/
COPY GildedRose.Tests/GildedRose.Tests.csproj GildedRose.Tests/
RUN dotnet restore
COPY . .
WORKDIR /src/GildedRose.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GildedRose.API.dll"]