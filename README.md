# Cofoundry.Samples.SPASite

An example demonstrating how to use Cofoundry to create a SPA (Single Page Application) with WebApi endpoints as well as demonstrating some advanced Cofoundry features.

The application is also separated into two projects demonstrating an example of how you might structure your domain layer to keep this separate from your web layer.

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

#### To get started:

1. Create a database named 'Cofoundry.Samples.SPASite' and check the Cofoundry connection string in the web.config file is correct for you sql server instance
2. Run the website and navigate to *"/admin"*, which will display the setup screen
3. Enter an application name and setup your user account. Submit the form to complete the site setup. 
4. Either log in and enter your own data or follow the steps below to import some test data

#### Importing test data:

To get you started we've put together some optional test data:

1. Run `InitData\Init.sql` script against your db to populate some initial
2. Copy the images from *"\InitData\Images"* to *"\src\Cofoundry.Samples.SPASite\App_Data\Files\Images"*
3. Restart the site. This is required to break the cache, but there will be an option in the UI to do this soon (see [issue #40](https://github.com/cofoundry-cms/cofoundry/issues/40))