(function (pages, itemViews, models, app, $, _, Backbone) {
    pages.Login = Backbone.View.extend({
        el : 'main',
        template: _.template($('#login').html()),

        initialize : function() {
            this.render();
        },
        render : function() {
            this.$el.empty().append(this.template);

            return this;
        }
    });
})(
    CofoundrySPA.PageViews = CofoundrySPA.PageViews || {},
    CofoundrySPA.ItemViews,
    CofoundrySPA.Models,
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);