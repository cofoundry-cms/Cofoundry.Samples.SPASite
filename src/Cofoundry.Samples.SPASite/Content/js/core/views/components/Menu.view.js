(function (components, app, $, _, Backbone) {
    components.Menu = Backbone.View.extend({
        el : '.navbar',
        events : {
            'click .logo': 'goHome',
            'click .navbar__link': 'linkNavigate'
        },
        initialize: function() {
            this.$unAuthLinks = this.$el.find('.unauth');
            this.listenTo(app.User, 'change', this.checkAuthState);
        },
        goHome: function(e) {
            e.preventDefault();
            app.router.navigate('', {trigger: true});
        },
        linkNavigate: function(e) {
            if (e.target.tagName === 'BUTTON') return;

            e.preventDefault();

            var url = e.target.pathname;

            app.router.navigate(url, {trigger: true});
        },
        checkAuthState: function() {
            var userAuth = app.User.get('authenticated');

            if (userAuth === true) {
                this.setMenuAuthenticated();
            }
            else {
                this.setMenuUnauthenticated();
            }
        },
        setMenuAuthenticated: function() {
            _.each(this.$unAuthLinks, function(link) {
                $(link).addClass('hidden');
            });

            this.$el.find('.auth').removeClass('hidden');
        },
        setMenuUnauthenticated: function() {
            _.each(this.$unAuthLinks, function(link) {
                $(link).removeClass('hidden');
            });

            this.$el.find('.auth').addClass('hidden');
        }
    });
})(
    CofoundrySPA.ComponentViews = CofoundrySPA.ComponentViews || {},
    CofoundrySPA.App,
    jQuery, 
    _, 
    Backbone
);        