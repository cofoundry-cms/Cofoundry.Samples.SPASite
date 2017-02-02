(function (pages, itemViews, models, app, $, _, Backbone, helper) {
    pages.Register = Backbone.View.extend({
        tagName: 'div',
        template: _.template($('#register').html()),
        events: {
            'submit .register-form': 'onFormSubmit'
        },

        initialize : function() {
            this.model = new models.Register();
        },
        render : function() {
            this.$el.html(this.template());
            return this;
        },
        onFormSubmit: function(e) {
            e.preventDefault();
            this.clearErrors();

            var that = this;

            this.$el.find('input[name]').each(function() {
                that.model.set(this.name, this.value);
            });

            this.model.save(null, {
                error: function(model, response) {
                    var JSONResponse = JSON.parse(response.responseText),
                        errors = JSONResponse.errors;

                    that.handleErrors(errors);
                },
                success: function(model, response) {
                    var token = response.data.antiForgeryToken;

                    that.handleRegister(token);
                }
            });
        },
        handleErrors: function(errors) {
            console.log(errors);

            _.each(errors, function(error) {
                var name = error.properties[0].toLowerCase(),
                    message = error.message,
                    $input = this.$el.find('input[name="' + name + '"] + .error');

                $input.text(message).removeClass('hidden');
            }, this);
        },
        clearErrors: function() {
            var errorTexts = this.$el.find('.error');

            _.each(errorTexts, function(error) {
                if (!$(error).hasClass('hidden')) $(error).addClass('hidden');
                $(error).text('');
            });
        },
        handleRegister: function(token) {
            this.showRegisteredMessage();

            SPACatsState.csrfToken = token;
            app.User.set({authenticated: true, token: token});
        },
        showRegisteredMessage: function() {
            this.$el.find('.register-form').addClass('hidden');
            this.$el.find('.message').removeClass('hidden');
        }
    });
})(
    CofoundrySPA.PageViews = CofoundrySPA.PageViews || {},
    CofoundrySPA.ItemViews,
    CofoundrySPA.Models,
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone,
    Helper
);