FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["NotificationListener.csproj", "./"]
RUN dotnet restore "NotificationListener.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "NotificationListener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationListener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationListener.dll"]
