# Use the official .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY ./publish ./
ENTRYPOINT ["dotnet", "FinancialAgentHelperApp.dll"]