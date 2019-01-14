<template>
  <div class="col-sm-3 cat">
    <router-link :to="{ name: 'catDetails', params: { id: cat.catId }}">
      <div class="cat__image">
          <image-asset :image="cat.mainImage" :width="263" :height="263" />
      </div>
      <div class="cat__details">
          <span class="name">{{ cat.name }}</span>
          <span class="likes">{{ cat.totalLikes }}</span>
      </div>
    </router-link>
  </div>
</template>

<script>
import ImageAsset from '@/components/ImageAsset'

export default {
  name: 'CatItem',
  props: {
    cat: Object
  },
  components: {
    ImageAsset
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

</style>
