# Epicodus Weekly Project | Dr. Sillystringz's Factory

##### Table of Contents
1. [Description](#description)
2. [Objectives](#objectives)
   - [Further Exploration Objectives](#further-exploration-objectives)
3. [Setting Up The Project](#setting-up-the-project)
   - [Setting Up The Database](#setting-up-the-database)
   - [Setting Up The Application](#setting-up-the-application)

## Description


- **Author**: Allister Moon Kays
- **Copyright**: MIT License

Pierre is back! He wants you to create a new application to market his sweet and savory treats. This time, he would like you to build an application with user authentication and a many-to-many relationship. Here are the features he wants in the application:

- The application should have user authentication. A user should be able to log in and log out. Only logged in users should have create, update and delete functionality. All users should be able to have read functionality.
- There should be a many-to-many relationship between Treats and Flavors. A treat can have many flavors (such as sweet, savory, spicy, or creamy) and a flavor can have many treats. For instance, the "sweet" flavor could include chocolate croissants, cheesecake, and so on.
- A user should be able to navigate to a splash page that lists all treats and flavors. Users should be able to click on an individual treat or flavor to see all the treats/flavors that belong to it.

## Objectives
- Does at least one of your classes have all CRUD methods implemented in your app?
- Are you able to view both sides of the many-many relationship? For a particular instance of a class, are you able to view all of the instances of the other class that are related to it?
- Build files and sensitive information is included in .gitignore file and is not to be tracked by Git, and includes instructions on how to create the appsettings.json and set up the project.
- Is the project in a polished, portfolio-quality state?
- Was required functionality in place by the deadline?
- Does the project demonstrate all of this week's concepts? If prompted, are you able to discuss your code with an instructor using correct terminology?

### Further Exploration
- Add all CRUD methods to both classes.
- Add properties to specify if a machine is operational, malfunctioning, or in the process of being repaired.
- Add properties to specify if an engineer is idle, or actively working on repairs.
- Add inspection dates to the machines, or dates of license renewal to the engineers.
- Add a table for incidents, showing which engineer repaired which machine.
- Add a table for locations, and specify which engineers or machines are located at which factory.
- Add styling to give life to the project.

## Setting Up The Project
You are expected to have the following installed on your computer:

- A working bash terminal
- An instance of MySQL 8 or Docker
- .Net 5.0 or greater (https://dotnet.microsoft.com/download)
- Have the global tool `Entity Framework` installed

Before you can begin either set of setup instructions, you must do the following:
1. Download the files or clone the repository to your computer.

### Setting Up The Database
This assumes either an instance of MySQL 8 is installed, or you have Docker available on your computer

#### Using Docker
If you have Docker, then the set up will be quick and easy!

1. Ensure the Docker daemon is currently running on your computer
2. Close any instances of MySQL running
  - Or, change the ports used when running docker for this project. However, you'll be responsible for updating the DB configuration in that case.
3. Open a terminal instance in the root of this project
4. Run `docker compose up` to start the database.

#### Manual Database Setup

1. Ensure MySQL is installed and currently running

#### Setting up table/column data

1. If you do not have `Entity Framework` installed on your computer, open a terminal window and run the following command:
  - `dotnet tool install --global dotnet-ef`
2. Open a terminal window and go into the `Factory` folder in this repo.
3. Run `dotnet ef database update`

### Setting Up The Application

1. The one thing you'll need to do before starting the project is setting up the `appsettings.json` file.
2. Open a terminal window
3. Run `bash scripts/create-appsettings.sh`
  - This will create the necessary `appsettings.json` file with assumed default information, this is:
  - Username is `root`. If you wish to change this, append `-u your-user-name` to the command
  - Password is `epicodus`. If you wish to change this, append `-p your-password` to the command
    - alternatively for no password, append the `-P` flag to the command
  - DB Name is `allister_kays`. If you wish to change this, append `-d database_name` to the command. The database does not need to exist, and any existing data be wiped out when the command runs.
  - An example would be `bash scripts/initialize-db.sh -u otherroot -p mypassword -d mydatabase`.
4. Alternatively, you can fill in the `appsettings.json` file yourself. You can copy the `appsettings.example.json` and rename it, then fill in the values.
5. Now you are ready to run the app
  - To run the application, use `dotnet run` while in the `Factory` directory.
  - Then, open your browser and load the dev server address to see the app. The default address will be `http://localhost:5000`.