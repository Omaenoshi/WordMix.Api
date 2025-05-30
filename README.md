﻿# WordMix.Api

## Short service name (For example: PictureStorageGateway)

## Description

- Short service description. (For example: Best picture storage in the world)
- API main functions. (For example: Provides easiest way to store and share pictures)
- Anything important to potential service consumers...

## Prerequisites

Make sure you have installed all of the following prerequisites on your development machine:

- Git - [Download & Install Git](https://git-scm.com/downloads). OSX and Linux machines typically have this already
  installed.
- .NET Core (version 8.0 or
  higher) - [Download & Install .NET Core](https://dotnet.microsoft.com/download/dotnet-core/8.0).

## Deployment

Service deployment description

## Configuration

### ConnectionStrings

Database connection settings.

Example:

```json
{
  "ConnectionStrings": {
    "Main": "Server=localhost;Port=5432;Database=localhost;User Id=user1;Password=password1;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=1024;"
  }
}
```

### Logging

Logging settings.

Example:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

### OpenTelemetry

OpenTelemetry Exporter settings.

Example:

```json
{
  "OtlpExporterOptions": {
    "Endpoint": "http://localhost:4317"
  }
}
```

## AspNet Metrics

AspNet metrics format is described
in [Byndyusoft.Execution.Metrics](https://github.com/Byndyusoft/Byndyusoft.Execution.Metrics) Readme file.

## General folders layout

### src

- Api - Web API Application
- Api.Client - Web API client and DI registration for consumers
- Api.Contracts - Web API client contracts
- DataAccess - Data access layer
- Domain - Application business logic
- Migrator - Database migrator based on https://github.com/fluentmigrator/fluentmigrator

### tests

- IntegrationTests - Web API Integration Tests
- UnitTests - Unit Tests

## Api development lifecycle

- Implement logic in `src`
- Add or adapt unit and integration tests (prefer before and simultaneously with coding) in `tests`
- Add or change the documentation as needed
- Open pull request in the correct branch. Target the project's `master` branch

# Maintainers

[github.maintain@byndyusoft.com](mailto:github.maintain@byndyusoft.com)