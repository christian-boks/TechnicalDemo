

# Philosophy

* Make a clear list of requirements - share them with the customer and be sure to be aligned before starting.
* Break the requirements down into smaller tasks.
* Iterate, solve one task at a time and test before moving on to the next. If possible let the customers have access to early versions. Makes it easier to validate the solution instead of just relying on text and mockups. Better to find any problems early.


Software development saying:
* Make it work.
* Make it correct.
* Make it robust / fast.


# Requirements

We want to model districts, stores, and sales persons. The business rules should be enforced by
constraints in the database.

* District always has a single primary sales person.
* The same salesperson can be the primary salesperson for multiple districts.
* Districts have multiple stores located in them.
* Districts can sometimes have one or more secondary sales persons.


## Data model

``` sql
CREATE TABLE salesperson (
  id SERIAL PRIMARY KEY, 
  name VARCHAR(100) NOT NULL
);

CREATE TABLE district (
  id SERIAL PRIMARY KEY, 
  name VARCHAR(100) NOT NULL, 
  primary_salesperson_id INTEGER NOT NULL REFERENCES salesperson
);

CREATE TABLE store (
  id SERIAL PRIMARY KEY, 
  city VARCHAR(100) NOT NULL, 
  district_id INTEGER NOT NULL REFERENCES district
);

CREATE TABLE district_salesperson (
  salesperson_id INTEGER NOT NULL REFERENCES salesperson, 
  district_id INTEGER NOT NULL REFERENCES district,
  PRIMARY KEY (salesperson_id, district_id)
);
```

## Sample Data
``` sql
-- Create sales persons
INSERT INTO salesperson (id, name)
VALUES (1,'Kurt');

INSERT INTO salesperson (id, name)
VALUES (2,'Hans');

INSERT INTO salesperson (id, name)
VALUES (3,'Frank');

-- Make Kurt (1) the primary sales person for the Northern Denmark (1) district.
INSERT INTO district (id, name, primary_salesperson_id)
VALUES (1, 'Northern Denmark', 1);

-- Make Hans (2) the primary sales person for the Southern Denmark (2) district.
INSERT INTO district (id, name, primary_salesperson_id)
VALUES (2, 'Southern Denmark', 2);

-- Make Hans (2) the primary sales person for the Eastern Denmark (3) district.
INSERT INTO district (id, name, primary_salesperson_id)
VALUES (3, 'Eastern Denmark', 2);

-- Create stores
INSERT INTO store (id, city, district_id)
VALUES (1, 'Aalborg', 1);

INSERT INTO store (id, city, district_id)
VALUES (2, 'Esbjerg', 2);

INSERT INTO store (id, city, district_id)
VALUES (3, 'Copenhagen', 3);

-- Make Frank (3) the secondary sales person for the Northern Denmark (1) district.
INSERT INTO district_salesperson (salesperson_id,district_id) VALUES (3,1);
```

# API Design

Based on the given requirements we can determine how we should design the API.

## Requirements

* Show all districts in a list.
* Add/remove sales persons from a district.
    * It must be possible to specify whether the salesperson should be primary or secondary. 
* List sales persons associated with a district.
* List stores belonging to a district.

## REST API
``` api
GET    /api/v1/districts - return all districts.

GET    /api/v1/salespersons/districts/{id} - return all sales persons for district.
POST   /api/v1/salespersons/districts/{id} - add sales person to district.
DELETE /api/v1/salespersons/{id}/districts/{id} - remove sales person from district.

GET    /api/v1/stores/districts/{id} - return all stores belonging to district.
```

## GRPC API
``` proto
service BackendApi {
  rpc GetAllDistricts(google.protobuf.Empty) returns (GetAllDistrictsReply);

  rpc GetAllSalesPersonsForDistrict(GetAllSalesPersonsForDistrictRequest)
      returns (GetAllSalesPersonsForDistrictReply);

  rpc AddSalesPersonToDistrict(AddSalesPersonToDistrictRequest)
      returns (google.protobuf.Empty);

  rpc RemoveSalesPersonFromDistrict(RemoveSalesPersonFromDistrictRequest)
      returns (google.protobuf.Empty);

  rpc GetAllStoresForDistrict(GetAllStoresForDistrictRequest)
      returns (GetAllStoresForDistrictReply);
}
``` 


# Backend Service Design

Based on the requirements for the data and API we can determine the number of repositories and controllers.

The holy(?) trinity of software design recommends the following guidelines:
1. Presentation
2. Business Logic
3. Data Access

## 1. Presentation
In this case the presentation is where the client will interact with the service, on the http level. So the first layer will be the controller layer, which will be 
responsible for all things http. In this example swagger annotations.

## 2. Business logic
This is the service layer where we can encapsulate all our business logic in the service.

## 3. Data Access
We structure the data access layer as a repository class for each table in the database.

### Decoupling
This design gives us a lot of freedom to compose the application of smaller independent modules. We get separation of concerns, each layer is only responsible for its part. 
And by using the dependency injection pattern we make it easier to test the software, since we can easily mock out parts of the system we aren't testing in a test.

# Error handling

Each layer should be responsible for its own error types.

* We don't want sql errors showing up in the client, and we don't want the repository classes to handle html status codes.
* Adding some kind of middleware to the application to handle any errors can be a good solution, depending on the size of the application.
* Generate unique id for any user facing errors, that can be shown to the user in the error message. This will help in locating the problem in the logs later.

# Testing
The testing pyramid dictates that we want the cheapest (runtime) to be the ones we have the most of. So most unit tests, then integration tests, and then end to end tests.

* Keep the unit tests in the same place as the code.
* Have integration and e2e tests in their own projects.
* Make it easy for developers to run the tests. 


# Sharing API and models between backend and frontend

As an application grows over time it can be difficult to keep the backend API and the frontend in sync. 

A great way to keep the two synchronized is to use the generated openapi specs as the source of truth

Have the application generate the specs as json / yaml on build. And use these to automatically generate 
code for the frontend and other services that wants to communicate with our application.


# Frontend application design

Much as same as the backend, structure code in reusable components / services.
Separation of concerns.

## Angular framework 
Very opinionated and suggests the following structure of the client application:

* UI parts into components. 
* Components grouped by modules.
* Business logic and communication with servers in services.

Most of the principles used on the backend design apply in frontend development as well. We want low coupling and separation of concerns. Dependency injection to ease composition and testing. 
Same goes for testing. However doing e2e tests would require some kind of backend. 

* Running a backend locally for the frontend to test against can be tricky.
  * If the system is large it could take too many resources to be practical. 
* Have a dedicated test system with the latest backend/frontend software installed.
* Use a mock backend where the responses to requests can be controlled.
  * This requires some work to keep synchronized with the actual backend. 


# Running in Production

Backend:
* Package service up in a docker image and have it run in on kubernetes (helm).
* Add authentication / authorization.
  * Depending on consumer should key be returned as cookie (browser) or "Authorization: Bearer" header (API).
  * Use JWTs or session ids. 
    * JWTs are simple and allows a service to locally check if access is allowed. But it is hard to revoke access for a compromised token.
    * Session ids require all services to talk to a session service to check access and permissions. But it becomes very easy to 
* Add indexes to database.
* Manage database migrations as schema changes over time.
* Add a cache if required.
* Export swagger definition and store as a separate versioned artifact, that other clients can consume.
* Feature flags - turn features on/off without a deployment. Could be used for A/B testing.
* Metrics - how much / duration are the exposed endpoints used.
* Monitoring - could be based on metrics. 
* Distributed tracing FTW!

Frontend:
* Deploy to a CDN (if running in the cloud)
* Feature flags - turn features on/off without a deployment. Could be used for A/B testing.


# CI/CD

* All code pushed to a branch is built and (all) tests are run. Any problems are reported back to the developer. This will help figure out if it only "works on my machine", or if it actually would work in production as well.
* Don't allow a pull request to be merged to the master branch if there are any errors.
* Require a certain % of new added lines in a PR to be unit tested.
* Make sure the build machine is fast and that the build + running tests are fast as well. The lower iteration time the better. This will give happy and productive developers.
* Automatic rollback if spike in errors after deployment. Can be tricky to pull off, as errors can manifest in other parts of the system, if it is a large system.

