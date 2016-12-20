(function (pages, itemViews, models, app, $, _, Backbone) {
    pages.CatDetails = Backbone.View.extend({
        el : 'main',
        tagName: 'div',
        className: 'row',
        template: _.template($('#catDetails').html()),

        initialize : function() {
            this.render();
        },
        render : function() {
            this.$el.html(this.template(this.model.toJSON()));

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