FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 80
# use forward headers
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY DaprTest.csproj ./
RUN dotnet restore DaprTest.csproj

# copy everything and build
COPY . .

RUN dotnet publish -c Release -o out

# build runtime image
FROM base AS final
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "DaprTest.dll"]