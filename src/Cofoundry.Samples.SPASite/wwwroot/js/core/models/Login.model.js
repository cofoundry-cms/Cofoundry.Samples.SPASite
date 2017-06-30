(function (models, app, $, _, Backbone) {
    models.Login = Backbone.Model.extend({
        url: '/api/auth/login',
        defaults: {
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