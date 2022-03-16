<script setup lang="ts">
import { RouterView } from "vue-router";
import LayoutHeader from "@/shared/header/LayoutHeader.vue";
</script>

<template>
  <div class="component">
    <LayoutHeader />
    <RouterView />
  </div>
</template>

<style scoped>
.component {
  display: flex;
  flex-direction: column;
}
</style>
