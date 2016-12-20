(function (pages, itemViews, models, app, $, _, Backbone) {
    pages.Register = Backbone.View.extend({
        el : 'main',
        template: _.template($('#register').html()),

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