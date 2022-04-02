ACOC.Barista
<h2>A cup of coffee</h2>
A cup of coffee is a hobby project to learn and implement ideas. The concept is at large application with a microservice pattern where a user can order an virtual cup of coffee to share with a friend. 

<h2>Barista</h2>
This is the barista-api. An administrator can configurate products in a a database. These configurations are used as templates when a user orders a product.

<h3>Webhooks</h3>
The heart of the barista is the servicebus. Every cup of coffee has a set of freely configured events wich all will trigger at a configured timespan. Every order holds a webhook-callback address wich will be posted to when the events are triggered. 

<h3>Key concepts</h3>
.NET 6, OpenApi, Swagger, MongoDb, Azure Servicebus, Azure Logical Apps. 
