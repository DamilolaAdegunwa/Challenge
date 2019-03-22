FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["API/WAC.WebService.Admin/WAC.WebService.Admin.csproj", "API/WAC.WebService.Admin/"]
COPY ["API/WAC.Admin.AppService/WAC.Admin.AppService.csproj", "API/WAC.Admin.AppService/"]
COPY ["API/WAC.Domain/WAC.Domain.csproj", "API/WAC.Domain/"]
COPY ["API/FWK.Domain_Std/FWK.Domain_Std.csproj", "API/FWK.Domain_Std/"]
COPY ["API/FWK.STD/FWK.STD.csproj", "API/FWK.STD/"]
COPY ["API/FWK.AppService/FWK.AppService.csproj", "API/FWK.AppService/"]
COPY ["API/WAC.Infra.Data/WAC.Infra.Data.csproj", "API/WAC.Infra.Data/"]
COPY ["API/FWK.Infra.Data/FWK.Infra.Data.csproj", "API/FWK.Infra.Data/"]
COPY ["API/FWK.ApiServices/FWK.ApiServices.csproj", "API/FWK.ApiServices/"]
RUN dotnet restore "API/WAC.WebService.Admin/WAC.WebService.Admin.csproj"
COPY . .
WORKDIR "/src/API/WAC.WebService.Admin"
RUN dotnet build "WAC.WebService.Admin.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WAC.WebService.Admin.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WAC.WebService.Admin.dll"]