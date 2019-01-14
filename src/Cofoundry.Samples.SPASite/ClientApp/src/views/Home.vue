<template>
  <main class="main">
    <div class="hero-banner">

      <img src="/images/cat-fight-hero.jpg">

      <div class="container hero-banner__overlay">
        <div class="hero-banner__text">
          <h1>Cats SPArring!</h1>
          <p>Welcome to SPA cats, the site where beautiful moggies SPA for your affections. We have some of the smartest, fluffies kitties battling to become top cat.</p>
          <p>Register now and you can help your favourite cat in the race to become most loved.</p>
        </div>
      </div>
  </div>

  <div v-if="loading">
    Loading...
  </div>
  
  <cat-grid v-if="searchResult"  :result="searchResult"/>

  </main>
</template>

<script>

import CatGrid from '@/components/CatGrid.vue'
import catsApi from '@/api/cats'

export default {
  name: 'home',
  components: {
    CatGrid
  },
  data () {
    return {
      loading: false,
      searchResult: null
    }
  },
  created () {
    this.loadGrid();
  },
  watch: {
    '$route': 'loadGrid'
  },
  methods: {
    loadGrid () {
      this.loading = true;
      catsApi.searchCats().then(result => {
        this.loading = false;
        this.searchResult = result;
      });
    }
  }
}
</script>
