<template>
  <div>
    <site-nav/>
    <router-view/>
  </div>
</template>

<script>

import SiteNav from '@/components/SiteNav.vue'

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
    this.$store.dispatch('auth/loadSession');
  }
}
</script>

<style lang="scss">

</style>
