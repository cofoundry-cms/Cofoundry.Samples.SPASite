<template>
    <div class="container content-block">
        <loader :is-loading="loading" />

        <div v-if="cat">
            <div class="col-sm-6 cat-images">
                <image-asset
                    v-for="image in cat.images"
                    :key="image.imageAssetId"
                    :image="image"
                    :width="540"
                />
            </div>

            <div class="col-sm-6 cat-info">
                <h2 class="name">
                    {{cat.name}}
                    <span class="likes--big">{{cat.totalLikes}}</span>
                </h2>
                <span class="breed" v-if="cat.breed">{{cat.breed.title}}</span>

                <div class="characteristics">
                    <span class="title">Characteristics:</span>
                    <span
                        class="characteristic"
                        v-for="feature in cat.features"
                        :key="feature.featureId"
                    >{{ feature.title }}</span>
                </div>
                <p class="description">{{cat.description}}</p>
                <button class="btn btn-love" @click="handleLike" v-if="member && !isLiked">I
                    <span class="glyphicon glyphicon-heart"></span>
                    {{cat.name}}
                </button>
                <button class="btn btn-love" @click="handleLike" v-if="member && isLiked">I dont
                    <span class="glyphicon glyphicon-heart"></span>
                    {{cat.name}}
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import { mapState } from "vuex";
import catsApi from "@/api/cats";
import ImageAsset from "@/components/ImageAsset";
import Loader from "@/components/Loader";

export default {
    name: "catDetails",
    components: {
        ImageAsset,
        Loader
    },
    data() {
        return {
            loading: true,
            cat: null
        };
    },
    computed: {
        isLiked() {
            return this.cat !== null 
                && this.$store.state.cats.likedCatIds.indexOf(this.cat.catId) !== -1;
        },
        ...mapState("auth", ["member"])
    },
    created() {
        this.loadCat();
    },
    methods: {
        loadCat() {
            this.loading = true;
            catsApi.getCatById(this.$route.params.id).then(result => {
                this.cat = result;
                this.loading = false;
            });
        },
        handleLike() {
            const actionName = this.isLiked ? "unlike" : "like";
            const likeModifier = this.isLiked ? -1 : 1;

            this.$store
                .dispatch("cats/" + actionName, this.cat.catId)
                .then(() => {
                    this.cat.totalLikes += likeModifier;
                });
        }
    }
};
</script>

<style scoped lang="scss">
.cat-images {
    position: relative;

    img {
        width: 100%;
        height: auto;
    }
}

.cat-info {
    padding-top: 30px;

    h2 {
        margin-bottom: 0;
        display: inline-block;
        position: relative;
        padding-right: 65px;
    }

    .breed {
        display: block;
        margin-bottom: 30px;
        font-weight: 700;
        text-transform: uppercase;
    }

    .description {
        margin-bottom: 30px;
    }

    @include respond-min($tablet) {
        padding-top: 0;
    }
}

.characteristics {
    margin-bottom: 30px;

    .title {
        display: block;
    }
}

.btn-love {
    background-color: $cms-color-secondary;
    color: white;
    transition: background-color 0.2s ease-out;

    &:hover,
    &:active {
        background-color: $cms-color-primary;
        color: white;
    }
}
.likes--big {
    background: url("/images/heart-icon.png") no-repeat left top;
    background-size: cover;
    width: 45px;
    height: 39px;
    line-height: 39px;
    text-align: center;
    font-size: 20px;
    position: absolute;
    top: 0;
    right: 0;
    color: white;
    font-weight: bold;
}
</style>