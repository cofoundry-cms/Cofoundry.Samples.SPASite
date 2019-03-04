module.exports = {
    css: {
        loaderOptions: {
            sass: {
                data: `
                  @import "@/scss/mixins.scss";
                  @import "@/scss/variables.scss";
                  @import "@/scss/normalize.scss";
                  @import "@/scss/typography.scss";
                  @import "@/scss/layout.scss";
                `
            }
        }
    }
}
