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

            this.checkAuth();
            return this;
        },
        checkAuth: function() {
            console.log(app.User.authenticated);
            if (app.User.authenticated === true) this.showLoveButton();
        },
        showLoveButton: function() {
            this.$el.find('.btn-love').removeClass('hidden');
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