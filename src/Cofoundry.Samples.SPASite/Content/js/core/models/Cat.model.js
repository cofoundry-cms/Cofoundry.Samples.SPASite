(function (models, app, $, _, Backbone) {
    models.Cat = Backbone.Model.extend({
        initialize: function(options) {
            console.log(options);
        },
        url: function() {
            if (this.id) {
                var urlstring = 'cats/' + this.id;
                return urlstring;
            }
        }
    });
})(
    CofoundrySPA.Models = CofoundrySPA.Models || {},
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);