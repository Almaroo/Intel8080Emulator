FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Intel8080Emulator.Output/Intel8080Emulator.Output.csproj", "Intel8080Emulator.Output/"]
RUN dotnet restore "Intel8080Emulator.Output/Intel8080Emulator.Output.csproj"
COPY . .
WORKDIR "/src/Intel8080Emulator.Output"
RUN dotnet build "Intel8080Emulator.Output.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Intel8080Emulator.Output.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Intel8080Emulator.Output.dll"]
