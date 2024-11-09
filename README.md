# Podme Subscription API

A simple API that allows users to subscribe and pause Podme subscriptions.

## Features

- Start subscription
- Pause subscription
- InMemory database with seeded users
- CQRS pattern with MediatR
- Clean Architecture
- Global exception handling

## Prerequisites

- .NET 8.0 SDK

## Test Users (Seeded) Podme.Infrastructure -> PodmeDbContext.cs

- john.doe@gmail.com
- jane.smith@gmail.com
- william.johnson@gmail.com
- emily.davis@gmail.com
- michael.brown@gmail.com
- sarah.martin@gmail.com
- daniel.garcia@gmail.com
- laura.jackson@gmail.com
- james.lee@gmail.com

## Architecture

The solution follows Clean Architecture principles:
- Podme.API: API layer with controllers
- Podme.Application: Application logic, CQRS handlers
- Podme.Domain: Domain entities and business rules
- Podme.Infrastructure: Data access and external concerns

## Technologies Used

- ASP.NET Core 8.0
- Entity Framework Core (InMemory)
- MediatR
- AutoMapper
- FluentValidation
- Swagger/OpenAPI

## Sonarcloud Report 
https://sonarcloud.io/project/overview?id=thilandakshina_Podme