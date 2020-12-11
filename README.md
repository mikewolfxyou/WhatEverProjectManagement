# WhatEverProjectManagement
WhatEverProjectManagement is a .Net Core 3.1 C# project, provide a project management API. 

# Business Request

### v0.1.0
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

### v0.2.0
There is new request coming in, there is another department in ACME Group - Project Audition(PA). 
Each time when a new project created/updated budget/state, PA need do an audition, and to see if project is OK or not
The new request:
* Append new properties for project:  
    * initial budget
    * current budget
    * current cost
    * initial completion date
    * current estimate completion date
    * append new state of project: audition, project in this state, the state, budget, cost and completion related columns can not be changed  
    * audition state (pending, success, failed)
    * audition result message history
* After project creation, the project wil be on auditing state, but can not go on till PA Department give a success
* Each time when project updated budget related, completion related or state related, need send a message to PA  
* PA will be another project, which process incoming project change messages, and do audition checking, give a audition state and explain of the result  


# Docker run to test
```
docker build -t projectmanagement-db -f docker/mysql_db.Dockerfile .
docker build -t projectmanagement -f docker/acme-project-management.Dockerfile .
docker-compose -f docker-compose.yml -f kafka-docker-compose.yml up
```

## Kafka (since v0.2.0 include Kafka message feature, need create Kafka topic)
Use HomeBrew install Kafka 
```
brew install kafka
```

First run `docker-compose -f docker-compose.yml -f kafka-docker-compose.yml up`
Then Create two Kafka topics, one for project audition department, one for project development department 
```
kafka-topics --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic ACMEGroup.ProjectAudition.Service.Projects
kafka-topics --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic ACMEGroup.ProjectDevelopment.Service.Projects
```
You use your browser to check if the Kafka Topic has been created:
```
http://localhost:8000
```

### Try the api:

open your browser use 
```
http://localhost:5000/swagger
```

* The project state can be found: [ProjectState.cs](https://github.com/mikewolfxyou/WhatEverProjectManagement/blob/main/src/ProjectManagement.Api/Models/ProjectState.cs)
: Planned - 0, Active - 1, Done - 2, Failed - 3

* participant should be employee ids (integer) json array: e.g. `[1,2,3]`
* progress is float(4,2) number or 0

### Query Employee
* Query all employees
``` 
GET /employee-query/employee
```

* Get employee by id
```
GET /employee-query/employee/{employeeId}
```

### Project management

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
* Download Kafka Binary: https://kafka.apache.org/downloads or use HomeBrew install Kafka

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

 

