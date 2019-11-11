# AzureFunction-AppInsightsLogging

This is a little project to experiment with logging to Application Insights in an Azure Function using a `TelemetryClient` instead of the `ILogger` infrastructure which is commonly used.  I want to make use of `TelemetryClient` directly because it allows to use all features that are exposed by Application Insights.  
An `ITelemetryInitializer` is used to make sure that all logs and traces are linked to the correct function invocation.
