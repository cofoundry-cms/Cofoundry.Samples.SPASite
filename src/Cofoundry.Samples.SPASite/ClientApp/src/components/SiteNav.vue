<template>
    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button
                    type="button"
                    class="navbar-toggle collapsed"
                    data-toggle="collapse"
                    data-target="#bs-example-navbar-collapse-6"
                    aria-expanded="false"
                >
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <router-link to="/" class="site-logo">SPA Cats</router-link>
            </div>

            <div
                id="bs-example-navbar-collapse-6"
                aria-expanded="false"
                class="navbar-collapse collapse"
            >
                <ul class="navbar__menu">
                    <li>
                        <router-link to="/" class="navbar__link">Cats</router-link>
                    </li>
                    <li>
                        <router-link to="/login" class="navbar__link" v-if="!member">Login</router-link>
                    </li>
                    <li>
                        <router-link to="/register" class="navbar__link" v-if="!member">Register</router-link>
                    </li>
                    <li>
                        <button class="navbar__link logout" v-if="member" @click="signOut">Logout</button>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
    name: "SiteNav",
    computed: mapState("auth", ["member"]),
    methods: mapActions("auth", ["signOut"])
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
.navbar {
    background-color: $cms-color-primary;
    border: 0;
    height: 60px;
}

.navbar-toggle {
    margin-top: 12px;
}

.navbar-default .navbar-collapse {
    border: 0;
    background-color: $cms-color-secondary;

    @include respond-min($tablet) {
        background-color: transparent;
    }
}

.navbar-header {
    height: 60px;
}

.navbar__menu {
    list-style-type: none;
    padding-left: 0;

    .navbar__link {
        background-color: transparent;
        border: 0;
        color: white;
        display: block;
        padding: 10px 0;
        width: 100%;
        text-align: left;
    }

    @include respond-min($tablet) {
        float: right;

        li {
            display: inline-block;
            line-height: 60px;
            padding: 0;

            &:hover {
                background-color: $cms-color-secondary;
            }
        }

        .navbar__link {
            padding: 0 15px;
        }
    }
}

.site-logo {
    background: url("../assets/spacats-logo.png") no-repeat left top;
    background-size: cover;
    margin-top: 11px;
    display: inline-block;
    width: 187px;
    height: 36px;
    @include hide-text();
    margin-left: 15px;

    @include respond-min($tablet) {
        margin-left: 0;
    }
}
</style>
