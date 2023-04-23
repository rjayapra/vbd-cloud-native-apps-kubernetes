FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY published .
EXPOSE 80
ENTRYPOINT ["dotnet", "myaspcoreapp.dll"]