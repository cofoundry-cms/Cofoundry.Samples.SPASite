(function (collectionViews, itemViews, collections, app, $, _, Backbone) {
    collectionViews.Cats = Backbone.View.extend({
        tagName: 'div',
        className: 'row',

        initialize : function() {
            var self = this;

            this.collection = new collections.Cats();
            this.collection.bind('reset', _.bind(this.render, this));
            this.collection.fetch({ 
                success: function () { 
                    self.render();
                } 
            });

            this.render();
        },

        render : function() {
            this.collection.each(function(cat) {
                var catView = new itemViews.Cat({ model: cat });
                this.$el.append(catView.render().el);
            }, this);

            return this;
        }
    });
})(
    CofoundrySPA.CollectionViews = CofoundrySPA.CollectionViews || {},
    CofoundrySPA.ItemViews,
    CofoundrySPA.Collections,
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);