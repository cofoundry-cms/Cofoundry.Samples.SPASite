(function (pages, itemViews, models, app, $, _, Backbone) {
    pages.Register = Backbone.View.extend({
        el : 'main',
        template: _.template($('#register').html()),
        events: {
            'submit .form': 'onFormSubmit'
        },

        initialize : function() {
            this.model = new models.Register();
            this.render();
        },
        render : function() {
            this.$el.empty().append(this.template);

            return this;
        },
        onFormSubmit: function(e) {
            e.preventDefault();

            var that = this;

            this.$el.find('input[name]').each(function() {
                that.model.set(this.name, this.value);
            });

            this.model.save(null, {
                error: function(model, response) {
                    console.log('error', response);
                },
                success: function(model, response) {
                    console.log('error', response);  
                }
            });
        },
    });
})(
    CofoundrySPA.PageViews = CofoundrySPA.PageViews || {},
    CofoundrySPA.ItemViews,
    CofoundrySPA.Models,
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);