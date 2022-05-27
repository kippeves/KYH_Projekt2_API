# KYH Projekt2 API

## The API

The api has three endpoints: 1. __/customers__ 2 .__/projects__ and 3. __/timereg__.

_Note: Deletes for all entities are non-volatile, and are read as a bool called IsActive in either entity. Methods read the bool to consider if the entity asked for is considered "deleted" or not. I have not implmemented a "un-delete" function._

### /Customers

It defaults to only showing information about the bare entity, not the projects or timeregistrations connected to it. Projects connected to a customer can be shown using _/customers/[CustomerID]/projects_ and will contain the timeregistrations connected to it for ease of use if you want to show it in a eventlist, per example. 

### /Projects
This endpoints show the projects used in the system. It defaults to showing all the timeregistrations connected to the project as well as customer/owner. 
It is possible to get timeregistrations for a singular project using /projects/[ProjectID]/timereg. It is currently used for the React-project but it is  redundant as it's included in the index-method.

### /Timereg
This endpoint shows timeregistrations in the system. It's possible to filter timeregistrations per customer by using /customer/[CustomerID].

## Database Model

![Database Diagram](/Diagram.png)

This is a very basic database, A customer has a name, a project has a name and a owner/customer and a project has a customer and project, start-time, end-time and description. It's a triangle-relationship that fulfills the requirements set.

## Systems Model
Thie is a rough model of the architecture the Project is based on. The WebAPI was built using C# 6.0 using ASP.NET Core, Entity Framework Core and MsSQL 2019. The React-application the model described was built in [this repo](https://github.com/kippeves/KYH_Projekt2_React). The MVC-app was never realized in code but was mentioned in the presentation of the project.

![SystemModel-Page-2](https://user-images.githubusercontent.com/3217872/170511840-03c23e32-9e78-4baa-9d18-4be6577b3f77.png)
