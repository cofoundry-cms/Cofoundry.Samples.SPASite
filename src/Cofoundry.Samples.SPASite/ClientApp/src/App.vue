<template>
  <div>
    <site-nav/>
    <router-view/>
  </div>
</template>

<script>

import SiteNav from '@/components/SiteNav.vue'

export default {
  components: {
    SiteNav
  }
}
</script>

<style lang="scss">

</style>
