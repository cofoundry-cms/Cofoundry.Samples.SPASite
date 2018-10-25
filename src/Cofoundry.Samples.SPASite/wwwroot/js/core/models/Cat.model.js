(function (models, app, $, _, Backbone) {
    models.Cat = Backbone.Model.extend({
        url: function() {
            if (this.id) {
                var urlstring = '/api/cats/' + this.id;
                return urlstring;
            }
        },
        parse: function(response) {
            var parsedResponse = response;

            if (this.id) {
                parsedResponse = response.data;
            }

            return parsedResponse;
        },
        formatMainImage: function(data) {
            var assetId = data.mainImage.imageAssetId,
                fileName = data.mainImage.fileName,
                extension = data.mainImage.extension,
                imagePath = '/assets/images/' + assetId + '_' + fileName + '.' + extension;

            return imagePath;
        },
        formatImages: function(data) {
            var imageArray = [];

            _.each(data.images, function(image) {
                var assetId = image.imageAssetId,
                    fileName = image.fileName,
                    extension = image.extension,
                    imagePath = '/assets/images/' + assetId + '_' + fileName + '.' + extension;

                imageArray.push(imagePath);
            }, this);

            return imageArray;
        }
    });
})(
    CofoundrySPA.Models = CofoundrySPA.Models || {},
    CofoundrySPA.App,
    jQuery,
    _, 
    Backbone
);