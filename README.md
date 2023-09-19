# powerplant-coding-challenge

- The goal of this application is to provide a solution for the powerplant-coding-challenge.

- The solution is Domain-Driven Design (DDD)-based or simply well-factored, SOLID applications using .NET Core. Learn more about these 

### topics here:

- SOLID Principles for C# Developers SOLID Principles of Object Oriented Design (the original, longer course)

#### Domain-Driven Design:

###### The Application Contract Project: 
    - This provides the service contracts to the Web Layer. the following properties involves in this layer

      - DTOs
      -  Manager Interfaces

###### The Application Project: 
 - This Layer covers the implemetation of the service contacts and the unit of work (Calling the other microservices and Domain services) And other non domain oriented service implementations are done here.

          - Service Implementations
          - UnitOfWork
          - Other non Domain services


###### The Domain(Core) Project:

-    The Domian(Core) project is the center of the Clean Architecture design, and all other project dependencies should point toward it. As  such, it has very few external dependencies. The one exception in this case is the System.Reflection.TypeExtensions package, which is used by ValueObject to help implement its IEquatable<> interface. The Core project should include things like:
        - Models
        - Domain Services(Contracts)

###### The Configuration Project: 

-  The Configuration Project contains all the configurations.

###### The Implementations Project: 

-  this project contains the implementations of the core logics.

###### The WEB Project:

The entry point of the application is the ASP.NET Core web API, with a public static void Main method in Program.cs. It currently uses the default MVC organization (Controllers and Views folders) as well as most of the default ASP.NET Core project template code. This includes its configuration system, which uses the default appsettings.json file plus environment variables, and is configured in Startup.cs. The project delegates to the Infrastructure project to wire up its services using Configuration project.

###### The Test Projects: 

Test projects are organized based on the kind of test (unit, and integration.) 

in this project i have implemented the Unit testing using MSUnit and Integration tests using XUnit:

xunit I'm using xunit because that's what ASP.NET Core uses internally to test the product. It works great and as new versions of ASP.NET Core ship, I'm confident it will continue to work well with it.

Moq I'm using Moq as a mocking framework for white box behavior-based tests. If I have a method that, under certain circumstances, should perform an action that isn't evident from the object's observable state, mocks provide a way to test that. I could also use my own Fake implementation, but that requires a lot more typing and files. Moq is great once you get the hang of it, and assuming you don't have to mock the world (which we don't in this case because of good, modular design).

Microsoft.AspNetCore.TestHost I'm using TestHost to test my web project using its full stack, not just unit testing action methods. Using TestHost, you make actual HttpClient requests without going over the wire (so no firewall or port configuration issues). Tests run in memory and are very fast, and requests exercise the full MVC stack, including routing, model binding, model validation, filters, etc.

#### Run the Application

##### using dotnet command

navigate to the start up project folder (Web project) using commad

###### cd src/web

then run 

###### dotnet run web.csproj

it will build and start the application in http://localhost:8888

use http://localhost:8888/swagger for swagger documentation.

##### using Docker

###### start the Docker (I am using Docker desktop and linux containers)
open Docker file location in command prompt, in this project Dockerfile is placed in the Root Directory along with src and tests folders.

run below commad
###### docker build -t power-plant -f Dockerfile .

to make sure image is created use below command

###### docker images

it will show list of all the images , you can see power-plant is created.

run the below command to start the application

###### docker run -p 8888:8888 power-plant

the docker is designed to expose the http port in 8888
8888:8888 in this one the first one is the port that you want to access and second 8888 is the container port.

use http://localhost:8888/swagger for swagger documentation.

##### using visual studio

open the solution in visual studio and web project as a start up project.

then press the green Start button on the Visual Studio toolbar, or press F5 or Ctrl+F5. 

F5 will start the application in debugger mode. it will open the swagger url automatically in your default browser.

##### Test the application using visual studio
Open Test Explorer. To open Test Explorer, choose Test > Test Explorer from the top menu bar (or press Ctrl + E, T).
Run your unit tests and integration test by clicking Run All (or press Ctrl + R, V).

Thanks for the Opportunity :)