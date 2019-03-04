<template>
    <div class="col-sm-3 cat">
        <router-link :to="{ name: 'catDetails', params: { id: cat.catId }}">
            <div class="cat__image">
                <image-asset :image="cat.mainImage" :width="263" :height="263"/>
            </div>
            <div class="cat__details">
                <span class="name">{{ cat.name }}</span>
                <likes-counter :num-likes="cat.totalLikes"/>
            </div>
        </router-link>
    </div>
</template>

<script>
import ImageAsset from "@/components/ImageAsset";
import LikesCounter from "@/components/LikesCounter";

export default {
    name: "CatItem",
    components: {
        ImageAsset,
        LikesCounter
    },
    props: {
        cat: Object
    }
};
</script>

<style scoped lang="scss">
.cat {
    margin-bottom: 30px;

    &:hover {
        .cat__image img {
            transform: translate(-50%, -50%) scale(1.1);
        }

        .name {
            color: $cms-color-secondary;
        }
    }
}

.cat__image {
    width: 100%;
    padding-bottom: 100%;
    overflow: hidden;
    position: relative;

    img {
        position: absolute;
        left: 50%;
        top: 50%;
        width: auto;
        height: 100%;
        transform: translate(-50%, -50%);
        transform-origin: 50% 50%;
        transition: transform 1s linear;
    }
}

.cat__details {
    margin-top: 10px;

    .name {
        color: $cms-color-text;
        font-weight: 700;
        font-size: 1.125rem;
        display: block;
    }

    .likes {
        float: left;
    }

    @include respond-min(992px) {
        .likes {
            float: right;
        }

        .name {
            display: inline-block;
        }
    }
}
</style>
