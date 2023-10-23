# HvZ-backend

## Introduction and Overview
This project is backend code for a Humans vs Zombies game management system. EntityFramework Code First workflow is used
to create a database in SQL Server. The backend is an ASP.NET Core Web API written in C#, and is utilized by the corresponding
frontend to access and manipulate the database. The database consists of these tables:

| Table name   | Description |
| ------------ | ----------- |
| AppUser      | Stores a Keycloak user's details for unique identification |
| Player	   | Stores information about a player in a game |
| Game	       | Stores details about a game |
| Squad        | Stores details about squads in within a game |
| Kill         | Stores details about kills made in a game |
| Location     | Stores locations, refering to either players or missions on the game map |
| Mission      | Stores missions for games |
| Rule         | Stores game rules |
| Conversation | Stores conversations existing in a game |
| Message      | Stores messages sent from players in conversations |

## Getting Started
### Prerequisities
- SQL Server Management Studio (SSMS)
- Visual Studio 2022
- Keycloak identity provider

### Installation and Setup
1. Clone the repository
2. Configure the `appsettings.json` file in source. Make a "TokenSecrets" section within the file:

```
"TokenSecrets": {
    "KeyURI": "Your Key URI",
    "IssuerURI": "Your Issuer"
}
```

- The "KeyURI" is the URL for retrieving the authentication keys
- The "IssuerURI" is the URL of the authentication server or identity provider

Replace the values with the actual configuration details, as they are needed for authentication and security.

3. In `appsettings.json` make a "ConnectionStrings" section:

```
  "ConnectionStrings": {
    "HvZDb": "Your database server"
  }
```

Change the "HvZDb" data source to your SQL server name.

4. Type in the Package Manager Console:

`add-migration initialDb`, then
`update-database`

This will create the database with dummy data from `HvZDbContext.cs` in SSMS.

## Usage 
The API can be used to interact with the database through HTTP requests. There are different endpoints for the database tables that can be used perform GET, POST or PUT requests from the frontend. 

## Contributors
- [Tommy Jåvold](https://github.com/t-lined)
- [Ine Mari Bredesen](https://github.com/inemari)
- [Tobias Vetrhus](https://github.com/TobiasVetrhus)
- [Ritwaan Hashi](https://github.com/Ritwaan)
- [Noah Høgstøl](https://github.com/Nuuah)
