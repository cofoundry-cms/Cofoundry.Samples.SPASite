// We have written all our lovely classes and whatnot, now let's initialise the application.
(function(app, $, _, Backbone) {

    $(function() {
        // Initialize Backbone router and global views
        app.router   = new app.Router();
        app.siteView = new app.SiteView();

        $(window).load(function (e) {
            app.Events.trigger('app loaded', e);
        });

        app.router.on('route:index', function() {
            app.siteView.setCurrentPage(new CofoundrySPA.PageViews.Index());
        });

        app.router.on('route:details', function () {
            app.siteView.setCurrentPage(new CofoundrySPA.PageViews.Details());
        });

        app.router.on('route:login', function (urlslug) {
            app.siteView.setCurrentPage(new CofoundrySPA.PageViews.Login());
        });

        app.router.on('route:register', function () {
            app.siteView.setCurrentPage(new CofoundrySPA.PageViews.Register());
        });

        // Start router
        Backbone.history.start({pushState:false, hashChange: false});

        // rendering the site gets this party started.
        app.siteView.render();
    });
})(
    CofoundrySPA.App,
    jQuery,
    _,
    Backbone
);
