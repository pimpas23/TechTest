# TechTest

This is a simple tech test for a job application. The task is to create a simple web application that allows users to add data in csv format with specific fields (see example csv) and query the database to get some information.

To run this project, you need to have docker and docker-compose and make installed on your machine.

On the project folder run the command "make infra" to create the container for the application and database and go to "http://localhost:8080/swagger/index.html" to interact with the application with swagger.

To run the tests, run the command "make unit-tests".

