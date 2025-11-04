#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
 
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7071
ENV ASPNETCORE_URLS=https://+:7071
 
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
 
COPY ["eSalonLjepote.Subscriber/eSalonLjepote.Subscriber.csproj", "eSalonLjepote.Subscriber/"]
RUN dotnet restore "eSalonLjepote.Subscriber/eSalonLjepote.Subscriber.csproj"
 
COPY . .
WORKDIR "/src/eSalonLjepote.Subscriber"
RUN dotnet build "eSalonLjepote.Subscriber.csproj" -c Release -o /app/build
 
 
FROM build AS publish
RUN dotnet publish "eSalonLjepote.Subscriber.csproj" -c Release -o /app
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eSalonLjepote.Subscriber.dll"]