<template>
  <img :src="url" :title="image.title"/>
</template>

<script>

export default {
  name: 'ImageAsset',
  props: {
    image: Object,
    width: Number,
    height:  Number
  },
  computed: {
    url () {
      let url = this.image.url;

      if (this.width > 0 || this.height > 0) {
        url += "?";

        if (this.width > 0) url += "width=" + this.width;
        if (this.height > 0) url += "height=" + this.height;
      }

      return url;
    }    
  }
}
</script>
