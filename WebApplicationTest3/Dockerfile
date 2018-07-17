FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY WebApplicationTest3/WebApplicationTest3.csproj WebApplicationTest3/
RUN dotnet restore WebApplicationTest3/WebApplicationTest3.csproj
COPY . .
WORKDIR /src/WebApplicationTest3
RUN dotnet build WebApplicationTest3.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebApplicationTest3.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApplicationTest3.dll"]
