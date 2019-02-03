<template>
<div v-if="errors.length" class="error form-error">
    <span v-if="errors.length === 1">{{errors[0]}}</span>
    <ul v-if="errors.length > 1">
        <li v-for="message in errors" :key="message">{{message}}</li>
    </ul>
</div>
</template>

<script>

export default {
    name: 'ValidationSummary',
    props: {
        errors: Array
    }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">

</style>
