# FinancialAgentHelperApp

A web-based tool for financial agents to generate and visualize long-term insurance and investment plans for clients. Built with ASP.NET Core MVC (.NET 9), it allows users to input client details and receive detailed projections for insurance and investment growth over time.

## Features
- Input client details: name, age, insurance coverage, monthly premium, investment, years to pay, and interest rate.
- Calculates and displays:
  - First period: Years of premium/investment payments
  - Second period: Next 10 years after payments stop
  - Third period: Remaining years up to age 100
- Visualizes yearly projections: policy year, age, payments, insurance cost, investment, growth, and total value.
- Form validation and error handling.

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- (Optional) [Docker](https://www.docker.com/)

### Running Locally
1. Clone the repository:
   ```bash
   git clone <repo-url>
   cd fincial-agent-helper1/FinancialAgentHelperApp
   ```
2. Run the app:
   ```bash
   dotnet run
   ```
3. Open [http://localhost:5000](http://localhost:5000) in your browser.

### Publishing and Deploying
1. Publish the app:
   ```bash
   dotnet publish -c Release -o ./publish
   ```
2. (Optional) Create a ZIP for Azure deployment:
   ```bash
   cd publish
   zip -r ../FinancialAgentHelperApp.zip .
   ```
3. Deploy to Azure Web App:
   ```bash
   az webapp deployment source config-zip --resource-group <ResourceGroup> --name <WebAppName> --src ../FinancialAgentHelperApp.zip
   ```
   Adjust `<ResourceGroup>` and `<WebAppName>` as needed.

#### Docker
1. Build and run the Docker container:
   ```bash
   docker build -t financial-agent-helper .
   docker run -d -p 5000:80 financial-agent-helper
   ```
2. Visit [http://localhost:5000](http://localhost:5000)

## Project Structure
- `Controllers/` — Handles web requests and business logic
- `Models/` — Data models for financial plans and projections
- `Views/` — Razor views for UI (form input, results tables)
- `publish/` — Output directory for published builds
- `Dockerfile` — For containerized deployment

## Live Demo
[https://FinancialAgentHelperApp-Pratham2025.azurewebsites.net](https://FinancialAgentHelperApp-Pratham2025.azurewebsites.net)

## License
MIT (or specify your license)
