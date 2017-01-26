(function (pages, itemViews, models, app, $, _, Backbone) {
    pages.CatDetails = Backbone.View.extend({
        el : 'main',
        tagName: 'div',
        className: 'row',
        template: _.template($('#catDetails').html()),
        events: {
            'click .btn-love': 'handleLike'
        },

        like: true,

        initialize : function() {
            this.render();
            this.listenTo(this.model, 'change', this.setLikeState);
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
                if (id === fav.catId) this.setLikeState();
            }, this);
        },
        showLoveButton: function() {
            this.$el.find('.btn-love').removeClass('hidden');
        },
        changeButton: function() {
            var $button = this.$el.find('.btn-love'),
                catName = this.model.get('name');

            if ($button.hasClass('unlike')) {
                $button.html('I <span class="glyphicon glyphicon-heart"></span> ' + catName).removeClass('unlike'); 
            } else {
                $button.html('UN-<span class="glyphicon glyphicon-heart"></span> ' + catName).addClass('unlike');
            }
        },
        setLikeState: function() {
            this.like = !this.like;
            this.changeButton();

            return this;
        },
        handleLike: function() {
            var id = this.model.get('catId'),
                url = '/api/cats/' + id + '/likes',
                type = 'POST',
                that = this;

            if (this.like === false) type = 'DELETE';
            
            $.ajax({
                url: url,
                type: type
            })
            .done(function( data, response ) {
                that.model.fetch();
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