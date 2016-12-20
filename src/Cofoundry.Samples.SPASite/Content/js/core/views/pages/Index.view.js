(function (pages, collectionViews, components, collections, app, $, _, Backbone) {
    pages.Index = Backbone.View.extend({
        el : 'main',
        events : {

        },
        initialize : function() {
            this.catsView = new collectionViews.Cats();
        },
        render : function() {
            this.$('.container').append(this.catsView.render().el);
            return;
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