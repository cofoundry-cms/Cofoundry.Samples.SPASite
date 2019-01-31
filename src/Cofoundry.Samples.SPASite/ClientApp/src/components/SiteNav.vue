<template>
<nav class="navbar navbar-default navbar-fixed-top">
    <div class="container">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-6" aria-expanded="false">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <router-link to="/" class="site-logo">SPA Cats</router-link>
        </div>

        <div id="bs-example-navbar-collapse-6" aria-expanded="false" class="navbar-collapse collapse">
            <ul class="navbar__menu">
                <li><router-link to="/" class="navbar__link">Cats</router-link></li>
                <li><router-link to="/login" class="navbar__link" v-if="!member">Login</router-link></li>
                <li><router-link to="/register" class="navbar__link" v-if="!member">Register</router-link></li>
                <li><button class="navbar__link logout" v-if="member" @click="signOut">Logout</button></li>
            </ul>
        </div>
    </div>
</nav>
</template>

<script>
import { mapState, mapActions } from 'vuex';

export default {
    name: 'SiteNav',
    computed: mapState('auth', [
        'member'
    ]),
    methods: mapActions('auth', [
        'signOut'
    ])
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

</style>
