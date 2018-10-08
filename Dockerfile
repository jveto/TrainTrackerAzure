FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["TrainTracker.csproj", "TrainTracker/"]
RUN dotnet restore "TrainTracker/TrainTracker.csproj"
COPY . .
WORKDIR "/src/TrainTracker"
RUN dotnet build "TrainTracker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TrainTracker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TrainTracker.dll"]