# Logging Reader Test

### This Project centralize, Logging Reader UI and API within a simple docker compose to easily run locally

### Instalation Proccess

***First at all, make sure you have Docker installed***

 - Clone this project inside you machine
 - Run the following command: `docker compose up`
 - UI App should be available on [UI link](http://localhost:8080/)
 - API App should be available on [API link](http://localhost:5160/swagger/)


### UI Project

This project uses:

  - React JS 18 [link](https://pt-br.reactjs.org/)
  - Vite [link](https://vitejs.dev/)
  - React Testing Library [link](https://testing-library.com/docs/react-testing-library/intro/)
  - Zustand [link](https://github.com/pmndrs/zustand)

### API Project

This project uses:

  - NET 7 [link](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
  - MongoDB Driver [link](https://www.mongodb.com/docs/drivers/csharp/)
  - XUnit [link](https://xunit.net/)
  - Swagger [link](https://swagger.io/)


### Usage
 
 This app is very simple, when you click on "Persist Logs". It parses all log entries and insert all on MongoDB (about 85k of logs)
 When you click on "Search Logs", you can filter by any field is available on screen.
 You can also can click on "Load More" button to lee more logs (All logs are paginated)

![image](https://user-images.githubusercontent.com/36234150/208799265-97e73737-78ec-47d5-9b17-73fef2283a63.png)
