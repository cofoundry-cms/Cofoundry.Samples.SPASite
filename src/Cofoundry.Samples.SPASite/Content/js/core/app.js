"use strict";

// Application namespace
var CofoundrySPA = CofoundrySPA || {};

(function(app, $, _, Backbone){
    app.routeMap = {
        '(/)'               : 'index',
        'cat/:id'           : 'details',
        'login'             : 'login',
        'register'          : 'register'
    }

    // Global event publish/subscribe
    app.Events = _.extend({}, Backbone.Events);

    // Global user profile
    app.User = {
        'authenticated': false,
        'token': null,
        'favourites': []
    };

    app.Router = Backbone.Router.extend({
        routes : app.routeMap
    });

})(CofoundrySPA.App = CofoundrySPA.App || {}, jQuery, _, Backbone);
