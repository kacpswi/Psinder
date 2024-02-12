# Psinder - Backend API

## Project Overview and Description

The Psinder project aims to provide a backend API for an internet application facilitating pet adoption. The implementation is based on technologies such as .NET, ASP.NET Core, Entity Framework, Linq, Identity ASP.NET Core, and Swagger.

## Project Details

The API allows user registration with authentication and authorization using JWT tokens. The system incorporates various user roles, including User, ShelterOwner, ShelterWorker, and Admin, each with specific permissions.

- Unauthenticated users can only browse shelters and animals in the system.
- Users with the User role, obtained upon account creation, can additionally like animals for adoption and send messages to shelter owners and workers.
- The ShelterWorker role allows adding and editing animals in the shelter and responding to user messages.
- The ShelterOwner role further grants the ability to add and edit shelters in the system.
- Admins have privileges to view detailed user information and change their roles.

The SQL Server-based database includes tables for Shelters, Animals, Messages, and a Likes table linking users with animals.

## API Endpoints

### Account Controller

- `POST /api/account/register` - user registration, accessible to all
- `POST /api/account/login` - user login, accessible to all, returns a JWT token

### Admin Controller

- `GET /api/admin/users-with-roles` - accessible only to Admin, returns users and their roles
- `POST /api/admin/edit-roles/{userId}` - accessible only to Admin, edits user roles
- `GET /api/admin/users` - accessible only to Admin, displays detailed user information with pagination

### Animal Controller

- `GET /api/shelter/{shelterId}/animal/{animalId}` - accessible to all, returns information about an animal in a shelter
- `DELETE /api/shelter/{shelterId}/animal/{animalId}` - accessible to ShelterOwner and ShelterWorker, deletes an animal from the shelter
- `PUT /api/shelter/{shelterId}/animal/{animalId}` - accessible to ShelterOwner and ShelterWorker, edits information about an animal
- `GET /api/shelter/{shelterId}/animals` - accessible to all, returns animals in a specific shelter
- `GET /api/animals` - accessible to all, returns animals in the system with pagination
- `POST /api/shelter/{shelterId}/animal` - accessible to ShelterOwner and ShelterWorker, adds an animal to the shelter

### Like Controller

- `POST /api/like/{animalId}` - accessible to logged-in users, likes a selected animal
- `GET /api/like` - accessible to logged-in users, returns a list of currently liked animals

### Message Controller

- `POST /api/messages` - accessible to logged-in users, sends a message to another user
- `GET /api/messages` - accessible to logged-in users, returns received and sent messages
- `DELETE /api/messages/{messageId}` - accessible to logged-in users, deletes a message

### Shelter Controller

- `GET /api/shelter/{id}` - accessible to all, returns information about a selected shelter
- `DELETE /api/shelter/{id}` - accessible to ShelterOwner, deletes a shelter
- `PUT /api/shelter/{id}` - accessible to ShelterOwner, edits a selected shelter
- `GET /api/shelter` - accessible to all, returns shelters with pagination
- `POST /api/shelter` - accessible to ShelterOwner, creates a new shelter

## Database Schema

![Database Schema](https://github.com/kacpswi/Psinder/assets/92365683/aa4d5280-7ed9-43bc-832d-9405dac271ff)


