FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7071
ENV ASPNETCORE_URLS=http://+:7071

# Install telnet and other necessary tools
RUN apt-get update && apt-get install -y telnet curl

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .

FROM build AS publish
RUN dotnet publish "eSalonLjepote.API/eSalonLjepote.API.csproj" -c Release -o /app
FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "eSalonLjepote.API.dll"]