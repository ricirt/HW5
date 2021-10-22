#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Bugs_N_Roses.API/Bugs_N_Roses.API.csproj", "Bugs_N_Roses.API/"]
RUN dotnet restore "Bugs_N_Roses.API/Bugs_N_Roses.API.csproj"
COPY . .
WORKDIR "/src/Bugs_N_Roses.API"
RUN dotnet build "Bugs_N_Roses.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bugs_N_Roses.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bugs_N_Roses.API.dll"]