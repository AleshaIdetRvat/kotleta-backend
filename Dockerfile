FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MariyaBackend.csproj", "./"]

RUN dotnet tool install --global dotnet-ef
# Установить переменную среды PATH чтобы включить dotnet tools
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet restore "MariyaBackend.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "MariyaBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MariyaBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MariyaBackend.dll"]
