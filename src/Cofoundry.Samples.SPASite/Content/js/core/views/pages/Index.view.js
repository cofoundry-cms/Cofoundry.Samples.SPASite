(function (pages, collectionViews, components, collections, app, $, _, Backbone) {
    pages.Index = Backbone.View.extend({
        el : 'main',
        template: _.template($('#indexTemplate').html()),
        events : {

        },
        initialize : function() {
            this.catsView = new collectionViews.Cats();
        },
        render : function() {
            this.$el.append(this.template);

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