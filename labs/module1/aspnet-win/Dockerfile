FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["demo-win.csproj", "."]
RUN dotnet restore "./demo-win.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "demo-win.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "demo-win.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "demo-win.dll"]