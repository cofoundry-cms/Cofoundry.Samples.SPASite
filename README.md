# Cofoundry.Samples.SPASite

An example demonstrating how to use Cofoundry to create a SPA (Single Page Application) with WebApi endpoints as well as demonstrating some advanced Cofoundry features.

The application is also separated into two projects demonstrating an example of how you might structure your domain layer to keep this separate from your web layer.

Notable features include:

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

1. Run `InitData\Init.sql` script against your db to populate some initial cats and features
2. Copy the images from *"\InitData\Images"* to *"\src\Cofoundry.Samples.SPASite\App_Data\Files\Images"*
3. Either restart the site, or go to the *settings* module in the admin panel and clear the cache.

## App Overview

*SPA Cats* is a simple site that let's you browse and rate cats. Cat data is entered into the Cofoundry CMS, which is then displayed on the homepage. People can browse the data, register as a member and vote for their favorite cat.

### Managing Content

To manage content you'll need to log in to the admin panel by navigating to **/admin**.

#### Adding New Cats

![Domain solution structure](https://github.com/cofoundry-cms/Cofoundry.Samples.SPASite/blob/master/readme/AdminCatList.png)

Once logged in, navigate to **Content > Cats**. This section is generated automatically based on the [`CatCustomEntityDefinition`](https://github.com/cofoundry-cms/Cofoundry.Samples.SPASite/blob/master/src/Cofoundry.Samples.SPASite.Domain/Domain/Cats/Definition/CatCustomEntityDefinition.cs) class in the Domain project. Click on the **Create** button to add a new Cat.

![Domain solution structure](https://github.com/cofoundry-cms/Cofoundry.Samples.SPASite/blob/master/readme/AdminCatCreate.png)

The data entry form is generated based on the [`CatDataModel`](https://github.com/cofoundry-cms/Cofoundry.Samples.SPASite/blob/master/src/Cofoundry.Samples.SPASite.Domain/Domain/Cats/Definition/CatDataModel.cs), a simple class with annotated properties. The Cat custom entity type has versioning enabled, so we can either save the new cat as a draft version or make it live by publishing it.

The two other custom entities *Breeds* and *Features* can be managed in the same way.

### Code

In this example we demonstrate separating your application into two projects to represent two distinct layers. For a simpler example which keeps all files in one project see [Cofoundry.Samples.SimpleSite](https://github.com/cofoundry-cms/Cofoundry.Samples.SimpleSite).

#### Cofoundry.Samples.SpaSite.Domain

Contains domain logic and data access.

![Domain solution structure](https://github.com/cofoundry-cms/Cofoundry.Samples.SPASite/blob/master/readme/SpaCatsDomain.png)

-  **Data:** We use some custom sql tables to store cat popularity data. An Entity Framework DbContext is used to access the custom tables, which demonstrates integrating custom sql tables with Cofoundry sql tables.
- **Domain:** The domain contains all the models, [queries and commands](https://github.com/cofoundry-cms/cofoundry/wiki/CQS) that we use to retrieve and store data. It also contains the [Custom Entity Definitions](https://github.com/cofoundry-cms/cofoundry/wiki/Custom-Entities) that define the *Breed*, *Cat* and *Features* custom entities, and the [User Area Definition](https://github.com/cofoundry-cms/cofoundry/wiki/User-Areas) that defines the *Members* login area. Structuring our code in this way gives us a clean separation between our domain logic layer and our application layer.
- **Install:** Here we take advantage of the [Auto Update](https://github.com/cofoundry-cms/cofoundry/wiki/Auto-Update) feature in Cofoundry to run sql scripts that create our custom tables and stored procedures.
- **MailTemplates:** We store our [mail templates](https://github.com/cofoundry-cms/cofoundry/wiki/Mail) in the domain layer so they can be used from inside our commands. Because we are including the template cshtml files as embedded resources here, we need to include an `AssemblyResourceRegistration` which is located in the bootstrap folder. 

#### Cofoundry.Samples.SpaSite.Web

Because all our logic is in the domain layer, our website project is fairly simple.

![Domain solution structure](https://github.com/cofoundry-cms/Cofoundry.Samples.SPASite/blob/master/readme/SpaCatsWeb.png)

- **Api:** Contains our web api controllers. This are quite lightweight and mostly just wrap out domain queries and commands.
- **App_Start:** [Cofoundry startup and registration](https://github.com/cofoundry-cms/cofoundry/wiki/Website-Startup) is handled via an owin *startup.cs* file. We also register some [routes](https://github.com/cofoundry-cms/cofoundry/wiki/Routing) which simply point to the home controller because our SPA app handles the routing on the client side.
- **Content:** SPA Cats is written in [backbone.js](http://backbonejs.org/) to keep it simple, but you could use any framework you like. The css is compiled from sass. The front end build uses the grunt file in *\src\Cofoundry.Samples.SPASite\grunt*.
- **Controllers/Models/Views:** The app runs entirely in JavaScript, so the home controller simply passes some basic user information and the XSRFToken through to the view, which is then rendered as a simple js state object that can be read by backbone.