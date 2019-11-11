# AzureFunction-AppInsightsLogging

This is a little project to experiment with logging to Application Insights in an Azure Function using a `TelemetryClient` instead of the `ILogger` infrastructure which is commonly used.  I want to make use of `TelemetryClient` directly because it allows to use all features that are exposed by Application Insights.  
An `ITelemetryInitializer` is used to make sure that all logs and traces are linked to the correct function invocation, but this doesn't seem to be working for now.

## Create Azure Resources

This repository contains an [ARM template](./azure/resources.json) that lets you create all Azure Resources that are required to experiment with this.  

## Run the function

After the Azure Resources have been created, the function can be deployed to Azure or can be run locally.  

To run the Function locally from your development computer, add a `local.settings.json` file to the project and make sure that the TelemetryKey of the previously deployed Application Insights resource is used as the `APPINSIGHTS_INSTRUMENTATIONKEY` setting:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "APPINSIGHTS_INSTRUMENTATIONKEY": "<your_appinsights_telemetrykey>"
  }
}
```
