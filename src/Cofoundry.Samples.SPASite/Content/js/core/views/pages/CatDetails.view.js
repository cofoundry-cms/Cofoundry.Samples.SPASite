(function (pages, itemViews, models, app, $, _, Backbone) {
    pages.CatDetails = Backbone.View.extend({
        el : 'main',
        tagName: 'div',
        className: 'row',
        template: _.template($('#catDetails').html()),
        events: {
            'click .btn-love': 'toggleLink'
        },

        initialize : function() {
            this.render();
        },
        render : function() {
            this.$el.empty().html(this.template(this.model.toJSON()));

            this.checkAuth();
            return this;
        },
        checkAuth: function() {
            if (app.User.get('authenticated') === true) this.showLoveButton();

            var favs = app.User.get('favourites'),
                id = this.model.get('catId');

            _.each(favs, function(fav) {
                console.log(id, fav.catId);
                if (id === fav.catId) this.changeButton();
            }, this);
        },
        showLoveButton: function() {
            this.$el.find('.btn-love').removeClass('hidden');
        },
        changeButton: function() {
            console.log('core');

            this.$el.find('.btn-love').html('UN-<span class="glyphicon glyphicon-heart"></span> <%= name %>').addClass('unlike');
        },
        toggleLink: function(e) {
            var id = this.model.get('catId'),
                url = '/api/cats/' + id + '/likes',
                type = 'POST',
                that = this;

            if ($(e.target).hasClass('unlike')) type = 'DELETE';
            
            $.ajax({
                url: url,
                type: type
            })
            .done(function( data ) {
                that.model.fetch();
                that.render();
            });
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