FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /backend
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /backend
COPY --from=build-env /backend/out .
ENTRYPOINT ["dotnet", "TrybeHotel.dll"]
