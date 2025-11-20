https://yaman-store-htbme5b5frefghag.indonesiacentral-01.azurewebsites.net/

ECommerce Backend API (.NET 8) 

Summary Overview

This project represents the back-end system of an e-commerce platform and is built using ASP.NET Core Web API following a clean three-layer architecture consisting of DataAccessLayer, BusinessLogicLayer, and ECommerceBackend. The system provides functionalities such as product management, category management, user authentication, shopping cart operations, and order processing, with full support for JWT-based authentication and authorization.

Architecture Summary

1- DataAccessLayer (DAL)

This layer contains the core entities such as Product, Category, Order, and OrderItem. It uses DbContext to communicate with SQL Server and applies the Repository Pattern for data access operations.
Its responsibility is to perform all database-related operations.

2- BusinessLogicLayer (BLL)

This layer includes the business logic services such as CartService, ProductService, and OrderService, along with DTOs used for data transfer between layers.
Its responsibility is to process data from the DAL layer and apply the business rules of the system, such as cart management, order creation, and validation.

3- ECommerceBackend (Web API)

This layer includes the Controllers for products, categories, cart, orders, and authentication.
Its responsibility is to expose RESTful API endpoints for use by front-end applications such as Blazor or any other client.

Authentication and Authorization

The project uses JWT-based authentication with Access Tokens and Refresh Tokens, along with role-based authorization such as Admin and User.

Technologies Used

•ASP.NET Core 8 Web API
•Entity Framework Core
•SQL Server
•Layered Architecture
•Repository Pattern
•DTOs
•LINQ
•JWT Authentication and Authorization
•Async and Await
•Dependency Injection
•RESTful API Design

[DB.pdf](https://github.com/user-attachments/files/23664174/DB.pdf)

Database Diagram (ERD) Explanation

This ERD represents the complete structure of the e-commerce database. It shows all relationships between the main tables such as users, products, orders, cart, stock, addresses, and categories.
The design uses One-to-Many and Many-to-Many relationships through linking tables to ensure a scalable and efficient data structure.
