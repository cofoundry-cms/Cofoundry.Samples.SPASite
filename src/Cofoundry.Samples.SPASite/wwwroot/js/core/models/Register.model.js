(function (models, app, $, _, Backbone) {
    models.Register = Backbone.Model.extend({
        url: '/api/auth/register',
        defaults: {
            name: '',
            surname: '',
            email: '',
            password: ''
        }
    });
})(
    CofoundrySPA.Models = CofoundrySPA.Models || {},
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);