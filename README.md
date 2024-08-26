# MarathonAppV2
I developed a web application for the RunTheSilkRoad organization, which manages marathon events in Kyrgyzstan. The app streamlines various business processes such as participant registration, event organization, and result tracking, all of which were previously handled manually with basic tools like Excel and Google Forms. This project modernized their operations to accommodate the rapidly growing number of marathon participants and events.

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
