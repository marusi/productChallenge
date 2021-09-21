#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ChamaSubAPI/ChamaSubAPI.csproj", "ChamaSubAPI/"]
RUN dotnet restore "ChamaSubAPI/ChamaSubAPI.csproj"
COPY . .
WORKDIR "/src/ChamaSubAPI"
RUN dotnet build "ChamaSubAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChamaSubAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChamaSubAPI.dll"]