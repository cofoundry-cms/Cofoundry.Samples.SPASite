# Cofoundry.Samples.SPASite

An example demonstrating how to use Cofoundry to create a SPA (Single Page Application) with WebApi endpoints as well as demonstrating some advanced Cofoundry features.

The application is also separated into two projects demonstrating an example of how you might structure your domain layer to keep this layer separate from your web layer.

- Startup registration
- Route Registration
- Web Api and use of `IApiResponseHelper`
- Structuring commands and queries using CQS 
- Multiple related custom entities - Cats, Breeds and Features
- A member area with a sign-up and login process
- Using an Entity Framework DbContext to represent custom database tables
- Executing stored procedures using `IEntityFrameworkSqlExecutor`
- Integrating custom entity data with Entity Framework data access
- Using the auto-updater to run sql scripts
- Email notifications & Email Templating
- Registering services with the DI container

To get started:

1. Create a database named 'Cofoundry.Samples.SPASite' and check the Cofoundry connection string in the web.config file is correct for you sql server instance
2. Run the website which should display the Setup screen, but don't enter any information yet.
3. Run `InitData/Init.sql` script against your db
4. On the setup screen enter a website name and your default credentials. Submit the form to complete the site setup
