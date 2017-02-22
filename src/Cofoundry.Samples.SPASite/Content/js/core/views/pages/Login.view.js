(function (pages, itemViews, models, app, $, _, Backbone, helper) {
    pages.Login = Backbone.View.extend({
        tagName: 'div',
        template: _.template($('#login').html()),
        events: {
            'submit .login-form': 'onFormSubmit'
        },

        initialize : function() {
            this.model = new models.Login();
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
                    that.getToken();
                }
            });
        },
        handleErrors: function(errors) {
            _.each(errors, function(error) {
                if (error.properties.length > 0) {
                    var name = error.properties[0].toLowerCase(),
                    message = error.message,
                    $input = this.$el.find('input[name="' + name + '"] + .error');

                    $input.text(message).removeClass('hidden');
                } else {
                    var message = error.message,
                        $formError = this.$el.find('.form-error');

                    $formError.text(message).removeClass('hidden');
                }
            }, this);
        },
        clearErrors: function() {
            var errorTexts = this.$el.find('.error');

            _.each(errorTexts, function(error) {
                if (!$(error).hasClass('hidden')) $(error).addClass('hidden');
                $(error).text('');
            });
        },
        getToken: function() {
            var url = '/api/auth/csrf-token',
                that = this;

            $.ajax({
                url: url,
                type: 'GET'
            })
            .done(function(data, response) {
                var token = data.data;
                that.handleLogin(token);
            });
        },
        handleLogin: function(token) {
            this.showLoginMessage();
            
            SPACatsState.csrfToken = token;
            app.User.set({authenticated: true, token: token});
        },
        showLoginMessage: function() {
            this.$el.find('.login-form').addClass('hidden');
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