# AspnetMicroservies
AspnetMicroservies

## Develop E-commerce Platform Using:
## Microservices Architecture and Implementation on .NET 5
## Building Microservices on .Net which used Asp.Net Web API, Docker, gRPC, RabbitMQ, Ocelot API Gateway, MongoDB, Redis, SqlServer


There is a couple of microservices which implemented e-commerce modules over Catalog, Basket, Discount and Ordering microservices with NoSQL (MongoDB, Redis) and Relational databases (PostgreSQL, Sql Server) with communicating over gRPC and RabbitMQ Event Driven Communication and using Ocelot API Gateway.

## I have implemented below features over the run-aspnetcore-microservices repository Until Now.

### Catalog microservice which includes;
ASP.NET Core Web API application.
REST API principles, CRUD operations
MongoDB database connection and containerization
Repository Pattern Implementation
Swagger Open API implementation

### Basket microservice which includes;
ASP.NET Web API application
REST API principles, CRUD operations
Redis database connection and containerization
Consume Discount Grpc Service for inter-service sync communication to calculate product final price
Publish BasketCheckout Queue with using MassTransit and RabbitMQ

### Discount microservice which includes;
ASP.NET Grpc Server application
Build a Highly Performant inter-service gRPC Communication with Basket Microservice
Exposing Grpc Services with creating Protobuf messages
Using Dapper for micro-orm implementation to simplify data access and ensure high performance
PostgreSQL database connection and containerization
