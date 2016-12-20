(function (itemViews, models, app, $, _, Backbone) {
    itemViews.CatDetails = Backbone.View.extend({
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
    CofoundrySPA.ItemViews = CofoundrySPA.ItemViews || {},
    CofoundrySPA.Models,
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);