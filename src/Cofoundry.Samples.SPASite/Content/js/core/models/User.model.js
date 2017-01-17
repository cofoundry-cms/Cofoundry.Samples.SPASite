(function (models, app, $, _, Backbone) {
    models.User = Backbone.Model.extend({
        defaults: {
            'authenticated': false,
            'token': null,
            'favourites': []
        },
        getFavourites: function() {
            var url = '/api/users/current/cats/liked',
                that = this;

            $.ajax({
                url: url,
                type: 'GET'
            }).done(function(data) {
                that.set({favourites: data.data});
            });
        }
    });
})(
    CofoundrySPA.Models = CofoundrySPA.Models || {},
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);