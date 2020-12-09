# WhatEverProjectManagement
WhatEverProjectManagement is a .Net Core 3.1 C# project, provide a project management API. 

# Business Request
Someday the CTO may reach out to your team and ask you to build a solution for managing these projects. Together with your Product Owner you would have talked with all stakeholders and gathered their requirements.

Based on these requirements the team decided that UX and Frontend will build the UI with the following features. Your task will be to provide the necessary APIs for the below:

* Overview of all projects
* Creating new projects
* Updating projects
* Assigning participants to a project.

After an analysis of the requirements you should have the following insights:

* A project must have a name, owner and state (planned, active, done, failed). The progress of active projects can be measured in percentage
* Only managers can be the owner of a project
* Participants can be assigned to a project
* Participants must be part of the same department as the owner
* Assignable participants or owner are employees, which are managed via an existing dedicated API (See https://employees-api.vercel.app/ for the API documentation)
It's now up to you to design and build an API which fulfils these requirements.

# Docker run to test
```
docker build -t projectmanagement -f docker/acme-project-management.Dockerfile .
docker run -it --rm -p 5000:5000 -e ASPNETCORE_ENVIRONMENT=Development --name projectmanagement projectmanagement
```

open your browser use 
```
http://localhost:5000/swagger
```
to try the api:

There is a memory data storge:
The Employee data can be found: [EmployeeDao.cs](https://github.com/mikewolfxyou/WhatEverProjectManagement/blob/main/src/ProjectManagement.Api/DataAccess/EmployeeDao.cs)
The project state can be found: [ProjectState.cs](https://github.com/mikewolfxyou/WhatEverProjectManagement/blob/main/src/ProjectManagement.Api/Models/ProjectState.cs)

Planned - 0, Active - 1, Done - 2, Failed - 3

* Overview of all projects
```
GET /project-management
```

* Creating new projects
```
POST /project-management/project
```

* Updating projects and Assigning participants to a project.
```
PUT /project-management/project/{id}
```

# Development
* Download .Net Core 3.1: https://dotnet.microsoft.com/download
* Download Rider: https://www.jetbrains.com/rider/

After install .Net Core and Rider, in Rider open solution file: ACMEProjectmanagement.sln 

## run test
In the command line 
```
dotnet test
```
## run code
In the command line
```
cd src/ProjectManagement.Api
dotnet run
```
 then open the browser use url:
```
http://localhost:5000/swagger
```

# TODO 
Append more unit tests for the validator and project reppsitory. 
