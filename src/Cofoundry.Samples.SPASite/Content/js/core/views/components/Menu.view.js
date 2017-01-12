(function (components, app, $, _, Backbone) {
    components.Menu = Backbone.View.extend({
        el : '.navbar',
        events : {
            'click .logo': 'goHome',
            'click .navbar__link': 'linkNavigate'
        },
        initialize: function () {

        },
        goHome: function(e) {
            e.preventDefault();
            app.router.navigate('', {trigger: true});
        },
        linkNavigate: function(e) {
            e.preventDefault();

            var url = e.target.pathname;

            app.router.navigate(url, {trigger: true});
        }
    });
})(
    CofoundrySPA.ComponentViews = CofoundrySPA.ComponentViews || {},
    CofoundrySPA.App,
    jQuery, 
    _, 
    Backbone
);        