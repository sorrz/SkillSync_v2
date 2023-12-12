
SkillSync v.2       :computer: Coders :nerd_face: Dan Stjärnborg, Johan Holm, Delia Grenstadius
<br>
---------------------------------------------------

## **Project Overview**

SkillSync v.2 is a web application designed to serve as a matching tool connecting students from TUC Systemutveckling with companies interested in hosting LIA students and potentially hiring recent graduates. The project is a collaboration between developers Dan Stjärnborg, Johan Holm, and Delia Grenstadius.

### **Description**

The goal of SkillSync v.2 is to create an effective platform for facilitating connections between TUC students and companies seeking LIA students or looking to hire newly examined software developers or software engineers.

### **Technology Stack**

- **Frontend:**
    - Vite
    - React with TypeScript
- **Backend:**
    - C#
- **Database:**
    - SQLite (for development, to be replaced with SQL Server in production)

### **Development Approach**

In this project, we follow Test-Driven Development (TDD) practices, ensuring a robust and well-tested codebase.

## **Getting Started**

To set up the SkillSync v.2 project locally, follow the instructions below:

### **Backend**

1. Clone the repository:
    
    ```bash
    
    git clone https://github.com/sorrz/SkillSync_v2.git
    
    ```
    
2. Navigate to the Backend directory:
    
    ```bash
    
    cd SkillSync_v2/Backend
    
    ```
    
3. Open the solution in your preferred C# IDE (e.g., Visual Studio).
4. Set up the SQLite database:
    - Update the connection string in the **`appsettings.json`** with the following code:
   ```bash
    
    "ConnectionStrings": {
        "DefaultConnection" : "Data Source=SkillSync_v2Api.db"
      }
    
    ```
    - Run migrations to create the database schema.
      
   ```bash
      
    Add-Migration InitialCreate
   
    ```
    then update:
   ```bash
    
    Update-Database
    
    ```
    
6. Run the backend application.

### **Frontend**

1. Navigate to the Frontend directory:
    
    ```bash
    
    cd SkillSync_v2/Frontend
    
    ```
    
2. Install dependencies:
    
    ```bash
    
    npm install
    
    ```
    
3. Run the frontend application:
    
    ```bash
    
    npm run dev
    
    ```
    
4. After running the command, check the terminal for the localhost address. Open your browser and visit the provided address (usually**`http://localhost:3000`** to access the SkillSync v.2 application.

### **Running Tests**

### Backend

Run the following command to execute backend tests:

```bash

dotnet test

```
Here we have included Coverage Report:
![image](https://github.com/sorrz/SkillSync_v2/assets/27415422/759c042a-dfcc-4b6a-99f9-f0b60435f324)


### Frontend

Run the following command for frontend tests:

```bash

npm run test

```

### **Project Structure**

- **Backend:**
    - The **`Backend`** directory contains the C# backend application.
- **Frontend:**
    - The **`Frontend`** directory holds the Vite and React TypeScript frontend application.
- **Logotype:**
    - Contains styling and logotype-related assets.

### **Issues and Contributions**

If you encounter any issues or have suggestions for improvement, feel free to create an issue or submit a pull request on the GitHub repository.

Happy coding!

