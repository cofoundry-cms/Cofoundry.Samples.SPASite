(function (models, app, $, _, Backbone) {
    models.Register = Backbone.Model.extend({
        url: '/api/auth/register',
        defaults: {
            name: '',
            surname: '',
            email: '',
            password: ''
        },
        parse: function(response) {
            
        }
    });
})(
    CofoundrySPA.Models = CofoundrySPA.Models || {},
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);