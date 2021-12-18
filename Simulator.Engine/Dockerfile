FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Simulator.Engine/Simulator.Engine.csproj", "Simulator.Engine/"]
RUN dotnet restore "Simulator.Engine/Simulator.Engine.csproj"
COPY . .
WORKDIR "/src/Simulator.Engine"
RUN dotnet build "Simulator.Engine.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simulator.Engine.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simulator.Engine.dll"]
