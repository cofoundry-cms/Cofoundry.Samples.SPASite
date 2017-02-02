// We have written all our lovely classes and whatnot, now let's initialise the application.
(function(models, app, $, _, Backbone, helper) {
    $(function() {
        // Global user profile
        app.User = new models.User();

        // Initialize Backbone router and global views
        app.router   = new app.Router();
        app.siteView = new app.SiteView();

        // Checks if user is logged in and if true sets the user data
        helper.prefilter();

        if (SPACatsState.isLoggedIn === true) {
            app.User.set({authenticated: true});
            app.User.getFavourites();
        }

        $(window).load(function (e) {
            app.Events.trigger('app loaded', e);
        });

        // Page route functions
        app.router.on('route:index', function() {
            app.homeView = new CofoundrySPA.PageViews.Index();
            app.siteView.goto(app.homeView);
        });

        app.router.on('route:details', function(id) {
            var cat = new models.Cat({ id: id });

            cat.fetch().done(null, function() {
                app.catView = new CofoundrySPA.PageViews.CatDetails({ model: cat });
                app.siteView.goto(app.catView);
            });
        });

        app.router.on('route:login', function() {
            app.loginView = new CofoundrySPA.PageViews.Login();
            app.siteView.goto(app.loginView);
        });

        app.router.on('route:register', function() {
            app.registerView = new CofoundrySPA.PageViews.Register();
            app.siteView.goto(new CofoundrySPA.PageViews.Register());
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
