(function (pages, itemViews, models, app, $, _, Backbone) {
    pages.CatDetails = Backbone.View.extend({
        tagName: 'div',
        className: 'container content-block',
        template: _.template($('#catDetails').html()),
        events: {
            'click .btn-love': 'handleLike'
        },

        like: true,
        auth: false,

        initialize : function() {
            this.listenTo(this.model, 'change', this.render);
        },
        render : function() {
            this.$el.html(this.template(this.model.toJSON()));

            if (this.auth === false) {
                this.checkAuth();
            } else {
                this.showLoveButton();
            }
            
            return this;
        },
        checkAuth: function() {
            if (app.User.get('authenticated') === true) this.showLoveButton();

            this.auth = true;

            var favs = app.User.get('favourites'),
                id = this.model.get('catId');

            _.each(favs, function(fav) {
                if (id === fav.catId) this.setLikeState();
            }, this);
        },
        showLoveButton: function() {
            var $button = this.$el.find('.btn-love');

            this.$el.find('.btn-love').removeClass('hidden');

            if (this.like === true && $button.hasClass('unlike') ||
                this.like === false && !$button.hasClass('unlike')) {
                this.changeButton();
            }
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
                that.model.fetch({
                    success: function (collection, response, options) {
                        that.setLikeState();
                    },
                    error: function (collection, response, options) {
                        console.log('error', response);
                    }
                });
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