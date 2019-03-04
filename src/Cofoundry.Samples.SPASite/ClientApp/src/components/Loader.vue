<template>
    <div>
        <div class="loader" v-if="isLoading">
            Loading...
        </div>
        <slot  v-if="!isLoading">
        </slot>
    </div>
</template>

<script>

export default {
    name: "Loader",
    props: {
        isLoading: Boolean
    }
};
</script>

<style scoped lang="scss">

</style>
