<template>

  <div class="container content-block">
    
    <div v-if="!cat">
      loading..
    </div>

    <div v-if="cat">

      <div class="col-sm-6 cat-images">
          <image-asset v-for="image in cat.images" 
                      :key="image.imageAssetId"
                      :image="image" 
                      :width="540"/>
      </div>

      <div class="col-sm-6 cat-info">
          <h2 class="name">{{cat.name}}<span class="likes--big">{{cat.totalLikes}}</span></h2>
          <span class="breed" v-if="cat.breed">{{cat.breed.title}}</span>

          <div class="characteristics">
              <span class="title">Characteristics:</span>
              <span class="characteristic"
                    v-for="feature in cat.features"
                    :key="feature.featureId">{{ feature.title }}</span>
          </div>
          <p class="description">{{cat.description}}</p>
          <button id="like" class="btn btn-love hidden" @click="handleLike">I <span class="glyphicon glyphicon-heart"></span> {{cat.name}}</button>
      </div>

    </div>
  </div>
</template>

<script>
  import catsApi from '@/api/cats'
  import ImageAsset from '@/components/ImageAsset'
  
  export default {
    name: 'catDetails',
    data () {
      return {
        loading: false,
        cat: null
      };
    },
    created () {
      this.loadCat();
    },
    methods: {
      loadCat () {
        this.loading = true;
        catsApi.getCatById(this.$route.params.id)
          .then(result => {
            console.log(result);
            this.cat = result;
            this.loading = false;
          });
      },
      handleLike () {
        //todo
      }
    },
    components: {
      ImageAsset
    }
  }
</script>