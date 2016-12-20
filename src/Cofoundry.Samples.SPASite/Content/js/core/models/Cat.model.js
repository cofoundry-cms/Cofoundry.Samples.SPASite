(function (models, app, $, _, Backbone) {
    models.Cat = Backbone.Model.extend({
        initialize: function(options) {},
        url: function() {
            if (this.id) {
                var urlstring = '/api/cats/' + this.id;
                return urlstring;
            }
        },
        parse: function(response) {
            var parsedResponse = response;

            if (this.id) {
                parsedResponse = response.data;
            }

            return parsedResponse;
        }
    });
})(
    CofoundrySPA.Models = CofoundrySPA.Models || {},
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);