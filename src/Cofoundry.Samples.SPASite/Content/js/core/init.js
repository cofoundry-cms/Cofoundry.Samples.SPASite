// We have written all our lovely classes and whatnot, now let's initialise the application.
(function(models, app, $, _, Backbone, helper) {
    $(function() {
        // Global user profile
        app.User = new models.User();

        // Initialize Backbone router and global views
        app.router   = new app.Router();
        app.siteView = new app.SiteView();

        // Checks if user is logged in and if true sets the user data
        helper.prefilter(SPACatsState.csrfToken);

        if (SPACatsState.isLoggedIn === true) {
            app.User.set({authenticated: true, token: SPACatsState.csrfToken});
            app.User.getFavourites();
        }

        $(window).load(function (e) {
            app.Events.trigger('app loaded', e);
        });

        // Page route functions
        app.router.on('route:index', function() {
            app.siteView.setCurrentPage(new CofoundrySPA.PageViews.Index());
        });

        app.router.on('route:details', function(id) {
            app.siteView.setCurrentPage(new CofoundrySPA.PageViews.CatDetails({ id: id }));
        });

        app.router.on('route:login', function(urlslug) {
            app.siteView.setCurrentPage(new CofoundrySPA.PageViews.Login());
        });

        app.router.on('route:register', function() {
            app.siteView.setCurrentPage(new CofoundrySPA.PageViews.Register());
        });

        // Start router
        Backbone.history.start({pushState: true});

        // rendering the site gets this party started.
        app.siteView.render();
    });
})(
    CofoundrySPA.Models,
    CofoundrySPA.App,
    jQuery,
    _,
    Backbone,
    Helper
);
