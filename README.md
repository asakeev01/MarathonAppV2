# MarathonAppV2
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

