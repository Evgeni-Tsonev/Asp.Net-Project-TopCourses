#  TopCourses
ASP .NET Core MVC application designed for people who want to upgrade theis skills.
<br />

# Project Introduction
TopCourses is a ASP.NET Core MVC project I builded in course at SoftUni. <br /> The website is application for people who want to upgrade theis skills or want to teach other people. The Project follow the best practices for Object Oriented design and high-quality code for the Web application.

# Built With
* ASP.NET Core 6 MVC
* ASP.NET Core areas
* MSSQL Server
* Entity Framework Core
* AJAX
* jQuery
* Moq
* NUnit
* Stripe
* SendGrid
* MongoDB
* Bootstrap
* Font Awesome Icons
* TinyMCE
* DataTables

# Functionality
* Users Registration.
* Every User can buy course.
* Every User can add course.

# Project Architecture
* I use simple project architecture consisting of 4 projects.

1. TopCourses - ASP .NET Core Web App MVC.
2. TopCourses.Infrastructure - Class Library, holding DBContext, Migrations and DB-Models.
3. TopCourses.Services - Class Library, holding Services and Service Models.
4. TopCourses.Test - NUnit Test Project, holding Service Tests.

![](img/ProjectArchitecture.jpg)

# Quick Start && Implementation

* Administrator User - seeded by default and you can use it without making new registration if you want.
```javascript
email = admin@abv.bg
Password = admin123

email = user@abv.bg
Password = user123
```

# Database Diagram

![database-image](https://user-images.githubusercontent.com/103019435/208256758-b878e071-5632-411d-98e1-02384ef23b97.png)

# Test
## Libraries used for testing:

* NUnit
* Moq
---

* Services Test Coverage
![code-coverage](https://user-images.githubusercontent.com/103019435/208257055-c3dca996-593a-4e83-940e-3201f34e88e2.png)

Potential Tasks:

# App Images
Home Page
![home-page](https://user-images.githubusercontent.com/103019435/208256759-b9a3c14c-6b2f-4c77-9896-f1a180c1e1d4.png)
<br/>
All Courses Page
![all-courses-page](https://user-images.githubusercontent.com/103019435/208256756-307b5320-62c5-4b74-847b-ae7e51d614e6.png)
<br/>
Course Details Page
![details-page](https://user-images.githubusercontent.com/103019435/208257057-14e13361-57cc-43d2-b219-ea74996faa25.png)
<br/>
Add Course Page
![add-course-page](https://user-images.githubusercontent.com/103019435/208256754-e3395625-187d-42f8-9249-d4ad19d965da.png)
<br/>
User Page
![user-page](https://user-images.githubusercontent.com/103019435/208256762-4e186818-c190-4ee3-b3b0-ed8cf00957a4.png)
<br/>
My Learning Page
![my-learning-page](https://user-images.githubusercontent.com/103019435/208256761-d2821cd1-dfbc-421c-a9be-f5dd9b39fbb6.png)
<br/>
Admin Dashboard page
![admin-dashboard-page](https://user-images.githubusercontent.com/103019435/208256755-941b4af9-8405-43a2-8799-e10ac5e164eb.png)