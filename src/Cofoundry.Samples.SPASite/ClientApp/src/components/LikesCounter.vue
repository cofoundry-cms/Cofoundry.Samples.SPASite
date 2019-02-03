<template>
    <span class="likes">{{ numLikes }}</span>
</template>

<script>

export default {
  name: 'LikesCounter',
  props: {
    numLikes: Number
  }
}
</script>

<style scoped lang="scss">

.likes {
    color: $cms-color-text;
    font-weight: bold;

    &:before {
        content: "";
        background: url("/images/heart-icon.png") no-repeat left top;
        background-size: cover;
        width: 15px;
        height: 13px;
        display: inline-block;    
        margin-right: 12px;
        top: 1px;
        position: relative;
    }
}
</style>
