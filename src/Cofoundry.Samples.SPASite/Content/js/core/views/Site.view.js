(function (components, pages, app, $, _, Backbone) {
    app.SiteView = Backbone.View.extend({
        el : '.main',
        events : {
            
        },
        initialize : function() {
            this.menu = new components.Menu();
        },
        render : function() {          
            return this;
        },
        goto : function(view) {
            var current = this.currentPage || null;
            var next = view;

            if (current) {
                current.remove();
            }

            next.render();
            this.$el.append(next.$el);

            this.currentPage = next;

            // if (this.currentPage) {
            //     var prevView = this.currentPage;
            //     this.currentPage = pageView;

            //     console.log(prevView, this.currentPage);

            //     prevView.remove();
            // } else {
            //     this.currentPage = pageView;                
            // }
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