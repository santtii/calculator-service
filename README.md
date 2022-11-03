# Calculator Service

## How to build and deploy

Steps to build and deploy the calculator service

1. git clone https://github.com/santtii/calculator-service.git
2. cd calculator-service/
3. docker-compose up -d --build

## How to run the application

The solution is based on two docker containers, **calculator.server** and **calculator.client**, and it will be necessary to be attached to the **calculator.client's** interactive terminal in order to test the application:

- docker attach calculator-client

## Logging

Application logs are saved in the **/var/log/** directory of each container, run the following command to connect to the server:

- docker exec -it calculator-server /bin/bash
