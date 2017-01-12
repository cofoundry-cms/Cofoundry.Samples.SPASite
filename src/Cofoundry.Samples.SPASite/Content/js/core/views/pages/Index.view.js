(function (pages, collectionViews, components, collections, app, $, _, Backbone) {
    pages.Index = Backbone.View.extend({
        el : 'main',
        template: _.template($('#indexTemplate').html()),

        initialize : function() {
            this.catsView = new collectionViews.Cats();
            this.checkAuth();
            this.render();
        },
        render : function() {
            this.$el.empty().append(this.template);

            this.$('.container').append(this.catsView.render().el);
            return;
        },
        checkAuth: function() {
            console.log(app.User.authenticated);
        }
    });
})(
    CofoundrySPA.PageViews = CofoundrySPA.PageViews || {},
    CofoundrySPA.CollectionViews,
    CofoundrySPA.ComponentViews,
    CofoundrySPA.Collections,
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);