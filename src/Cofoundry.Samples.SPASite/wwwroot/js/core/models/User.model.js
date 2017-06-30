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

                return true;
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