# RequestManager

## Dependencies

There are a couple of NuGet dependencies in the individual project files and all of the "packages.config" NuGet configuration files are included in the package, so for the first
time the solution files are opened it should be automatically download these dependencies. These dependencies are mainly the following list;

- Microsoft.Unity
- NUnit Unit Testing Framework
- Moq Mocking Interface

## Methodology

Built-in WebRequest class is wrapped with a proxy class which utilizes some useful functionalities. A simple finite state machine is implemented and deployed for parsing purposes. 

### State Machine for HTTP Request Parser

![alt tag](https://raw.githubusercontent.com/hakanarici/RequestManager/master/FSM.png)
