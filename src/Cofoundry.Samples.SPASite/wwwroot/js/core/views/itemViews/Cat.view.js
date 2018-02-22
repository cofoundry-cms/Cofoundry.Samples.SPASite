(function (itemViews, models, app, $, _, Backbone) {
    itemViews.Cat = Backbone.View.extend({
        tagName: 'div',
        className: 'col-sm-3 cat',
        template: _.template($('#catItem').html()),
        events: {
            'click': 'goToCat'
        },

        render : function() {
            this.$el.html(this.template(this.model.toJSON()));

            return this;
        },
        goToCat: function(e) {
            e.preventDefault();

            var id = this.model.get('catId');

            app.router.navigate('/cat/' + id, {trigger: true})
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