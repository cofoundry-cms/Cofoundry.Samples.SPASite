(function (collections, models, app, $, _, Backbone) {
    collections.Cats = Backbone.Collection.extend({
        model: models.Cat,

        url: '/api/cats',

        parse: function(response) {
            var cats = response.data.items;

            return cats;
        }
    });
})(
    CofoundrySPA.Collections = CofoundrySPA.Collections || {},
    CofoundrySPA.Models,
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);