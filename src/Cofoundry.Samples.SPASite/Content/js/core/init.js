// We have written all our lovely classes and whatnot, now let's initialise the application.
(function(models, app, $, _, Backbone) {
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

        app.router.on('route:details', function(id) {
            var cat = new models.Cat({ id: id });

            cat.fetch().done(_.bind(function() {
                app.siteView.setCurrentPage(new CofoundrySPA.PageViews.CatDetails({ model: cat }));
            }, this)); 
        });

        app.router.on('route:login', function(urlslug) {
            app.siteView.setCurrentPage(new CofoundrySPA.PageViews.Login());
        });

        app.router.on('route:register', function() {
            app.siteView.setCurrentPage(new CofoundrySPA.PageViews.Register());
        });

        // Start router
        Backbone.history.start({pushState: true, hashChange: false});

        // rendering the site gets this party started.
        app.siteView.render();
    });
})(
    CofoundrySPA.Models,
    CofoundrySPA.App,
    jQuery,
    _,
    Backbone
);
