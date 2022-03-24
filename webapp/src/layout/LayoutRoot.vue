<script setup lang="ts">
import LayoutHeader from "./LayoutHeader.vue";
</script>

<template>
  <LayoutHeader class="header" />
  <div class="router-view">
    <RouterView />
  </div>
</template>

<style scoped>
.header {
  z-index: 999;
}
.router-view {
  top: 76px;
  position: relative;
}
</style>
