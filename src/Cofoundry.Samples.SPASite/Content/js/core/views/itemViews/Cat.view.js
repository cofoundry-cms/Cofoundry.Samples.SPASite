(function (itemViews, models, app, $, _, Backbone) {
    itemViews.Cat = Backbone.View.extend({
        tagName: 'div',
        className: 'col-sm-3 cat',
        template: _.template($('#catItem').html()),

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