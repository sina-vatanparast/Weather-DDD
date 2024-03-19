## DDD Weather Domain

Business Requirements: 
- As a weatherman, I want to store weather forecasts per day. 
- As a user, I want to retrieve the weather forecast for a week in a human-readable way (like
	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"). - Input forecasts cannot be in the past. 
- Temperature cannot be more than +60 and cannot be less than -60 degrees.

### Prerequisites to run the projects locally:
- Docker Desktop
- Replacing connection strings in API projects, appsettings.json file with the values I have sent to you as a file by email.
- Whitelisting your IP addresses in Azure, which I will do that as long as I receive your IPs to communicate with Azure SQL database and Service Bus.

### Solution Description:

- **Bounded Contexts:** I found 2 Bounded Contexts in this domain. One for the weatherman creating weather forecast day by day and another, the public user requesting weather forecast (for a week in this problem domain)
- **Aggregates:** Each Bounded Context has only one Aggregate with one Aggregate Root
- **Micro Level Architecture:** Onion Architecture
- **Solution Level Architecture:** Microservices. each microservice represents a bounded context
- **Communication Strategy:** Async communication provided by Azure Service Bus Queue since I needed point-to-point messaging with the guarantee of at least/most one-time receiving with duplicate detection feature and dead-lettering for poison messages. Aggregate Root is responsible for creating events while the Infrastructure is in charge of sending them.
In the sender application (Weatherman), the sending mechanism is implemented in the DbContext after successfully persisting data in the Database.
In the recipient, the Infrastructure is receiving the messages coming from the queue in the Azure Service Bus. To keep it simple, I have configured a namespace and a queue in Azure Portal to skip this responsibility in the runtime.
- **UI**: I have benefited from Swagger to make it easy for the user to call the APIs in both applications.
- **DataBase:** There are 2 SQL DBs in Azure one from Weatherman and another, for Weatherforcast public. These DBs are serverless so I hope you do not mind the Cold Start. The first request is likely to face TimeOut due to this reason.
- **Shared Kernel:** This a shared set of classes I have made it available on **NuGet Gallery** to simply add it as a package to both Bounded Contexts.
- **Exception Handling:** I have used different means of handling exceptions depending on the layer and nature of them. Some of them are only logged, some are handled by middleware, and those related to poison messages are handled by dead-lettering the message.
- **Unit Testing**: provided for aggregates in a separate project.
- **Containerized** containers running on the Docker engine.


### Imaginary Conversation With the domain expert
Since I had no communication with the domain expert, I made an imaginary one that I would avoid in the real world. Here are the list of questions and answers:
1) *Any authentication needed for weatherman? or public API?*
No, you can go ahead with Public APs.
2) *In case of calling the API twice for the same date, should the system(weatherman) update the temperature or raise an exception?*
We can simply update the temperature for that day.
3) *Does Weatherman support batch inputs?* 
Not for now, we can implement it in the future.
4) *Temperature inputs can be in both Celsius and Fahrenheit?* 
Currently, we only expect numbers in Celsius.
5) *Temperature is only int type or can be double?*
Integer would suffice.
6) *Creating today's forecast is possible?* 
Yes, only past days are forbidden.

### To Improve
If I had more time, I intend to make some improvements to my current solution which I can mention some of them:
- More Unit Tests to cover all parts of the application
- Implementing an Azure function triggered by time(every midnight, preferably) and using SQL binding to delete weather forecasts for past days, at least in the Public Weather Forcast database.
- Implementing a retry mechanism dealing with transient faults for messaging on both sides in case of network errors, Internal Server Errors in the Service Bus, Throttling Exceptions, and so on.
- Think much more about possible exceptions in general, predict and handle them.
