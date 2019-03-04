<template>
    <div class="container content-block" v-if="result">
        <div class="row">
            <cat-item v-for="cat in result.items" :cat="cat" :key="cat.catId"/>
        </div>
    </div>
</template>

<script>
import CatItem from "@/components/CatItem";

export default {
    name: "CatGrid",
    components: {
        CatItem
    },
    props: {
        result: Object
    }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
</style>
