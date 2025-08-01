# playwright-demo-tests
This Playwright test project demonstrates browser automation testing the Snipe-IT demo site. The automated script performs the following steps:
- Login to the snipeit demo at https://demo.snipeitapp.com/login
- Create a new Macbook Pro 13" asset with the ready to deploy status and checked out to a random user
- Find the asset you just created in the assets list to verify it was created successfully
- Navigate to the asset page from the list and validate relevant details from the asset creation
- Validate the details in the "History" tab on the asset page

This project depends on:

- .NET Version 17.14.1
- Playwright Version 1.54.0


# Project Structure
- Pages/ - Contains Page related actions
- Tests/ - Contains Test cases
- Utils/ - Contains Playwright Setup 


# Setup Commands to Run
## 1. Clone the repo
https://github.com/Dineth-dev/snipeit-playwright-demo.git

## 2. Change to .NET project
cd snipeit-playwright-demo
cd SnipeitPlaywrightDemo

## 2. Restore .NET dependencies
dotnet restore

## 3. Install Playwright browsers
dotnet playwright install

## 4. Run tests
dotnet test

