# M4WS-sample
Sample Angular/.NET Core app which consumes Mago4 Desktop Web Services

## Prerequisites
* .NET Core SDK 2.1
* Node.js 10.15
* Visual Studio Code
* C# extension for VS Code (OmniSharp)

## Startup
* Clone the repository and open it with VS Code.
* Accept the "restore" action offered by OmniSharp extension.
* Open a terminal window and install dependencies:
```
npm install
```
* Build the Angular frontend:
```
npm run build
```
* Build the .NET core backend:
```
dotnet build
```
* update the `appsettings.json` file to match your Mago4 configuration:
```json
"ConnectInfo": {
    "Server": "localhost",
    "Instance": "Mago4"
  }
```

## Usage
* open a command prompt and move to the repository folder
* launch the application server:
```
dotnet run
```
* open a browser and navigate to `localhost:5000`

In the upper-right corner an icon shows the status of the connection with the Mago4 server. In case of trouble, an error message is displayed.

Entering an user name, the combobox populates with the companies allowed for that user.
