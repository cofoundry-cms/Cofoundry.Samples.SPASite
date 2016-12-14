(function (collections, models, app, $, _, Backbone) {
    collections.Cats = Backbone.Collection.extend({
        model: models.Cat,

        url: '/api/cats',

        parse: function(response) {
            console.log(response);
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