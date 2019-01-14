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

            <a href="/" class="site-logo">SPA Cats</a>
        </div>

        <div id="bs-example-navbar-collapse-6" aria-expanded="false" class="navbar-collapse collapse">
            <ul class="navbar__menu">
<!--               
      <router-link to="/">Cats</router-link> |
      <router-link to="/login">Login</router-link>
      <router-link to="/register">Register</router-link> -->
                <li><a href="/" class="navbar__link">Cats</a></li>
                <li><a href="/login" class="navbar__link unauth">Login</a></li>
                <li><a href="/register" class="navbar__link unauth">Register</a></li>
                <li><form action="/sign-out" method="POST"><button class="navbar__link logout auth hidden" type="submit">Logout</button></form></li>
            </ul>
        </div>
    </div>
</nav>
</template>

<script>
export default {
  name: 'SiteNav'
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

</style>
