# MarathonAppV2
Here you can check website https://my.runthesilkroad.com/user/login
![Alt Text](/images/runningImg.jpeg)

I developed a backend part of the web application for the RunTheSilkRoad organization, which manages marathon events in Kyrgyzstan. The app streamlines various business processes such as participant registration, event organization, paying fees and result tracking, all previously handled manually with basic tools like Excel and Google Forms. This project modernized their operations to accommodate the rapidly growing number of marathon participants and events.

## Requirements
### User part:

1. Registration - User will fill in the fields with email, password, and confirm password, in order to be saved in the system, mail will be sent to confirm the email in the system.
2. Authorization/Authentication - User will have a role for accessing just allowed parts of the website, login, and logout.
3. Authorization via Google - It is possible for users to be saved in the system and login to the website by login Google.
4. Forget/Change password - It is possible for user to change password even if user forgot the password.
5. Profile - In the “Profile” section of the personal account, the user must specify: 1) First name 2) Last name 3) Date of birth 4) Gender. All these data are not available for editing after initial filling. He should also be able to indicate: 1) T-shirt size 2) Contact number 3) Emergency number 4) Participation country. The specified data is available for editing.
6. Documents - In order to take starter kit before marathon: ID, medical insurance and certificate of disability(if participant is disable) must be uploaded and confirmed by manager or volunteer.
7. User marathons - All marathons in which user participate will be in this section.
8. User results - All marathos in which user participated with results will be in this section.
9. Application - User is able to apply to the marathon’s single distance.
10. Marathons - User is able to view all available marathons.


### Admin panel:

10. Roles in admin panel - There is one owner who has access to every part of admin panel, admin also has access to every part of admin panel(except creation, editing, deleting volunteers and admins), volunteer is limited depending on business process.
11. Creation of volunteers and admins - Owner is able to create volunteers or admins.
12. All users - Volunteers and admins are able to view all users, also admins are able to edit users, owner is able to view all users, volunteers, admins and edit them.
13. All applicants - Volunteers and admins are able to view all applicants of the specific marathon.
14. Confirm or deny status - Volunteers and admins are able to confirm or deny status of the user’s documents.
15. Voucher - Admins are able to create, view, delete and download vouchers as Excel file for the corporate applicants.
16. Starter kit - Volunteers and admins are able to issue starter kits.
17. Marathons - Admins are able to create marathons.


## Design
### Architecture
The Project follows clean architecture with CQRS pattern. CQRS is done using Mediatr library. Here is a diagram of architecture:
![Alt Text](/images/cqrs.jpeg)

The WebApi Project’s Infrastructure is dependent on Domain and Application projects. Application is dependent on Domain Project.
https://www.youtube.com/watch?v=tLk4pZZtiDY&t=379s
### Mediatr
Mediatr used in this template to implement CQRS Pattern. Here is a diagram of the CQRS Pattern:
![Alt Text](/images/cqrs2.png)
![Alt Text](/images/mar1.png)

You send Commands and Queries using Mediatr in your controller in this way: **await _mediator.Send(command);**
You can also add behaviours to your mediatr request. Configs are here: **src/WebApi/Common/Extensions/MediatrServices/MediatrServiceExtension.cs**

### Objectives of architecture
- Base for future extensions.
- Clear picture of the workflow of the application.
- Speed of development.

## Implementation

### Solution Structure
### Conventions

- All RequestDto should be named with postfixRequestDto:**TransferRequestDto**
- All Commands should be named with postfix Command:**WithdrawCommand**
- All Queries should be named with postfix Query:**GetUserAccountQuery**
- All Responses from Commands and Queries should be named with postfix OutDto:**GetUserAccountOutDto**
- All folders should be created using plural form:
**+ Correct: Extensions**
**- Incorrect: Extension**

### Responsibility of each layer

### WebApi Project

Depends on Application, Domain and Infrastructure Projects.
Consists of Common,Endpoints, staticfiles Folders and appsettings.json.

- Common folder contains all Common things related our API (For example: you may create folders for Extensions, Bases, Attributes, Helpers, Filters, Middlewares, Resources).

- Endpoints folder contains Controllers, Request Dtos with their Validaitons, Swagger Examples (Divided based on entity that you are mainly working on)

- staticfiles folder contains files of the project(For example: the logos of the partner companies)
Contains Swagger, Request DTOs for binding, Swagger Examples, Library Configurations, Sending Commands and Queries using Mediatr.

- Library Configurations Path: src/WebApi/Common/Extensions (Ef, Validation, Mediatr and other services cnofigs are here)
- Validation of Request Dtos: src/WebApi/Endpoints/Users/Dtos/Requests (Divided based on entity). See example of validation in src/WebApi/Endpoints/Accounts/Dtos/Requests/TransferRequestDto.cs
- Swagger Response and Request Examples: src/WebApi/Endpoints/Users/Dtos/SwaggeExamples (Divided based on entity)

### Infrastructure Project

Depends on Application, Domain.

Consists of Common and Persistence Folders + any folder for other tasks not related to Business logic. For example, Aunthecation Services, External API Calls, Cloud APIs, File Manipulation Services and others

- Common folder contains all Common things related our Infrastructure (For example you may create folders for Extensions, Bases, Attributes, Helpers, Constants, Resources).
- Persistence folder contains EF Configurations, Migrations, Repositories, UnitOfWork, Seeds (basically DAL)

Contains external logic not related to Business logic.

- Migrations Folder: src/Infrastructure/Persistence/Migrations
- Fluent API Configs src/Infrastructure/Persistence/Configurations
- Repositories src/Infrastructure/Persistence/Repositories (Feel free to extend BaseRepository)
- Seed src/Infrastructure/Persistence/Seed

### Application Project

Depends on Domain.

Consists of Common and Usecases Folders.

- Common folder contains all Common things related our Application (For example you may create folders for Extensions, Bases, Interfaces, Helpers, Behaviours, Resources).

- Usecases folder contains Commands, Queries, their Handlers, OutDtos
Contains mapping logic, using the repository to retrieve data, and calling domain services.

- Query Folder: src/Application/UseCases/Accounts/Queries
- Command Folder src/Application/UseCases/Accounts/Commands

### Domain Project

Depends on some libraries. As little as possible.

Consists of Common, Entities and Services Folders.

- Common folder contains all Common things related to our Application (For example you may create folders for Extensions, Bases, Interfaces, Helpers, Exceptions, Domain Validations, Contracts, and Resources).
- Entities folder contains Entities with Exceptions related to these entities
- Services folder contains Services that only work with Entities, external services should be defined in Infrastructure layer.

Contains business logic using services and entities. Talks with infrastructure using contracts(interfaces)

- Entities Folder: src/Domain/Entities (Divided based on the schema)
- Services Folder: src/Domain/Services
- Domain Validation Folder src/Domain/Common/Validations
- Resources Folder src/Domain/Common/Resources
- Common Exceptions Folder src/Domain/Common/Exceptions
- Contracts Folder src/Domain/Common/Contracts (Infrastructure are using those interfaces to implement them)
- Constants Folder src/Domain/Common/Constants


### Technical Details

### API Design

REST Best practices are using on the current template while designing current API. More about best practices you may found here.

### Error Handling

For error handling I used popular library Hellang.Middleware.ProblemDetails.

This library allows to return exception in RFC 7807 standart called Problem Details. I created 5 common Exceptions and mapped them to the appropriate Status Codes located in the file: src/WebApi/Common/Extensions/ErrorHandlingServices/ErrorHandlingServiceExtension.cs. Feel Free to modify this file for writing mappings for your exceptions. You should also provide error code in the form of integer. Error code is unique identifier of the exception. I also extended built-in Problem Details model to include error codes in response to the error: src/WebApi/Common/Extensions/ErrorHandlingServices/CustomProblemDetails.cs

### Current Base Exceptions:

- Validation Exception -> Throws by Fluent Validation. Mapped to Status Code 400
- AuthenticationCustomException (src/Domain/Common/Exceptions/AuthenticationCustomException.cs) -> Should be thrown when user is not authenticated. Mapped to Status Code 403
- AuthorizationException (src/Domain/Common/Exceptions/AuthorizationException.cs) -> Should be thrown when user is not authorized. Mapped to Status Code 401
- NotFoundException (src/Domain/Common/Exceptions/NotFoundException.cs) -> Should be thrown when entity is not found. Use First() method from BaseRepository to automatically throw this exception when entity is not found. Mapped to Status Code 404
- InnerException (src/Domain/Common/Exceptions/InnerException.cs) -> Should be thrown when something unexpected happens and should not be displayed on the screen. Mapped to Status Code 500
- DomainException (src/Domain/Common/Exceptions/DomainException.cs) -> Should be thrown when something is expected to happen and should be displayed on the screen. It is an abstract class, so you must create a specific Business Exception that will inherit DomainException. See example: TransferAccountLimitExceededException. Mapped to Status Code 400

### Validation

Fluent Validation Library used in this template for request dto validation. Validator logic contains in Request Dto files itself. Fluent Validation integrated with Swagger to show what validation the Dto has:
![Alt Text](/images/mar2.png)

Domain Project is also using Fluent Validation to Validate Entities before doing any manipulation with them. Use Domain validation to write common rules for some entities. For example: Before doing manipulation with account, check whether the balance is not < 0.
Configs are here:src/WebApi/Common/Extensions/FluentValidationServices/FluentValidationServiceExtension.cs

### Repositories and Unit of Work

Repository used in this template to encapsulate Data Access. It provides the following benefits:

- Improves Readability of the code
- Improves Testability of the code
- Reusing popular methods

If this is not convenient for you, you may inject directly DbContext and work with it from Handlers.

If you create new repository you should inherit from src/Infrastructure/Persistence/Repositories/Base/BaseRepository.cs because it provides useful base methods that you may need. Most of the methods were copy-pasted from the open-source library. Feel free to extend BaseRepository to have additional methods for your need.

Unit of work is needed only for encapsulation of transactions and to have only one injection instead of injecting all repositories. If you create new repository, add this repository to src/Domain/Common/Contracts/IUnitOfWork.cs accordingly. To work with repositories inject Unit of Work as follows:
![Alt Text](/images/mar3.png)

Configs are here: src/WebApi/Common/Extensions/RepositoryServices/RepositoryServiceExtension.cs

### Mapping

Mapster was used for mapping in this project. Here are advantages of the Mapster:

- It is fast
- It is convenient
- It is flexible

I used this article as an idea for Mapping strategy. To create map config for your dto you need to inherit your Dto to BaseDto<TSource, TDest> and override AddCustomMappings method. Example (src/Application/UseCases/Accounts/Queries/GetUserAccount/GetUserAccountOutDto.cs):
![Alt Text](/images/mar4.png)

Then it will automatically use your mapping config when you call Adapt() or ProjectTo() methods.

Configs are here: src/WebApi/Common/Extensions/MapsterServices/MapsterExtension.cs

Pagination, Sorting and Filtering

Gridify was used for pagination, sorting and filtering in this project. Here are advantages of the Gridify:

- It is convenient
- It is fast

To implement filtering, sorting and pagination you just need to create query parameter in your controller like this:
![Alt Text](/images/mar5.png)

Then you need to call the built-in extension method.GridifyQueryable(request.Query) on your IQuerable result like this:
![Alt Text](/images/mar6.png)

All configs come from appsettings.json

![Alt Text](/images/mar7.png)

Feel free to modify configurations per your needs.

Configs are here: src/WebApi/Common/Extensions/GridifyServices/GridifyServiceExtension.cs

### Localization

To implement localization this article was used. The current project supports the following cultures: en-us, ru-ru, default culture: en.

- All the error messages should be contained in resources, except Inner Exceptions. The texts of InnerExceptions could be located as constants since they don't require to be displayed.
- Each layer(WebApi, Infrastructure...) should have its own Resources folder with texts used in this layer.
- Each entity should have its own resource file and associated texts inside so we could avoid a lot of text messages in one resource file.

Configs are here: src/WebApi/Common/Extensions/LocalizationServices/LocalizationServiceExtension.cs

### Swagger

To have a good and readable API please use appropriate attributes to decorate Swagger. Readable API will decrease amount of explanation to the frontend side.

- [ProducesResponseType(typeof(GetUserAccountOutDto), StatusCodes.Status200OK)] -> shows what object type will be returned in case of status 200
- [ProducesDefaultResponseType(typeof(CustomProblemDetails))] -> shows what object type will be returned in case of any other response
- [Consumes(MediaTypeNames.Application.Json)] -> shows what endpoint can consume from swagger(in this case only JSON)
- [Produces(MediaTypeNames.Application.Json)] -> shows what endpoint can produce from swagger(in this case only JSON)
- [SwaggerRequestExample(typeof(WithdrawRequestDto), type of(WithdrawExamples))] -> shows request object examples
- [SwaggerResponseExample(typeof(WithdrawRequestDto), type of(WithdrawExamples))] -> shows response object examples
- Fluent Validation integrated with Swagger to show what validation the Dto has

Here is example of proper docs of swagger endpoint:
![Alt Text](/images/mar8.png)

Configs are here: src/WebApi/Common/Extensions/SwaggerServices/SwaggerServiceExtension.cs
