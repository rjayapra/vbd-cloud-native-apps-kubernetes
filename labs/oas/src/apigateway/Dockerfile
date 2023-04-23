FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5010

ENV ASPNETCORE_URLS=http://+:5010

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["apigateway.csproj", "./"]
RUN dotnet restore "apigateway.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "apigateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "apigateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "apigateway.dll"]
