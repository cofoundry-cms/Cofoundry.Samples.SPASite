(function (pages, components, app, $, _, Backbone) {
    pages.Index = Backbone.View.extend({
        el : 'main',
        events : {

        },
        initialize : function() {
            console.log('word to ya mom!');
        },
        render : function() {
            return;
        }
    });
})(
    CofoundrySPA.PageViews = CofoundrySPA.PageViews || {},
    CofoundrySPA.ComponentViews,
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);