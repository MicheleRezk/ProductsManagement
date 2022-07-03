## Products Management

This is Products Management Api Solution to create, update, get and delete products.

### Solution

Solution consists of 3 projects:

#### Complevo.ProductsManagement

contains all backend services for products services, using MongoDB to store our products,
to test it, you have to first create a new product using this endpoint: /api/products, all services schema are in [swagger.json](swagger.json) file

#### Complevo.ProductsManagement.Tests

contains unit tests for controllers.

#### Complevo.ProductsManagement.IntegrationTests

contains the integration tests of the api services

### running integration tests

- To run the integration tests, you should have mongodb running on port 27017

### how to test it

- docker-compose up

### Future Features

it's nice to have other frontend project using React to build products management app.
