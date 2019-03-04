<template>
    <div>
        <site-nav/>
        <router-view/>
    </div>
</template>

<script>
import SiteNav from "@/components/SiteNav.vue";

export default {
    data() {
        return {
            loading: false
        };
    },
    components: {
        SiteNav
    },
    created() {
        this.loading = true;
        this.$store.dispatch("auth/loadSession");
    }
};
</script>

<style lang="scss">
.content-block {
    margin-top: 30px;

    @include respond-min($tablet) {
        margin-top: 60px;
    }
}

/* Typography */
html {
    font-size: 100%;
    font-family: "Roboto", sans-serif;
    line-height: 1.5em;
    color: $cms-color-text;
    font-weight: 300;
}

strong,
b {
    font-weight: bold;
}

i,
em {
    font-style: italic;
}

u {
    text-decoration: underline;
}

p {
    font-size: 1rem;
    line-height: 1.5rem;
    margin-bottom: 1.5rem;

    a {
        color: $cms-color-primary;
    }
}

h1 {
    font-weight: 700;
    font-size: 2.5rem;
    line-height: 2.5rem;
    margin-bottom: 2rem;
}

h2 {
    font-weight: 700;
    font-size: 2.5rem;
    line-height: 2.5rem;
    margin-bottom: 2rem;
}
</style>
