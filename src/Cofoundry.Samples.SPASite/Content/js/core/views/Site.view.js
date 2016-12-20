(function (components, pages, app, $, _, Backbone) {
    app.SiteView = Backbone.View.extend({
        el : 'html',
        events : {
            
        },
        initialize : function() {
        },
        render : function() {
            if (this.currentPage) {
                this.currentPage.render();
            }
            
            return;
        },
        setCurrentPage : function(pageView) {
            this.currentPage = pageView;
        },
        enterLoadingState : function() {
            this.$el.addClass('JS-loading');
        },
        exitLoadingState : function() {
            this.$el.removeClass('JS-loading');
        }
    });
})(
    CofoundrySPA.ComponentViews,
    CofoundrySPA.PageViews,
    CofoundrySPA.App,
    jQuery, 
    _, 
    Backbone
);